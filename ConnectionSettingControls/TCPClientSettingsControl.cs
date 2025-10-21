using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#pragma warning disable IDE1006

namespace Microsan
{
    public partial class TCPClientSettingsControl : UserControl
    {
        private const string EMPTY_TEXTBOX_DEFAULT = "12345678";
        private readonly Action<bool> ConnectHandler;

        public static ConnectionSettingsControlBase GetConnectionSettingsControlBase()
        {
            return new ConnectionSettingsControlBase
            {
                Create = TCPClientSettingsControl.Create,
                ApplySettings = TCPClientSettingsControl.ApplySettings,
                RetrieveSettings = TCPClientSettingsControl.RetrieveSettings,
                SetConnectedState = TCPClientSettingsControl.SetConnectedState
            };
        }

        public static UserControl Create(Action<bool> ConnectCallback)
        {
            return new TCPClientSettingsControl(ConnectCallback);
        }

        public static void ApplySettings(UserControl context, ConnectionSettingsBase data)
        {
            if (data.Type != TCPClientConnection.TypeName) return;

            TCPClientSettingsControl ctrl = context as TCPClientSettingsControl;
            TCPClientSettings cfg = data as TCPClientSettings;
            ctrl.txtHostIP.Text = cfg.Host;
            ctrl.txtHostPort.Text = cfg.Port.ToString();
            ctrl.txtMessageStartId.Text = (cfg.msgPrefix == EMPTY_TEXTBOX_DEFAULT)?"": cfg.msgPrefix;
            ctrl.txtMessageStopId.Text = (cfg.msgPostfix == EMPTY_TEXTBOX_DEFAULT) ? "" : cfg.msgPostfix;
        }

        public static void RetrieveSettings(UserControl context, ConnectionSettingsBase data)
        {
            if (data.Type != TCPClientConnection.TypeName) return;

            TCPClientSettingsControl ctrl = context as TCPClientSettingsControl;
            TCPClientSettings cfg = data as TCPClientSettings;

            cfg.Host = ctrl.txtHostIP.Text;
            cfg.Port = Convert.ToInt32(ctrl.txtHostPort.Text);
            cfg.msgPrefix = ctrl.txtMessageStartId.Text;
            cfg.msgPostfix = ctrl.txtMessageStopId.Text;
            cfg.msgPrefix = (cfg.msgPrefix == EMPTY_TEXTBOX_DEFAULT) ? "" : cfg.msgPrefix;
            cfg.msgPostfix = (cfg.msgPostfix == EMPTY_TEXTBOX_DEFAULT) ? "" : cfg.msgPostfix;
        }
        public static void SetConnectedState(UserControl context, bool connected)
        {
            TCPClientSettingsControl ctrl = context as TCPClientSettingsControl;

            ctrl.btnConnect.Enabled = !connected;
            ctrl.btnDisconnect.Enabled = connected;

            ctrl.grpBoxIpPort.Enabled = !connected;
            ctrl.grpBoxMessageStartStop.Enabled = !connected;
        }

        public TCPClientSettingsControl(Action<bool> ConnectHandler)
        {
            InitializeComponent();

            this.ConnectHandler = ConnectHandler;

            if (ConnectHandler == null)
            {
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = false;
            }
            btnConnect.Click += btnConnect_Click;
            btnDisconnect.Click += btnDisconnect_Click;

            txtMessageStartId.Text = "";
            txtMessageStopId.Text = "";

            txtMessageStartId.MouseDown += tb_MouseDown;
            txtMessageStartId.MouseUp += tb_MouseUp;
            txtMessageStartId.MouseMove += tb_MouseMove;
            
            txtMessageStopId.MouseDown += tb_MouseDown;
            txtMessageStopId.MouseUp += tb_MouseUp;
            txtMessageStopId.MouseMove += tb_MouseMove;

            txtHostIP.MouseDown += tb_MouseDown;
            txtHostIP.MouseUp += tb_MouseUp;
            txtHostIP.MouseMove += tb_MouseMove;

            txtHostPort.MouseDown += tb_MouseDown;
            txtHostPort.MouseUp += tb_MouseUp;
            txtHostPort.MouseMove += tb_MouseMove;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectHandler?.Invoke(true);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            ConnectHandler?.Invoke(false);
        }
        /// <summary>
        /// Fix for GetCharIndexFromPosition() not reaching end of text.
        /// </summary>
        private int GetSafeCharIndex(TextBox tb, Point pt)
        {
            int idx = tb.GetCharIndexFromPosition(pt);

            // If click is past the right edge of text, move to end
            int textWidth = TextRenderer.MeasureText(tb.Text, tb.Font).Width;
            if (pt.X > textWidth)
                idx = tb.TextLength;

            return idx;
        }

        private void tb_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Tag = GetSafeCharIndex(tb, e.Location);
        }

        private void tb_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            TextBox tb = (TextBox)sender;
            if (tb.Tag is int == false) return;

            int downIndex = (int)tb.Tag;
            int currIndex = GetSafeCharIndex(tb, e.Location);
            tb.SelectionStart = Math.Min(downIndex, currIndex);
            tb.SelectionLength = Math.Abs(downIndex - currIndex);
        }

        private void tb_MouseUp(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Tag is int == false) return;
            int downIndex = (int)tb.Tag;
            int currIndex = GetSafeCharIndex(tb, e.Location);
            if (downIndex == currIndex)
            {
                tb.SelectionStart = currIndex;
                tb.SelectionLength = 0;
            }
        }
    }

    
}
