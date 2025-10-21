/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-05-05
 * Time: 23:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;
//using Crom.Controls.TabbedDocument;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualBasic;
using System.ComponentModel;
using WeifenLuo.WinFormsUI.Docking;

namespace Microsan
{
    /// <summary>
    /// Description of DataGridViewSendForm.
    /// </summary>
    public partial class DataGridViewSendControl : UserControl
    {
        const string DATA_COL_NAME = "Data";
        const string NOTE_COL_NAME = "Note";
        const string SEND_COL_NAME = "Action";
        const string SEND_COL_BTN_TEXT = "Send";
        const string IS_DELIMITER_COL_NAME = "IsDelimiter";

        private void DebugMessage(string msg) { if (Microsan.Debugger.Message != null) Microsan.Debugger.Message(msg + "\n"); }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public Action<string> SendData;

        /// <summary>
        /// This is only a stored local reference
        /// </summary>
        List<SendDataJsonItems> sendGroups;

        int currentSelectedTabIndex = 0;

        DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SendDataHandler"></param>
        public DataGridViewSendControl(Action<string> SendDataHandler)
        {
            InitializeComponent();

            SendData = SendDataHandler;

            btnColumn.Name = SEND_COL_NAME;
            btnColumn.HeaderText = SEND_COL_NAME;
            btnColumn.Text = SEND_COL_BTN_TEXT;
            btnColumn.UseColumnTextForButtonValue = true;
            dgv.Columns.Add(btnColumn);
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            //dgv.DataSourceChanged += dgv_DataSourceChanged;
            dgv.DataBindingComplete += dgv_DataBindingComplete;
            dgv.CellClick += dgv_CellClick;
            dgv.CellEnter += dgv_CellEnter;
            dgv.RowPrePaint += dgv_RowPrePaint;
            dgv.CellMouseDown += dgv_CellMouseDown;
            //dgv.EditingControlShowing += dgv_EditingControlShowing;
        }

        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgv.DataSource == null) return;

            if (dgv.Columns[DATA_COL_NAME] != null)
            {
                dgv.Columns[DATA_COL_NAME].MinimumWidth = 128;
                dgv.Columns[DATA_COL_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns[DATA_COL_NAME].SortMode = DataGridViewColumnSortMode.Programmatic;
                dgv.Columns[DATA_COL_NAME].DefaultCellStyle.Font = Fonts.CourierNew;
                dgv.Columns[DATA_COL_NAME].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[DATA_COL_NAME].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgv.Columns[SEND_COL_NAME] != null)
            {
                dgv.Columns[SEND_COL_NAME].Width = 48;
                dgv.Columns[SEND_COL_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns[SEND_COL_NAME].ReadOnly = true;
                dgv.Columns[SEND_COL_NAME].SortMode = DataGridViewColumnSortMode.Programmatic;
                dgv.Columns[SEND_COL_NAME].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[SEND_COL_NAME].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgv.Columns[NOTE_COL_NAME] != null)
            {
                dgv.Columns[NOTE_COL_NAME].MinimumWidth = 128;
                dgv.Columns[NOTE_COL_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns[NOTE_COL_NAME].SortMode = DataGridViewColumnSortMode.Programmatic;
                dgv.Columns[NOTE_COL_NAME].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[NOTE_COL_NAME].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgv.Columns[IS_DELIMITER_COL_NAME] != null)
            {
                dgv.Columns[IS_DELIMITER_COL_NAME].Visible = false;
            }
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var rowData = row.DataBoundItem as SendDataItem;
                if (rowData != null && rowData.IsDelimiter)
                {
                    int colIndex = dgv.Columns[SEND_COL_NAME].Index;
                    row.Cells[colIndex] = new DataGridViewTextBoxCell(); // replace button with plain cell
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendGroups"></param>
        public void SetData(List<SendDataJsonItems> sendGroups)
        {
            this.sendGroups = sendGroups;
            if (sendGroups.Count == 0)
            {
                sendGroups.Add(new SendDataJsonItems("New Group", "Rename this group to something meaningful"));
            }
            tabCtrl.TabPages.Clear();
            for (int i = 0;i<sendGroups.Count; i++)
            {
                tabCtrl.TabPages.Add(TabPageExt.Create(sendGroups[i].Name, sendGroups[i].Note));
            }
            if (sendGroups.Count != 0)
            {
                SetDgvDataSourceSafe(sendGroups[0].items);
            }
        }

        private void SetDgvDataSourceSafe(BindingList<SendDataItem> items)
        {
            dgv.CellClick -= dgv_CellClick;
            dgv.CellEnter -= dgv_CellEnter;

            dgv.DataSource = null;
            dgv.DataSource = items;
            btnColumn.DisplayIndex = 1;

            dgv.CellClick += dgv_CellClick;
            dgv.CellEnter += dgv_CellEnter;
        }

        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv.Rows[e.RowIndex].DataBoundItem is SendDataItem rowData)
            {
                if (rowData.IsDelimiter)
                {
                    dgv.Rows[e.RowIndex].Height = 8;
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                    dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black; // optional
                }
                else
                {
                    dgv.Rows[e.RowIndex].Height = 22;
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = dgv.DefaultCellStyle.BackColor;
                    dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = dgv.DefaultCellStyle.ForeColor;
                }
            }
        }

        private void tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = tabCtrl.SelectedIndex;
            if (idx < 0 || idx >= sendGroups.Count) return;

            currentSelectedTabIndex = idx;

            SetDgvDataSourceSafe(sendGroups[idx].items);
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sendGroups[currentSelectedTabIndex].items.Count == 0) return;
            if (e.ColumnIndex != 0) return;
            if (e.RowIndex == -1) return;

            string toSend = sendGroups[currentSelectedTabIndex].items[e.RowIndex].Data;
            SendData(toSend);
        }
        
        private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Visible = false;
                e.Cancel = true;
            }
        }
        
        private void dgv_DataSourceChanged(object sender, EventArgs e)
        {
            
        }

        void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgv.BeginEdit(false);
            if (dgv.EditingControl != null)
            {
                TextBox tb = (TextBox)dgv.EditingControl;
                int currIndex = tb.GetCharIndexFromPosition(tb.PointToClient(MousePosition));
                tb.SelectionStart = currIndex;
                tb.SelectionLength = 0;
            }
        }

        void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = (TextBox)e.Control;
            if (tb.IsHandleCreated)
                return;
            tb.MouseUp += tb_MouseUp;
            tb.MouseDown += tb_MouseDown;
            tb.MouseMove += tb_MouseMove;
        }

        /// <summary>
        /// Fix for GetCharIndexFromPosition() not reaching end of text.
        /// </summary>
        private int GetSafeCharIndex(TextBox tb, Point pt)
        {
            int idx = tb.GetCharIndexFromPosition(pt);

            // If click is past the right edge of text, move to end
           /* int textWidth = TextRenderer.MeasureText(tb.Text, tb.Font).Width;
            if (pt.X > textWidth)
                idx = tb.TextLength;*/
           // int idx = tb.GetCharIndexFromPosition(pt);
            if (idx < 0) idx = 0;
            if (idx > tb.TextLength) idx = tb.TextLength;
            return idx;

            //return idx;
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

        private void addNewRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sendGroups[currentSelectedTabIndex].items.Add(new SendDataItem());
        }

        private void insertNewRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dgv.CurrentCell?.RowIndex ?? 0;
            sendGroups[currentSelectedTabIndex].items.Insert(index, new SendDataItem());
            dgv.CurrentCell = dgv.Rows[index].Cells[1];
        }

        private void dgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            // Ignore clicks outside of valid cells (like column headers)
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                dgv.CurrentCell = null;
                return;
            }

            dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            
        }

        private void confirmDeleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dgv.CurrentCell?.RowIndex ?? 0;
            sendGroups[currentSelectedTabIndex].items.RemoveAt(index);
        }

        private void tabsContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // the following selects a tab directly when right clicked
            Point pos = tabCtrl.PointToClient(Cursor.Position);
            for (int i = 0; i < tabCtrl.TabCount; i++)
            {
                if (tabCtrl.GetTabRect(i).Contains(pos))
                {
                    tabCtrl.SelectedIndex = i;
                    break;
                }
            }

            int idx = tabCtrl.SelectedIndex;
            tsmiMoveTabToLeft.Enabled = (idx > 0);
            tsmiMoveTabToRight.Enabled = (idx >= 0 && idx < tabCtrl.TabPages.Count - 1);
        }

        private void editTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var projectMeta = new Dictionary<string, string>()
            {
                { "Name", tabCtrl.SelectedTab.Text },
                { "Note", tabCtrl.SelectedTab.ToolTipText }
            };
            var result = MultiInputDialog.Show("Edit Tab Name", projectMeta);
            if (result != null)
            {
                string Name = result["Name"];
                string Note = result["Note"];
                tabCtrl.SelectedTab.Text = Name;
                tabCtrl.SelectedTab.ToolTipText = Note;
                sendGroups[currentSelectedTabIndex].Name = Name;
                sendGroups[currentSelectedTabIndex].Note = Note;
            }
        }

        private void addTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var projectMeta = new Dictionary<string, string>()
            {
                { "Name", tabCtrl.SelectedTab.Text },
                { "Note", tabCtrl.SelectedTab.ToolTipText }
            };
            var result = MultiInputDialog.Show("Add New Tab", projectMeta);
            if (result != null)
            {
                string Name = result["Name"];
                string Note = result["Note"];
                tabCtrl.TabPages.Add(TabPageExt.Create(Name, Note));
                sendGroups.Add(new SendDataJsonItems(Name, Note));
            }
        }

        private void toLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idx = tabCtrl.SelectedIndex;
            if (idx > 0)
            {
                tabCtrl.SelectedIndexChanged -= tabCtrl_SelectedIndexChanged;
                var tp = tabCtrl.TabPages[idx];
                tabCtrl.TabPages.RemoveAt(idx);
                tabCtrl.TabPages.Insert(idx - 1, tp);
                tabCtrl.SelectedIndex = idx - 1; // keep it selected
                var sendGroupsItem = sendGroups[idx];
                sendGroups.RemoveAt(idx);
                sendGroups.Insert(idx - 1, sendGroupsItem);
                tabCtrl.SelectedIndexChanged += tabCtrl_SelectedIndexChanged;
            }
        }

        private void tsmiMoveTabToRight_Click(object sender, EventArgs e)
        {
            int idx = tabCtrl.SelectedIndex;
            if (idx >= 0 && idx < tabCtrl.TabPages.Count - 1)
            {
                tabCtrl.SelectedIndexChanged -= tabCtrl_SelectedIndexChanged;
                var tp = tabCtrl.TabPages[idx];
                tabCtrl.TabPages.RemoveAt(idx);
                tabCtrl.TabPages.Insert(idx + 1, tp);
                tabCtrl.SelectedIndex = idx + 1; // keep it selected
                var sendGroupsItem = sendGroups[idx];
                sendGroups.RemoveAt(idx);
                sendGroups.Insert(idx + 1, sendGroupsItem);
                tabCtrl.SelectedIndexChanged += tabCtrl_SelectedIndexChanged;
            }
        }

        private void tsmiRemoveTabConfirm_Click(object sender, EventArgs e)
        {
            int idx = tabCtrl.SelectedIndex;
            tabCtrl.TabPages.RemoveAt(idx);
            sendGroups.RemoveAt(idx);
        }

        private void tsmiInsertDelimiterRow_Click(object sender, EventArgs e)
        {
            int index = dgv.CurrentCell?.RowIndex ?? 0;
            SendDataItem sdi = new SendDataItem();
            sdi.IsDelimiter = true;
            sendGroups[currentSelectedTabIndex].items.Insert(index, sdi);
            //dgv.CurrentCell = dgv.Rows[index].Cells[1];
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public static class TabPageExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="tooltip"></param>
        /// <returns></returns>
        public static TabPage Create(string title, string tooltip = null)
        {
            var tp = new TabPage(title);
            if (tooltip != null)
                tp.ToolTipText = tooltip;
            return tp;
        }
    }
}
