
namespace Microsan
{
    partial class SerialSettingsControl
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
            this.grpBoxPort = new System.Windows.Forms.GroupBox();
            this.btnRefreshPorts = new System.Windows.Forms.Button();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.grpBoxMessageStartStop = new System.Windows.Forms.GroupBox();
            this.txtMessageStopId = new System.Windows.Forms.TextBox();
            this.txtMessageStartId = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.grpBaud = new System.Windows.Forms.GroupBox();
            this.txtBaudRate = new System.Windows.Forms.TextBox();
            this.grpBoxPort.SuspendLayout();
            this.grpBoxMessageStartStop.SuspendLayout();
            this.grpBaud.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxPort
            // 
            this.grpBoxPort.Controls.Add(this.btnRefreshPorts);
            this.grpBoxPort.Controls.Add(this.cmbPort);
            this.grpBoxPort.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxPort.Location = new System.Drawing.Point(3, 3);
            this.grpBoxPort.Name = "grpBoxPort";
            this.grpBoxPort.Size = new System.Drawing.Size(289, 59);
            this.grpBoxPort.TabIndex = 11;
            this.grpBoxPort.TabStop = false;
            this.grpBoxPort.Text = "Port";
            // 
            // btnRefreshPorts
            // 
            this.btnRefreshPorts.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshPorts.Location = new System.Drawing.Point(211, 25);
            this.btnRefreshPorts.Name = "btnRefreshPorts";
            this.btnRefreshPorts.Size = new System.Drawing.Size(73, 23);
            this.btnRefreshPorts.TabIndex = 4;
            this.btnRefreshPorts.Text = "refresh";
            this.btnRefreshPorts.UseVisualStyleBackColor = true;
            this.btnRefreshPorts.Click += new System.EventHandler(this.btnRefreshPorts_Click);
            // 
            // cmbPort
            // 
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(6, 25);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(199, 26);
            this.cmbPort.TabIndex = 3;
            // 
            // grpBoxMessageStartStop
            // 
            this.grpBoxMessageStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxMessageStartStop.Controls.Add(this.txtMessageStopId);
            this.grpBoxMessageStartStop.Controls.Add(this.txtMessageStartId);
            this.grpBoxMessageStartStop.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxMessageStartStop.Location = new System.Drawing.Point(3, 125);
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
            this.btnDisconnect.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.Location = new System.Drawing.Point(152, 196);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(131, 34);
            this.btnDisconnect.TabIndex = 9;
            this.btnDisconnect.Text = "Disconnect";
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnConnect.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(12, 196);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(131, 34);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "Connect";
            // 
            // grpBaud
            // 
            this.grpBaud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBaud.Controls.Add(this.txtBaudRate);
            this.grpBaud.Location = new System.Drawing.Point(3, 68);
            this.grpBaud.Name = "grpBaud";
            this.grpBaud.Size = new System.Drawing.Size(97, 54);
            this.grpBaud.TabIndex = 12;
            this.grpBaud.TabStop = false;
            this.grpBaud.Text = "BAUD";
            // 
            // txtBaudRate
            // 
            this.txtBaudRate.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaudRate.Location = new System.Drawing.Point(6, 19);
            this.txtBaudRate.Name = "txtBaudRate";
            this.txtBaudRate.Size = new System.Drawing.Size(84, 26);
            this.txtBaudRate.TabIndex = 0;
            this.txtBaudRate.Text = "115200";
            // 
            // SerialSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpBaud);
            this.Controls.Add(this.grpBoxPort);
            this.Controls.Add(this.grpBoxMessageStartStop);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Name = "SerialSettingsControl";
            this.Size = new System.Drawing.Size(296, 237);
            this.grpBoxPort.ResumeLayout(false);
            this.grpBoxMessageStartStop.ResumeLayout(false);
            this.grpBoxMessageStartStop.PerformLayout();
            this.grpBaud.ResumeLayout(false);
            this.grpBaud.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxPort;
        private System.Windows.Forms.GroupBox grpBoxMessageStartStop;
        public System.Windows.Forms.TextBox txtMessageStopId;
        public System.Windows.Forms.TextBox txtMessageStartId;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnRefreshPorts;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.GroupBox grpBaud;
        private System.Windows.Forms.TextBox txtBaudRate;
    }
}
