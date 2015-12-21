using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Forms;
using Sanford.Multimedia.Midi;

namespace PianoPlayer
{
    public enum Prokeys88Instrument : byte
    {
        AcousticGrand = 0,
        BrightAcoustic = 1,
        ElectricPiano2 = 2,
        ElectricPiano1 = 4,
        FMPiano = 5,
        Clav = 7,
        PercussiveOrgan = 17,
    }

    public static class ReactiveSequencer
    {
        public static IObservable<IMidiMessage> Create(Sequence seq)
        {
            return Observable
                .Using(() => new Sequencer(), rs => rs.AsObservable())
                .Publish()
                .RefCount();
        }

        public static IObservable<IMidiMessage> AsObservable(this Sequencer rs)
        {
            var pc =
                Observable.FromEventPattern(a => rs.PlayingCompleted += a, r => rs.PlayingCompleted -= r)
                    .Select(_ => Unit.Default);
            var disposed =
                Observable.FromEventPattern(a => rs.Disposed += a, r => rs.Disposed -= r)
                    .Select(_ => Unit.Default);
            var channelMessages =
                Observable.FromEventPattern<ChannelMessageEventArgs>(a => rs.ChannelMessagePlayed += a,
                    r => rs.ChannelMessagePlayed -= r).Select(ea => (IMidiMessage)ea.EventArgs.Message);
            var stopped =
                Observable.FromEventPattern<StoppedEventArgs>(a => rs.Stopped += a, r => rs.Stopped -= r)
                    .Select(_ => Unit.Default);
            var meta =
                Observable.FromEventPattern<MetaMessageEventArgs>(a => rs.MetaMessagePlayed += a,
                    r => rs.MetaMessagePlayed -= r).Select(ea => (IMidiMessage)ea.EventArgs.Message);
            var sysEx =
                Observable.FromEventPattern<SysExMessageEventArgs>(a => rs.SysExMessagePlayed += a,
                    r => rs.SysExMessagePlayed -= r).Select(ea => (IMidiMessage)ea.EventArgs.Message);
            var chased =
                Observable.FromEventPattern<ChasedEventArgs>(a => rs.Chased += a, r => rs.Chased -= r)
                    .Select(_ => Unit.Default);
            var errorStreams = stopped.Merge(disposed);
            var endStreams = pc;
            var dataStreams = channelMessages.Merge(meta).Merge(sysEx);
            return dataStreams.TakeUntil(endStreams).TakeUntil(errorStreams).Publish().RefCount();
        }

        public static IObservable<IMidiMessage> AsObservable(this InputDevice rs)
        {
            var channelMessages =
                Observable.FromEventPattern<ChannelMessageEventArgs>(a => rs.ChannelMessageReceived+= a,
                    r => rs.ChannelMessageReceived -= r).Select(ea => (IMidiMessage)ea.EventArgs.Message);
            var meta =
                Observable.FromEventPattern<SysCommonMessageEventArgs>(a => rs.SysCommonMessageReceived += a,
                    r => rs.SysCommonMessageReceived -= r).Select(ea => (IMidiMessage)ea.EventArgs.Message);
            var sysEx =
                Observable.FromEventPattern<SysExMessageEventArgs>(a => rs.SysExMessageReceived += a,
                    r => rs.SysExMessageReceived -= r).Select(ea => (IMidiMessage)ea.EventArgs.Message);
            var sysRealtime =
                Observable.FromEventPattern<SysRealtimeMessageEventArgs>(a => rs.SysRealtimeMessageReceived += a, r => rs.SysRealtimeMessageReceived -= r)
                    .Select(ea => (IMidiMessage)ea.EventArgs.Message);
            var dataStreams = channelMessages.Merge(meta).Merge(sysEx).Merge(sysRealtime);
            return dataStreams.Publish().RefCount();
        }
    }
    public partial class Form1 : Form
    {
//        Dictionary<GeneralMidiInstrument, Prokeys88Instrument>[] InstrumentMapping = new Dictionary<GeneralMidiInstrument,Prokeys88Instrument>[10];

        Sanford.Multimedia.Midi.Sequencer sequencer1;

        ChannelControl[] channelControls = new ChannelControl[16];
        Channel[] channels = new Channel[16];

        public Form1()
        {
            InitializeComponent();

            cbOutDevice.Items.Add("<none>");
            for (int q = 0; q < Sanford.Multimedia.Midi.OutputDevice.DeviceCount; q++)
            {
                MidiOutCaps caps = OutputDevice.GetDeviceCapabilities(q);
                cbOutDevice.Items.Add(caps.name);
            }
            cbOutDevice.SelectedIndex = cbOutDevice.Items.Count > 1 ? 1 : 0;

            cbInDevice.Items.Add("<none>");
            for (int q = 0; q < Sanford.Multimedia.Midi.InputDevice.DeviceCount; q++)
            {
                MidiInCaps caps = InputDevice.GetDeviceCapabilities(q);
                cbInDevice.Items.Add(caps.name);
            }
            cbInDevice.SelectedIndex = cbInDevice.Items.Count > 1 ? 1 : 0;

            // ChannelControls
            const int controlWidth = 256;
            for (int q = 0; q < 16; q++)
            {
                ChannelControl cc = new ChannelControl(q);
                cc.Size = new Size(controlWidth, cc.Size.Height);
                channelPanel.Controls.Add(cc);
                channelControls[q] = cc;
            }

            RepositionChannelControls();
        }

        void RepositionChannelControls()
        {
            const int margin = 6;
            int controlHeight = channelControls[0].Height;
            int offsetY = controlHeight + margin;

            int rows = (channelPanel.Height + margin) / offsetY;

            int columns = 16 / rows;
            if ((16 % rows) != 0)
                columns++;

            int controlWidth = (channelPanel.Width - (columns - 1) * margin) / columns;
            int offsetX = controlWidth + margin;

            for (int q = 0; q < channelControls.Length; q++)
            {
                int row = q / columns;
                int column = q % columns;
                ChannelControl cc = channelControls[q];
                cc.Size = new Size(controlWidth, controlHeight);
                cc.Location = new Point(column * offsetX, row * offsetY);
            }
        }

        OutputDevice outDevice;
        OutputDevice OutDevice
        {
            get
            {
                if (outDevice == null && cbOutDevice.SelectedIndex > 0)
                    outDevice = new OutputDevice(cbOutDevice.SelectedIndex - 1);
                return outDevice;
            }
        }

        InputDevice inDevice;
        InputDevice InDevice
        {
            get
            {
                if (inDevice == null && cbInDevice.SelectedIndex > 0)
                    inDevice = new InputDevice(cbInDevice.SelectedIndex - 1);
                return inDevice;
            }
        }

        private void buLoad_Click(object sender, EventArgs e)
        {
            if (sequencer1 != null)
            {
                sequencer1.Stop();
                sequencer1.Dispose();
                sequencer1 = null;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            // 
            // sequencer1
            // 
            sequencer1 = new Sanford.Multimedia.Midi.Sequencer();
            sequencer1.PlayingCompleted += new System.EventHandler(this.sequencer1_PlayingCompleted);
            sequencer1.Disposed += new System.EventHandler(this.sequencer1_Disposed);
            sequencer1.ChannelMessagePlayed += new System.EventHandler<Sanford.Multimedia.Midi.ChannelMessageEventArgs>(this.sequencer1_ChannelMessagePlayed);
            sequencer1.Stopped += new System.EventHandler<Sanford.Multimedia.Midi.StoppedEventArgs>(this.sequencer1_Stopped);
            sequencer1.MetaMessagePlayed += new System.EventHandler<Sanford.Multimedia.Midi.MetaMessageEventArgs>(this.sequencer1_MetaMessagePlayed);
            sequencer1.SysExMessagePlayed += new System.EventHandler<Sanford.Multimedia.Midi.SysExMessageEventArgs>(this.sequencer1_SysExMessagePlayed);
            sequencer1.Chased += new System.EventHandler<Sanford.Multimedia.Midi.ChasedEventArgs>(this.sequencer1_Chased);
            sequencer1.Sequence = new Sequence(ofd.FileName);

            {
                bool[] channelsInUse = new bool[16];
                foreach (Track track in sequencer1.Sequence)
                {
                    foreach (MidiEvent midiEvent in track.Iterator())
                    {
                        ChannelMessage cm = midiEvent.MidiMessage as ChannelMessage;
                        if (cm != null)
                            channelsInUse[cm.MidiChannel] = true;
                    }
                }
                for (int q = 0; q < 16; q++)
                {
                    channelControls[q].Visible = channelsInUse[q];
                }
            }
            sequencer1.Start();
            buPauseContinue.Enabled = true;
        }

        private void sequencer1_ChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {
            ChannelMessage msg = e.Message;

            Channel channel = channels[e.Message.MidiChannel];
            ChannelControl channelControl = channelControls[e.Message.MidiChannel];

            // To Be Removed (event please)
            if (channelControl.Muted)
                channel.Mute();
            else
                channel.Unmute();

            switch (msg.Command)
            {
                case ChannelCommand.ProgramChange:
                    {
                        channelControl.CurrentInstrument = (GeneralMidiInstrument)e.Message.Data1;
                        break;
                    }
                case ChannelCommand.NoteOn:
                    {
                        channel.SetInstrument(channelControl.CurrentInstrument);
                        channel.NoteOn(msg.Data1, msg.Data2);
                        break;
                    }
                case ChannelCommand.NoteOff:
                    {
                        channel.NoteOff(msg.Data1, msg.Data2);
                        break;
                    }
                case ChannelCommand.Controller:
                    {
                        channel.SendChannelMessage(msg);
                        break;
                    }
                default:
                    channel.SendChannelMessage(msg);
                    break;
            }
            channelControl.Active = channel.ActiveNotes > 0;
        }

        class Channel
        {
            OutputDevice outDevice;
            int channel;
            int[] currentVelocities = new int[128];

            int activeNotes;
            public int ActiveNotes
            {
                get { return activeNotes; }
            }

            public Channel(OutputDevice outDevice, int channel)
            {
                this.channel = channel;
                this.outDevice = outDevice;
            }

            bool muted = false;
            public void Mute()
            {
                muted = true;

                for (int note = 0; note < 128; note++)
                    if (currentVelocities[note] > 0)
                        PlayNoteOn(note, 0);
            }

            public void Unmute()
            {
                muted = false;
            }

            void StoreVelocity(int note, int velocity)
            {
                bool wasOn = currentVelocities[note] > 0;
                bool isOn = velocity > 0;

                currentVelocities[note] = velocity;

                if (isOn) activeNotes++;
                if (wasOn) activeNotes--;
            }

            public void NoteOn(int note, int velocity)
            {
                StoreVelocity(note, velocity);

                if (velocity == 0 || !muted)
                    PlayNoteOn(note, velocity);
            }

            private void PlayNoteOn(int note, int velocity)
            {
                ChannelMessage msg = new ChannelMessage(ChannelCommand.NoteOn, channel, note, velocity);
                SendChannelMessage(msg);
            }

            public void NoteOff(int note, int velocity)
            {
                StoreVelocity(note, 0);

                PlayNoteOff(note, velocity);
            }

            private void PlayNoteOff(int note, int velocity)
            {
                ChannelMessage msg = new ChannelMessage(ChannelCommand.NoteOff, channel, note, velocity);
                SendChannelMessage(msg);
            }

            GeneralMidiInstrument lastInstrument = (GeneralMidiInstrument)255;
            public void SetInstrument(GeneralMidiInstrument instrument)
            {
                if (instrument == lastInstrument)
                    return;

                ChannelMessage msg = new ChannelMessage(ChannelCommand.ProgramChange, channel, (int)instrument, 0);
                SendChannelMessage(msg);

                lastInstrument = instrument;
            }

            public void SendChannelMessage(ChannelMessage msg)
            {
                outDevice.Send(msg);
            }
        }

        private void sequencer1_Chased(object sender, ChasedEventArgs e)
        {

        }

        private void sequencer1_Disposed(object sender, EventArgs e)
        {

        }

        private void sequencer1_MetaMessagePlayed(object sender, MetaMessageEventArgs e)
        {
        }

        private void sequencer1_PlayingCompleted(object sender, EventArgs e)
        {
            buPauseContinue.Enabled = false;
        }

        bool isStopped;
        private void sequencer1_Stopped(object sender, StoppedEventArgs e)
        {
            isStopped = true;
        }

        private void sequencer1_SysExMessagePlayed(object sender, SysExMessageEventArgs e)
        {
            OutDevice.Send(e.Message);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            RepositionChannelControls();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (outDevice != null)
            {
                outDevice.Dispose();
                outDevice = null;
            }

            if (inDevice != null)
            {
                inDevice.Dispose();
                inDevice = null;
            }

            if (sequencer1 != null)
            {
                sequencer1.Stop();
                sequencer1.Dispose();
                sequencer1 = null;
            }
        }

        private void buRewind_Click(object sender, EventArgs e)
        {
            if (sequencer1 != null)
            {
                sequencer1.Start();
                buPauseContinue.Enabled = true;
            }
        }

        private void buPauseContinue_Click(object sender, EventArgs e)
        {
            if (sequencer1 != null)
            {
                if (isStopped)
                {
                    isStopped = false;
                    sequencer1.Continue();
                    buPauseContinue.Text = "||";
                }
                else
                {
                    sequencer1.Stop();
                    buPauseContinue.Text = ">";
                }
            }
        }

        private void cbOutDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (outDevice != null)
            {
                outDevice.Dispose();
                outDevice = null;
            }

            if (OutDevice == null)
                return;

            for (int q = 0; q < 16; q++)
                channels[q] = new Channel(OutDevice, q);
        }

        private void cbInDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inDevice != null)
            {
                inDevice.StopRecording();
                inDevice.Dispose();
                inDevice = null;
            }

            if (InDevice == null)
                return;

            InDevice.ChannelMessageReceived += new EventHandler<ChannelMessageEventArgs>(InDevice_ChannelMessageReceived);
            InDevice.SysCommonMessageReceived += new EventHandler<SysCommonMessageEventArgs>(InDevice_SysCommonMessageReceived);
            InDevice.SysExMessageReceived += new EventHandler<SysExMessageEventArgs>(InDevice_SysExMessageReceived);
            InDevice.SysRealtimeMessageReceived += new EventHandler<SysRealtimeMessageEventArgs>(InDevice_SysRealtimeMessageReceived);
            InDevice.StartRecording();
        }

        void InDevice_SysExMessageReceived(object sender, SysExMessageEventArgs e)
        {
            OutDevice.Send(e.Message);
        }

        void InDevice_SysRealtimeMessageReceived(object sender, SysRealtimeMessageEventArgs e)
        {
            OutDevice.Send(e.Message);
        }

        void InDevice_SysCommonMessageReceived(object sender, SysCommonMessageEventArgs e)
        {
            OutDevice.Send(e.Message);
        }

        void InDevice_ChannelMessageReceived(object sender, ChannelMessageEventArgs e)
        {
            OutDevice.Send(e.Message);
        }
    }
}