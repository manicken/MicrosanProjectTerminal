using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

#pragma warning disable IDE1006

namespace Microsan
{
    public partial class SerialSettingsControl : UserControl
    {
        private const string EMPTY_TEXTBOX_DEFAULT = "12345678";

        private readonly Action<bool> ConnectHandler;

        public static ConnectionSettingsControlBase GetConnectionSettingsControlBase()
        {
            return new ConnectionSettingsControlBase
            {
                Create = SerialSettingsControl.Create,
                ApplySettings = SerialSettingsControl.ApplySettings,
                RetrieveSettings = SerialSettingsControl.RetrieveSettings,
                SetConnectedState = SerialSettingsControl.SetConnectedState
            };
        }
        public static ConnectionSettingsBase GetNewConfigData()
        {
            return new SerialSettings();
        }
        public static UserControl Create(Action<bool> ConnectCallback)
        {
            return new SerialSettingsControl(ConnectCallback);
        }

        public static void ApplySettings(UserControl context, ConnectionSettingsBase data)
        {
            if (data.Type != SerialConnection.TypeName) return;

            SerialSettingsControl ctrl = context as SerialSettingsControl;
            SerialSettings cfg = data as SerialSettings;
            ctrl.cmbPort.Text = cfg.PortName;
            ctrl.txtBaudRate.Text = cfg.BaudRate.ToString();
            ctrl.txtMessageStartId.Text = (cfg.msgPrefix == EMPTY_TEXTBOX_DEFAULT) ? "" : cfg.msgPrefix;
            ctrl.txtMessageStopId.Text = (cfg.msgPostfix == EMPTY_TEXTBOX_DEFAULT) ? "" : cfg.msgPostfix;
        }

        public static void RetrieveSettings(UserControl context, ConnectionSettingsBase data)
        {
            if (data.Type != SerialConnection.TypeName) return;

            SerialSettingsControl ctrl = context as SerialSettingsControl;
            SerialSettings cfg = data as SerialSettings;
            
            cfg.PortName = ctrl.cmbPort.Text;
            cfg.BaudRate = Convert.ToInt32(ctrl.txtBaudRate.Text);
            cfg.msgPrefix = ctrl.txtMessageStartId.Text;
            cfg.msgPostfix = ctrl.txtMessageStopId.Text;
            cfg.msgPrefix = (cfg.msgPrefix == EMPTY_TEXTBOX_DEFAULT) ? "" : cfg.msgPrefix;
            cfg.msgPostfix = (cfg.msgPostfix == EMPTY_TEXTBOX_DEFAULT) ? "" : cfg.msgPostfix;
        }

        public static void SetConnectedState(UserControl context, bool connected)
        {
            SerialSettingsControl ctrl = context as SerialSettingsControl;

            ctrl.btnConnect.Enabled = !connected;
            ctrl.btnDisconnect.Enabled = connected;

            ctrl.grpBoxPort.Enabled = !connected;
            ctrl.grpBoxMessageStartStop.Enabled = !connected;
        }

        public SerialSettingsControl(Action<bool> ConnectHandler)
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

            txtBaudRate.MouseDown += tb_MouseDown;
            txtBaudRate.MouseUp += tb_MouseUp;
            txtBaudRate.MouseMove += tb_MouseMove;

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

        private void btnRefreshPorts_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cmbPort.Items.Clear();
            cmbPort.Items.AddRange(ports);
        }

        private void btnConnect_Click_1(object sender, EventArgs e)
        {

        }
    }

    
}
