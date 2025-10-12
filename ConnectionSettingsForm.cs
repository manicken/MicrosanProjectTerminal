using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Microsan
{
    public partial class ConnectionSettingsForm : Form
    {
        private Action<string> ConnectionTypeSelectedHandler;
        int widthOffset = 0;
        int heightOffset = 0;

        public ConnectionSettingsForm(Action<string> ConnectionTypeSelectedHandler, string[] items)
        {
            InitializeComponent();

            widthOffset = this.Width - panel.Width;
            heightOffset = this.Height - panel.Height;

            cmbProtocol.Items.AddRange(items);

            this.ConnectionTypeSelectedHandler = ConnectionTypeSelectedHandler;
            this.Shown += connectionSettingsForm_Shown;
            this.FormClosing += connectionSettingsForm_FormClosing;
            cmbProtocol.SelectedIndexChanged += cmbProtocol_SelectedIndexChanged;

            //this.SizeChanged += ConnectionSettingsForm_SizeChanged;
        }

        private void ConnectionSettingsForm_SizeChanged(object sender, EventArgs e)
        {
           // this.Text = this.Size.ToString();
        }

        private void connectionSettingsForm_Shown(object sender, EventArgs e)
        {
            if (cmbProtocol.Items.Count > 0)
                cmbProtocol.SelectedIndex = 0;           
        }

        private void cmbProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cmbProtocol.SelectedItem as string).Length != 0)
                ConnectionTypeSelectedHandler?.Invoke(cmbProtocol.SelectedItem as string);
        }

        public void SetControl(UserControl ctrl)
        {
            if (ctrl == null)
                return;
            SuspendLayout();
            panel.Controls.Clear();
            this.Width = widthOffset + ctrl.Width;
            this.Height = heightOffset + ctrl.Height;
            ctrl.Dock = DockStyle.Fill;
            panel.Controls.Add(ctrl);
            ResumeLayout();
        }

        private void connectionSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) e.Cancel = true;
        }
    }
}
