namespace PianoPlayer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buLoad = new System.Windows.Forms.Button();
            this.channelPanel = new System.Windows.Forms.Panel();
            this.buRewind = new System.Windows.Forms.Button();
            this.buPauseContinue = new System.Windows.Forms.Button();
            this.cbOutDevice = new System.Windows.Forms.ComboBox();
            this.cbInDevice = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buLoad
            // 
            this.buLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buLoad.Location = new System.Drawing.Point(588, 12);
            this.buLoad.Name = "buLoad";
            this.buLoad.Size = new System.Drawing.Size(152, 23);
            this.buLoad.TabIndex = 0;
            this.buLoad.Text = "Load Midi";
            this.buLoad.UseVisualStyleBackColor = true;
            this.buLoad.Click += new System.EventHandler(this.buLoad_Click);
            // 
            // channelPanel
            // 
            this.channelPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.channelPanel.Location = new System.Drawing.Point(12, 41);
            this.channelPanel.Name = "channelPanel";
            this.channelPanel.Size = new System.Drawing.Size(728, 364);
            this.channelPanel.TabIndex = 1;
            // 
            // buRewind
            // 
            this.buRewind.Location = new System.Drawing.Point(13, 13);
            this.buRewind.Name = "buRewind";
            this.buRewind.Size = new System.Drawing.Size(75, 23);
            this.buRewind.TabIndex = 2;
            this.buRewind.Text = "|<<";
            this.buRewind.UseVisualStyleBackColor = true;
            this.buRewind.Click += new System.EventHandler(this.buRewind_Click);
            // 
            // buPauseContinue
            // 
            this.buPauseContinue.Location = new System.Drawing.Point(94, 13);
            this.buPauseContinue.Name = "buPauseContinue";
            this.buPauseContinue.Size = new System.Drawing.Size(75, 23);
            this.buPauseContinue.TabIndex = 3;
            this.buPauseContinue.Text = "||";
            this.buPauseContinue.UseVisualStyleBackColor = true;
            this.buPauseContinue.Click += new System.EventHandler(this.buPauseContinue_Click);
            // 
            // cbOutDevice
            // 
            this.cbOutDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOutDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOutDevice.FormattingEnabled = true;
            this.cbOutDevice.Location = new System.Drawing.Point(176, 12);
            this.cbOutDevice.Name = "cbOutDevice";
            this.cbOutDevice.Size = new System.Drawing.Size(208, 21);
            this.cbOutDevice.TabIndex = 4;
            this.cbOutDevice.SelectedIndexChanged += new System.EventHandler(this.cbOutDevice_SelectedIndexChanged);
            // 
            // cbInDevice
            // 
            this.cbInDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInDevice.FormattingEnabled = true;
            this.cbInDevice.Location = new System.Drawing.Point(388, 12);
            this.cbInDevice.Name = "cbInDevice";
            this.cbInDevice.Size = new System.Drawing.Size(194, 21);
            this.cbInDevice.TabIndex = 5;
            this.cbInDevice.SelectedIndexChanged += new System.EventHandler(this.cbInDevice_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 417);
            this.Controls.Add(this.cbInDevice);
            this.Controls.Add(this.cbOutDevice);
            this.Controls.Add(this.buPauseContinue);
            this.Controls.Add(this.buRewind);
            this.Controls.Add(this.channelPanel);
            this.Controls.Add(this.buLoad);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buLoad;
        private System.Windows.Forms.Panel channelPanel;
        private System.Windows.Forms.Button buRewind;
        private System.Windows.Forms.Button buPauseContinue;
        private System.Windows.Forms.ComboBox cbOutDevice;
        private System.Windows.Forms.ComboBox cbInDevice;

    }
}

