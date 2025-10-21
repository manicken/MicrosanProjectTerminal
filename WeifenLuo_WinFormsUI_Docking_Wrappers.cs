using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Microsan
{
    public class LogDockContent : DockContent
    {
        public LogDockContent(RichTextBoxControl rtxtCtrl)
        {
            Text = "Log";
            rtxtCtrl.Dock = DockStyle.Fill;
            Controls.Add(rtxtCtrl);
        }
    }
    public class DgvSendDockContent : DockContent
    {
        public DgvSendDockContent(DataGridViewSendControl dgvSendCtrl)
        {
            Text = "DGV Sender";
            dgvSendCtrl.Dock = DockStyle.Fill;
            Controls.Add(dgvSendCtrl);
        }
    }

    public class ConnSettingsDockContent : DockContent
    {
        public ConnSettingsDockContent(ConnectionSettingsControl ctrl)
        {
            Text = "Connection Settings";
            ctrl.Dock = DockStyle.Fill;
            Controls.Add(ctrl);
        }
    }
}