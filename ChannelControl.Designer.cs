namespace PianoPlayer
{
    partial class ChannelControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbAutoInstrument = new System.Windows.Forms.CheckBox();
            this.cbChosenInstrument = new System.Windows.Forms.ComboBox();
            this.cbMute = new System.Windows.Forms.CheckBox();
            this.cbSolo = new System.Windows.Forms.CheckBox();
            this.laNumber = new System.Windows.Forms.Label();
            this.rbActive = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // cbAutoInstrument
            // 
            this.cbAutoInstrument.AutoSize = true;
            this.cbAutoInstrument.Checked = true;
            this.cbAutoInstrument.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoInstrument.Location = new System.Drawing.Point(4, 3);
            this.cbAutoInstrument.Name = "cbAutoInstrument";
            this.cbAutoInstrument.Size = new System.Drawing.Size(62, 17);
            this.cbAutoInstrument.TabIndex = 0;
            this.cbAutoInstrument.Text = "Not Set";
            this.cbAutoInstrument.UseVisualStyleBackColor = true;
            // 
            // cbChosenInstrument
            // 
            this.cbChosenInstrument.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbChosenInstrument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChosenInstrument.FormattingEnabled = true;
            this.cbChosenInstrument.Location = new System.Drawing.Point(4, 26);
            this.cbChosenInstrument.Name = "cbChosenInstrument";
            this.cbChosenInstrument.Size = new System.Drawing.Size(87, 21);
            this.cbChosenInstrument.TabIndex = 1;
            this.cbChosenInstrument.SelectedIndexChanged += new System.EventHandler(this.cbChosenInstrument_SelectedIndexChanged);
            // 
            // cbMute
            // 
            this.cbMute.AutoSize = true;
            this.cbMute.Location = new System.Drawing.Point(4, 54);
            this.cbMute.Name = "cbMute";
            this.cbMute.Size = new System.Drawing.Size(50, 17);
            this.cbMute.TabIndex = 2;
            this.cbMute.Text = "Mute";
            this.cbMute.UseVisualStyleBackColor = true;
            this.cbMute.CheckedChanged += new System.EventHandler(this.cbMute_CheckedChanged);
            // 
            // cbSolo
            // 
            this.cbSolo.AutoSize = true;
            this.cbSolo.Location = new System.Drawing.Point(4, 77);
            this.cbSolo.Name = "cbSolo";
            this.cbSolo.Size = new System.Drawing.Size(47, 17);
            this.cbSolo.TabIndex = 3;
            this.cbSolo.Text = "Solo";
            this.cbSolo.UseVisualStyleBackColor = true;
            this.cbSolo.CheckedChanged += new System.EventHandler(this.cbSolo_CheckedChanged);
            // 
            // laNumber
            // 
            this.laNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.laNumber.AutoSize = true;
            this.laNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laNumber.Location = new System.Drawing.Point(48, 63);
            this.laNumber.Name = "laNumber";
            this.laNumber.Size = new System.Drawing.Size(47, 31);
            this.laNumber.TabIndex = 4;
            this.laNumber.Text = "1X";
            // 
            // rbActive
            // 
            this.rbActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbActive.AutoSize = true;
            this.rbActive.Enabled = false;
            this.rbActive.Location = new System.Drawing.Point(77, 53);
            this.rbActive.Name = "rbActive";
            this.rbActive.Size = new System.Drawing.Size(14, 13);
            this.rbActive.TabIndex = 5;
            this.rbActive.UseVisualStyleBackColor = true;
            // 
            // ChannelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.rbActive);
            this.Controls.Add(this.laNumber);
            this.Controls.Add(this.cbSolo);
            this.Controls.Add(this.cbMute);
            this.Controls.Add(this.cbChosenInstrument);
            this.Controls.Add(this.cbAutoInstrument);
            this.MaximumSize = new System.Drawing.Size(1280, 96);
            this.MinimumSize = new System.Drawing.Size(96, 96);
            this.Name = "ChannelControl";
            this.Size = new System.Drawing.Size(94, 94);
            this.Load += new System.EventHandler(this.ChannelControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbAutoInstrument;
        private System.Windows.Forms.ComboBox cbChosenInstrument;
        private System.Windows.Forms.CheckBox cbMute;
        private System.Windows.Forms.CheckBox cbSolo;
        private System.Windows.Forms.Label laNumber;
        private System.Windows.Forms.RadioButton rbActive;
    }
}
