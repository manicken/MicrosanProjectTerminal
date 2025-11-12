
namespace Microsan
{
    partial class MqttClientSettingsControl
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
            this.grpClientID = new System.Windows.Forms.GroupBox();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.grpTopic = new System.Windows.Forms.GroupBox();
            this.txtTopic = new System.Windows.Forms.TextBox();
            this.grpUsernamePassword = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.grpRetain = new System.Windows.Forms.GroupBox();
            this.chkWithRetainFlag = new System.Windows.Forms.CheckBox();
            this.grpTLS = new System.Windows.Forms.GroupBox();
            this.chkTLS = new System.Windows.Forms.CheckBox();
            this.grpBoxIpPort.SuspendLayout();
            this.grpBoxMessageStartStop.SuspendLayout();
            this.grpClientID.SuspendLayout();
            this.grpTopic.SuspendLayout();
            this.grpUsernamePassword.SuspendLayout();
            this.grpRetain.SuspendLayout();
            this.grpTLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxIpPort
            // 
            this.grpBoxIpPort.Controls.Add(this.txtHostIP);
            this.grpBoxIpPort.Controls.Add(this.txtHostPort);
            this.grpBoxIpPort.Controls.Add(this.label2);
            this.grpBoxIpPort.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxIpPort.Location = new System.Drawing.Point(3, 3);
            this.grpBoxIpPort.Name = "grpBoxIpPort";
            this.grpBoxIpPort.Size = new System.Drawing.Size(262, 59);
            this.grpBoxIpPort.TabIndex = 11;
            this.grpBoxIpPort.TabStop = false;
            this.grpBoxIpPort.Text = "Host/Ip && Port";
            // 
            // txtHostIP
            // 
            this.txtHostIP.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHostIP.Location = new System.Drawing.Point(6, 22);
            this.txtHostIP.Name = "txtHostIP";
            this.txtHostIP.Size = new System.Drawing.Size(174, 29);
            this.txtHostIP.TabIndex = 2;
            this.txtHostIP.Text = "192.168.001.004";
            this.txtHostIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtHostPort
            // 
            this.txtHostPort.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHostPort.Location = new System.Drawing.Point(186, 22);
            this.txtHostPort.Name = "txtHostPort";
            this.txtHostPort.Size = new System.Drawing.Size(69, 29);
            this.txtHostPort.TabIndex = 4;
            this.txtHostPort.Text = "00127";
            this.txtHostPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(173, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = ":";
            // 
            // grpBoxMessageStartStop
            // 
            this.grpBoxMessageStartStop.Controls.Add(this.txtMessageStopId);
            this.grpBoxMessageStartStop.Controls.Add(this.txtMessageStartId);
            this.grpBoxMessageStartStop.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxMessageStartStop.Location = new System.Drawing.Point(3, 279);
            this.grpBoxMessageStartStop.Name = "grpBoxMessageStartStop";
            this.grpBoxMessageStartStop.Size = new System.Drawing.Size(262, 65);
            this.grpBoxMessageStartStop.TabIndex = 10;
            this.grpBoxMessageStartStop.TabStop = false;
            this.grpBoxMessageStartStop.Text = "Message prefix/postfix:";
            // 
            // txtMessageStopId
            // 
            this.txtMessageStopId.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessageStopId.Location = new System.Drawing.Point(137, 22);
            this.txtMessageStopId.MaxLength = 8;
            this.txtMessageStopId.Name = "txtMessageStopId";
            this.txtMessageStopId.Size = new System.Drawing.Size(117, 31);
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
            this.txtMessageStartId.Size = new System.Drawing.Size(122, 31);
            this.txtMessageStartId.TabIndex = 4;
            this.txtMessageStartId.Text = "12345678";
            this.txtMessageStartId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.Location = new System.Drawing.Point(140, 350);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(125, 34);
            this.btnDisconnect.TabIndex = 9;
            this.btnDisconnect.Text = "Disconnect";
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(3, 350);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(131, 34);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "Connect";
            // 
            // grpClientID
            // 
            this.grpClientID.Controls.Add(this.txtClientID);
            this.grpClientID.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpClientID.Location = new System.Drawing.Point(3, 69);
            this.grpClientID.Name = "grpClientID";
            this.grpClientID.Size = new System.Drawing.Size(262, 49);
            this.grpClientID.TabIndex = 12;
            this.grpClientID.TabStop = false;
            this.grpClientID.Text = "Client ID:";
            // 
            // txtClientID
            // 
            this.txtClientID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientID.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientID.Location = new System.Drawing.Point(10, 20);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.Size = new System.Drawing.Size(244, 22);
            this.txtClientID.TabIndex = 0;
            // 
            // grpTopic
            // 
            this.grpTopic.Controls.Add(this.txtTopic);
            this.grpTopic.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTopic.Location = new System.Drawing.Point(3, 124);
            this.grpTopic.Name = "grpTopic";
            this.grpTopic.Size = new System.Drawing.Size(262, 49);
            this.grpTopic.TabIndex = 13;
            this.grpTopic.TabStop = false;
            this.grpTopic.Text = "Default Topic:";
            // 
            // txtTopic
            // 
            this.txtTopic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTopic.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTopic.Location = new System.Drawing.Point(10, 20);
            this.txtTopic.Name = "txtTopic";
            this.txtTopic.Size = new System.Drawing.Size(244, 22);
            this.txtTopic.TabIndex = 0;
            // 
            // grpUsernamePassword
            // 
            this.grpUsernamePassword.Controls.Add(this.txtPassword);
            this.grpUsernamePassword.Controls.Add(this.txtUsername);
            this.grpUsernamePassword.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpUsernamePassword.Location = new System.Drawing.Point(3, 179);
            this.grpUsernamePassword.Name = "grpUsernamePassword";
            this.grpUsernamePassword.Size = new System.Drawing.Size(262, 49);
            this.grpUsernamePassword.TabIndex = 14;
            this.grpUsernamePassword.TabStop = false;
            this.grpUsernamePassword.Text = "Username/Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(126, 21);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(129, 22);
            this.txtPassword.TabIndex = 0;
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(10, 20);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(107, 22);
            this.txtUsername.TabIndex = 0;
            // 
            // grpRetain
            // 
            this.grpRetain.Controls.Add(this.chkWithRetainFlag);
            this.grpRetain.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRetain.Location = new System.Drawing.Point(3, 228);
            this.grpRetain.Name = "grpRetain";
            this.grpRetain.Size = new System.Drawing.Size(142, 45);
            this.grpRetain.TabIndex = 15;
            this.grpRetain.TabStop = false;
            this.grpRetain.Text = "WithRetainFlag:";
            // 
            // chkWithRetainFlag
            // 
            this.chkWithRetainFlag.AutoSize = true;
            this.chkWithRetainFlag.Location = new System.Drawing.Point(10, 19);
            this.chkWithRetainFlag.Name = "chkWithRetainFlag";
            this.chkWithRetainFlag.Size = new System.Drawing.Size(15, 14);
            this.chkWithRetainFlag.TabIndex = 0;
            this.chkWithRetainFlag.UseVisualStyleBackColor = true;
            // 
            // grpTLS
            // 
            this.grpTLS.Controls.Add(this.chkTLS);
            this.grpTLS.Enabled = false;
            this.grpTLS.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTLS.Location = new System.Drawing.Point(140, 228);
            this.grpTLS.Name = "grpTLS";
            this.grpTLS.Size = new System.Drawing.Size(125, 45);
            this.grpTLS.TabIndex = 16;
            this.grpTLS.TabStop = false;
            this.grpTLS.Text = "Use TLS:";
            // 
            // chkTLS
            // 
            this.chkTLS.AutoSize = true;
            this.chkTLS.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTLS.Location = new System.Drawing.Point(10, 19);
            this.chkTLS.Name = "chkTLS";
            this.chkTLS.Size = new System.Drawing.Size(110, 18);
            this.chkTLS.TabIndex = 0;
            this.chkTLS.Text = "future impl.";
            this.chkTLS.UseVisualStyleBackColor = true;
            // 
            // MqttClientSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpTLS);
            this.Controls.Add(this.grpRetain);
            this.Controls.Add(this.grpUsernamePassword);
            this.Controls.Add(this.grpTopic);
            this.Controls.Add(this.grpClientID);
            this.Controls.Add(this.grpBoxIpPort);
            this.Controls.Add(this.grpBoxMessageStartStop);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Name = "MqttClientSettingsControl";
            this.Size = new System.Drawing.Size(271, 402);
            this.grpBoxIpPort.ResumeLayout(false);
            this.grpBoxIpPort.PerformLayout();
            this.grpBoxMessageStartStop.ResumeLayout(false);
            this.grpBoxMessageStartStop.PerformLayout();
            this.grpClientID.ResumeLayout(false);
            this.grpClientID.PerformLayout();
            this.grpTopic.ResumeLayout(false);
            this.grpTopic.PerformLayout();
            this.grpUsernamePassword.ResumeLayout(false);
            this.grpUsernamePassword.PerformLayout();
            this.grpRetain.ResumeLayout(false);
            this.grpRetain.PerformLayout();
            this.grpTLS.ResumeLayout(false);
            this.grpTLS.PerformLayout();
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
        private System.Windows.Forms.GroupBox grpClientID;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.GroupBox grpTopic;
        private System.Windows.Forms.TextBox txtTopic;
        private System.Windows.Forms.GroupBox grpUsernamePassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.GroupBox grpRetain;
        private System.Windows.Forms.CheckBox chkWithRetainFlag;
        private System.Windows.Forms.GroupBox grpTLS;
        private System.Windows.Forms.CheckBox chkTLS;
    }
}
