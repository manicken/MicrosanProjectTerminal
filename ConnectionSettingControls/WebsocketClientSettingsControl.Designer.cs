
namespace Microsan
{
    partial class WebsocketClientSettingsControl
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
            this.grpBoxUri = new System.Windows.Forms.GroupBox();
            this.txtUri = new System.Windows.Forms.TextBox();
            this.grpBoxMessageStartStop = new System.Windows.Forms.GroupBox();
            this.txtMessageStopId = new System.Windows.Forms.TextBox();
            this.txtMessageStartId = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.grpSecure = new System.Windows.Forms.GroupBox();
            this.chkUseSecure = new System.Windows.Forms.CheckBox();
            this.grpBoxUri.SuspendLayout();
            this.grpBoxMessageStartStop.SuspendLayout();
            this.grpSecure.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxUri
            // 
            this.grpBoxUri.Controls.Add(this.txtUri);
            this.grpBoxUri.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxUri.Location = new System.Drawing.Point(3, 3);
            this.grpBoxUri.Name = "grpBoxUri";
            this.grpBoxUri.Size = new System.Drawing.Size(289, 59);
            this.grpBoxUri.TabIndex = 11;
            this.grpBoxUri.TabStop = false;
            this.grpBoxUri.Text = "Uri: (excl ws:// or wss://)";
            // 
            // txtUri
            // 
            this.txtUri.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUri.Location = new System.Drawing.Point(6, 22);
            this.txtUri.Name = "txtUri";
            this.txtUri.Size = new System.Drawing.Size(277, 29);
            this.txtUri.TabIndex = 2;
            this.txtUri.Text = "192.168.001.004";
            this.txtUri.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grpBoxMessageStartStop
            // 
            this.grpBoxMessageStartStop.Controls.Add(this.txtMessageStopId);
            this.grpBoxMessageStartStop.Controls.Add(this.txtMessageStartId);
            this.grpBoxMessageStartStop.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxMessageStartStop.Location = new System.Drawing.Point(3, 115);
            this.grpBoxMessageStartStop.Name = "grpBoxMessageStartStop";
            this.grpBoxMessageStartStop.Size = new System.Drawing.Size(289, 65);
            this.grpBoxMessageStartStop.TabIndex = 10;
            this.grpBoxMessageStartStop.TabStop = false;
            this.grpBoxMessageStartStop.Text = "Message Start/Stop IDs";
            // 
            // txtMessageStopId
            // 
            this.txtMessageStopId.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessageStopId.Location = new System.Drawing.Point(149, 22);
            this.txtMessageStopId.MaxLength = 8;
            this.txtMessageStopId.Name = "txtMessageStopId";
            this.txtMessageStopId.Size = new System.Drawing.Size(130, 31);
            this.txtMessageStopId.TabIndex = 4;
            this.txtMessageStopId.Text = "12345678";
            this.txtMessageStopId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMessageStartId
            // 
            this.txtMessageStartId.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessageStartId.Location = new System.Drawing.Point(9, 22);
            this.txtMessageStartId.MaxLength = 8;
            this.txtMessageStartId.Name = "txtMessageStartId";
            this.txtMessageStartId.Size = new System.Drawing.Size(130, 31);
            this.txtMessageStartId.TabIndex = 4;
            this.txtMessageStartId.Text = "12345678";
            this.txtMessageStartId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.Location = new System.Drawing.Point(155, 186);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(131, 34);
            this.btnDisconnect.TabIndex = 9;
            this.btnDisconnect.Text = "Disconnect";
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(9, 186);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(131, 34);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "Connect";
            // 
            // grpSecure
            // 
            this.grpSecure.Controls.Add(this.chkUseSecure);
            this.grpSecure.Location = new System.Drawing.Point(3, 66);
            this.grpSecure.Name = "grpSecure";
            this.grpSecure.Size = new System.Drawing.Size(289, 43);
            this.grpSecure.TabIndex = 12;
            this.grpSecure.TabStop = false;
            this.grpSecure.Text = "Use secure: (uses wss:// instead of ws://)";
            // 
            // chkUseSecure
            // 
            this.chkUseSecure.AutoSize = true;
            this.chkUseSecure.Location = new System.Drawing.Point(10, 19);
            this.chkUseSecure.Name = "chkUseSecure";
            this.chkUseSecure.Size = new System.Drawing.Size(64, 17);
            this.chkUseSecure.TabIndex = 0;
            this.chkUseSecure.Text = "enabled";
            this.chkUseSecure.UseVisualStyleBackColor = true;
            // 
            // WebsocketClientSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpSecure);
            this.Controls.Add(this.grpBoxUri);
            this.Controls.Add(this.grpBoxMessageStartStop);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Name = "WebsocketClientSettingsControl";
            this.Size = new System.Drawing.Size(296, 223);
            this.grpBoxUri.ResumeLayout(false);
            this.grpBoxUri.PerformLayout();
            this.grpBoxMessageStartStop.ResumeLayout(false);
            this.grpBoxMessageStartStop.PerformLayout();
            this.grpSecure.ResumeLayout(false);
            this.grpSecure.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxUri;
        public System.Windows.Forms.TextBox txtUri;
        private System.Windows.Forms.GroupBox grpBoxMessageStartStop;
        public System.Windows.Forms.TextBox txtMessageStopId;
        public System.Windows.Forms.TextBox txtMessageStartId;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox grpSecure;
        private System.Windows.Forms.CheckBox chkUseSecure;
    }
}
