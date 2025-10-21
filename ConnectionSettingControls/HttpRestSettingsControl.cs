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
    public partial class HttpRestSettingsControl : UserControl
    {
        private const string EMPTY_TEXTBOX_DEFAULT = "12345678";
        private readonly Action<bool> ConnectHandler;

        public static ConnectionSettingsControlBase GetConnectionSettingsControlBase()
        {
            return new ConnectionSettingsControlBase
            {
                Create = HttpRestSettingsControl.Create,
                ApplySettings = HttpRestSettingsControl.ApplySettings,
                RetrieveSettings = HttpRestSettingsControl.RetrieveSettings,
                SetConnectedState = HttpRestSettingsControl.SetConnectedState
            };
        }

        public static UserControl Create(Action<bool> ConnectCallback)
        {
            return new HttpRestSettingsControl(ConnectCallback);
        }

        public static void ApplySettings(UserControl context, ConnectionSettingsBase data)
        {
            if (data.Type != HttpRestClientConnection.TypeName) return;

            HttpRestSettingsControl ctrl = context as HttpRestSettingsControl;
            HttpRestSettings cfg = data as HttpRestSettings;
            ctrl.txtUri.Text = cfg.Uri;
            ctrl.chkUseSecure.Checked = cfg.UseSecure;
            ctrl.txtMessageStartId.Text = (cfg.msgPrefix == EMPTY_TEXTBOX_DEFAULT)?"": cfg.msgPrefix;
            ctrl.txtMessageStopId.Text = (cfg.msgPostfix == EMPTY_TEXTBOX_DEFAULT) ? "" : cfg.msgPostfix;
        }

        public static void RetrieveSettings(UserControl context, ConnectionSettingsBase data)
        {
            if (data.Type != HttpRestClientConnection.TypeName) return;

            HttpRestSettingsControl ctrl = context as HttpRestSettingsControl;
            HttpRestSettings cfg = data as HttpRestSettings;

            cfg.Uri = ctrl.txtUri.Text;
            cfg.UseSecure = ctrl.chkUseSecure.Checked;
            cfg.msgPrefix = ctrl.txtMessageStartId.Text;
            cfg.msgPostfix = ctrl.txtMessageStopId.Text;
            cfg.msgPrefix = (cfg.msgPrefix == EMPTY_TEXTBOX_DEFAULT) ? "" : cfg.msgPrefix;
            cfg.msgPostfix = (cfg.msgPostfix == EMPTY_TEXTBOX_DEFAULT) ? "" : cfg.msgPostfix;
        }
        public static void SetConnectedState(UserControl context, bool connected)
        {
            HttpRestSettingsControl ctrl = context as HttpRestSettingsControl;

            ctrl.grpSecure.Enabled = !connected;
            ctrl.grpBoxUri.Enabled = !connected;
            ctrl.grpBoxMessageStartStop.Enabled = !connected;
        }

        public HttpRestSettingsControl(Action<bool> ConnectHandler)
        {
            InitializeComponent();

            txtMessageStartId.Text = "";
            txtMessageStopId.Text = "";

            txtMessageStartId.MouseDown += tb_MouseDown;
            txtMessageStartId.MouseUp += tb_MouseUp;
            txtMessageStartId.MouseMove += tb_MouseMove;
            
            txtMessageStopId.MouseDown += tb_MouseDown;
            txtMessageStopId.MouseUp += tb_MouseUp;
            txtMessageStopId.MouseMove += tb_MouseMove;

            txtUri.MouseDown += tb_MouseDown;
            txtUri.MouseUp += tb_MouseUp;
            txtUri.MouseMove += tb_MouseMove;

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
