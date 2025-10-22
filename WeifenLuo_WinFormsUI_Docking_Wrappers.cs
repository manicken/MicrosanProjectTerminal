using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Microsan
{
    public class RichTextBoxControlDockContent : DockContent
    {
        public RichTextBoxControlDockContent(RichTextBoxControl rtxtCtrl, string title = "Log")
        {
            Text = title;
            rtxtCtrl.Dock = DockStyle.Fill;
            CloseButton = false;
            Controls.Add(rtxtCtrl);
        }
    }
    public class DataGridViewSendTabbedControlDockContent : DockContent
    {
        public DataGridViewSendTabbedControlDockContent(DataGridViewSendTabbedControl dgvSendCtrl,string title = "DGVT Sender")
        {
            Text = title;
            dgvSendCtrl.Dock = DockStyle.Fill;
            CloseButton = false;
            Controls.Add(dgvSendCtrl);
        }
    }
    public class DataGridViewSendControlDockContent : DockContent
    {
        public DataGridViewSendControl dgvSendCtrlRef;
        public DataGridViewSendControlDockContent(DataGridViewSendControl dgvSendCtrl, string title = "DGV Sender")
        {
            dgvSendCtrlRef = dgvSendCtrl;
            Text = title;
            dgvSendCtrl.Dock = DockStyle.Fill;
            CloseButton = false;
            Controls.Add(dgvSendCtrl);
        }
        protected override string GetPersistString()
        {
            // Include type name + unique ID
            return $"{GetType().FullName}:{Text}";
        }
    }

    public class ConnectionSettingsControlDockContent : DockContent
    {
        public ConnectionSettingsControlDockContent(ConnectionSettingsControl ctrl, string title = "Connection Settings")
        {
            Text = title;
            ctrl.Dock = DockStyle.Fill;
            CloseButton = false;
            Controls.Add(ctrl);
        }
    }
}