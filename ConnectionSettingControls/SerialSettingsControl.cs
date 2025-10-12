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
        private readonly Action<bool> ConnectHandler;

        public const string TypeName = "Serial";
        public static ConnectionBase GetConnectionBase()
        {
            return new ConnectionBase
            {
                Create = SerialSettingsControl.Create,
                ApplySettings = SerialSettingsControl.ApplySettings,
                RetrieveSettings = SerialSettingsControl.RetrieveSettings,
                GetNewConfigData = SerialSettingsControl.GetNewConfigData

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
            if (data.Type != TypeName) return;

            SerialSettingsControl ctrl = context as SerialSettingsControl;
            SerialSettings cfg = data as SerialSettings;
            ctrl.cmbPort.Text = cfg.PortName;
            ctrl.txtBaudRate.Text = cfg.BaudRate.ToString();
            ctrl.txtMessageStartId.Text = cfg.msgPrefix;
            ctrl.txtMessageStopId.Text = cfg.msgPostfix;
        }

        public static void RetrieveSettings(UserControl context, ConnectionSettingsBase data)
        {
            if (data.Type != TypeName) return;

            SerialSettingsControl ctrl = context as SerialSettingsControl;
            SerialSettings cfg = data as SerialSettings;
            
            cfg.PortName = ctrl.cmbPort.Text;
            cfg.BaudRate = Convert.ToInt32(ctrl.txtBaudRate.Text);
            cfg.msgPrefix = ctrl.txtMessageStartId.Text;
            cfg.msgPostfix = ctrl.txtMessageStopId.Text;
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
            txtMessageStartId.MouseClick += txtMessageStartStopIds_MouseClick;
            txtMessageStopId.MouseClick += txtMessageStartStopIds_MouseClick;
            txtMessageStartId.Leave += txtMessageStartStopIds_Leave;
            txtMessageStopId.Leave += txtMessageStartStopIds_Leave;

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
        public void Show(string ip, string port, string startId, string stopId)
        {
            
            txtMessageStartId.Text = startId;
            txtMessageStopId.Text = stopId;
        }

        public void SetConnectedState(bool connected)
        {
            btnConnect.Enabled = !connected;
            btnDisconnect.Enabled = connected;

            grpBoxPort.Enabled = !connected;
            grpBoxMessageStartStop.Enabled = !connected;
        }

        private void txtMessageStartStopIds_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == "12345678")
            {
                txt.Text = "";
                txt.ForeColor = Color.Black;
            }
        }

        private void txtMessageStartStopIds_Leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == "")
            {
                txt.Text = "12345678";
                txt.ForeColor = Color.Gray;
            }
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
    }

    public class SerialSettings : RawProtocolSettingsBase
    {
        public string PortName { get; set; } = "COM1";
        public int BaudRate { get; set; } = 9600;

        public SerialSettings() { Type = "Serial"; }
    }
}
