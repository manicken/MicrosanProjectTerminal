
namespace Microsan
{
    partial class ConnectionSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionSettingsForm));
            this.grpBoxProtocolSelect = new System.Windows.Forms.GroupBox();
            this.cmbProtocol = new System.Windows.Forms.ComboBox();
            this.panel = new System.Windows.Forms.Panel();
            this.grpBoxProtocolSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxProtocolSelect
            // 
            this.grpBoxProtocolSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxProtocolSelect.Controls.Add(this.cmbProtocol);
            this.grpBoxProtocolSelect.Location = new System.Drawing.Point(3, 2);
            this.grpBoxProtocolSelect.Name = "grpBoxProtocolSelect";
            this.grpBoxProtocolSelect.Size = new System.Drawing.Size(364, 42);
            this.grpBoxProtocolSelect.TabIndex = 0;
            this.grpBoxProtocolSelect.TabStop = false;
            this.grpBoxProtocolSelect.Text = "Protocol:";
            // 
            // cmbProtocol
            // 
            this.cmbProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProtocol.FormattingEnabled = true;
            this.cmbProtocol.Location = new System.Drawing.Point(6, 14);
            this.cmbProtocol.Name = "cmbProtocol";
            this.cmbProtocol.Size = new System.Drawing.Size(352, 21);
            this.cmbProtocol.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Location = new System.Drawing.Point(0, 43);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(370, 237);
            this.panel.TabIndex = 1;
            // 
            // ConnectionSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 280);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.grpBoxProtocolSelect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConnectionSettingsForm";
            this.Text = "Connection Settings";
            this.grpBoxProtocolSelect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxProtocolSelect;
        private System.Windows.Forms.ComboBox cmbProtocol;
        private System.Windows.Forms.Panel panel;
    }
}