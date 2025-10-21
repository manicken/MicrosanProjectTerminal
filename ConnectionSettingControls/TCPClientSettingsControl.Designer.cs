
namespace Microsan
{
    partial class TCPClientSettingsControl
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
            this.grpBoxIpPort = new System.Windows.Forms.GroupBox();
            this.txtHostIP = new System.Windows.Forms.TextBox();
            this.txtHostPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpBoxMessageStartStop = new System.Windows.Forms.GroupBox();
            this.txtMessageStopId = new System.Windows.Forms.TextBox();
            this.txtMessageStartId = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.grpBoxIpPort.SuspendLayout();
            this.grpBoxMessageStartStop.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxIpPort
            // 
            this.grpBoxIpPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxIpPort.Controls.Add(this.txtHostIP);
            this.grpBoxIpPort.Controls.Add(this.txtHostPort);
            this.grpBoxIpPort.Controls.Add(this.label2);
            this.grpBoxIpPort.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxIpPort.Location = new System.Drawing.Point(3, 3);
            this.grpBoxIpPort.Name = "grpBoxIpPort";
            this.grpBoxIpPort.Size = new System.Drawing.Size(289, 59);
            this.grpBoxIpPort.TabIndex = 11;
            this.grpBoxIpPort.TabStop = false;
            this.grpBoxIpPort.Text = "Host/Ip && Port";
            // 
            // txtHostIP
            // 
            this.txtHostIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHostIP.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHostIP.Location = new System.Drawing.Point(6, 22);
            this.txtHostIP.Name = "txtHostIP";
            this.txtHostIP.Size = new System.Drawing.Size(198, 29);
            this.txtHostIP.TabIndex = 2;
            this.txtHostIP.Text = "192.168.001.004";
            this.txtHostIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtHostPort
            // 
            this.txtHostPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHostPort.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHostPort.Location = new System.Drawing.Point(212, 22);
            this.txtHostPort.Name = "txtHostPort";
            this.txtHostPort.Size = new System.Drawing.Size(69, 29);
            this.txtHostPort.TabIndex = 4;
            this.txtHostPort.Text = "00127";
            this.txtHostPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(198, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = ":";
            // 
            // grpBoxMessageStartStop
            // 
            this.grpBoxMessageStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxMessageStartStop.Controls.Add(this.txtMessageStopId);
            this.grpBoxMessageStartStop.Controls.Add(this.txtMessageStartId);
            this.grpBoxMessageStartStop.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxMessageStartStop.Location = new System.Drawing.Point(3, 63);
            this.grpBoxMessageStartStop.Name = "grpBoxMessageStartStop";
            this.grpBoxMessageStartStop.Size = new System.Drawing.Size(289, 65);
            this.grpBoxMessageStartStop.TabIndex = 10;
            this.grpBoxMessageStartStop.TabStop = false;
            this.grpBoxMessageStartStop.Text = "Message prefix/postfix:";
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
            this.btnDisconnect.Location = new System.Drawing.Point(152, 134);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(131, 34);
            this.btnDisconnect.TabIndex = 9;
            this.btnDisconnect.Text = "Disconnect";
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(12, 134);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(131, 34);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "Connect";
            // 
            // TCPClientSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpBoxIpPort);
            this.Controls.Add(this.grpBoxMessageStartStop);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Name = "TCPClientSettingsControl";
            this.Size = new System.Drawing.Size(296, 175);
            this.grpBoxIpPort.ResumeLayout(false);
            this.grpBoxIpPort.PerformLayout();
            this.grpBoxMessageStartStop.ResumeLayout(false);
            this.grpBoxMessageStartStop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxIpPort;
        public System.Windows.Forms.TextBox txtHostIP;
        public System.Windows.Forms.TextBox txtHostPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpBoxMessageStartStop;
        public System.Windows.Forms.TextBox txtMessageStopId;
        public System.Windows.Forms.TextBox txtMessageStartId;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
    }
}
