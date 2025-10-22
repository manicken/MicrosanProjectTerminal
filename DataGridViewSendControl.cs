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

        public Action<string,string> TabNameChanged;

        public Action<SendDataJsonItems> TabAdded;

        public Action<SendDataJsonItems> TabRemoved;

        public Func<string, bool> CheckIfNameExist;

        /// <summary>
        /// This is only a stored local reference
        /// </summary>
        SendDataJsonItems sendItems;

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

            dgv.DataBindingComplete += dgv_DataBindingComplete;
            dgv.CellClick += dgv_CellClick;
            dgv.RowPrePaint += dgv_RowPrePaint;
            dgv.CellMouseDown += dgv_CellMouseDown;
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
        /// <param name="sendItems"></param>
        public void SetData(SendDataJsonItems sendItems)
        {
            this.sendItems = sendItems;

            SetDgvDataSourceSafe(this.sendItems.items);
        }

        private void SetDgvDataSourceSafe(BindingList<SendDataItem> items)
        {
            dgv.CellClick -= dgv_CellClick;

            dgv.DataSource = null;
            dgv.DataSource = items;
            btnColumn.DisplayIndex = 1;

            dgv.CellClick += dgv_CellClick;
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

        

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sendItems.items.Count == 0) return;
            if (e.ColumnIndex != 0) return;
            if (e.RowIndex == -1) return;

            string toSend = sendItems.items[e.RowIndex].Data;
            SendData(toSend);
        }

        private void addNewRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sendItems.items.Add(new SendDataItem());
        }

        private void insertNewRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dgv.CurrentCell?.RowIndex ?? 0;
            sendItems.items.Insert(index, new SendDataItem());
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
            if (dgv.CurrentCell == null) return;
            int index = dgv.CurrentCell.RowIndex;
            sendItems.items.RemoveAt(index);
        }

        private void editTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var projectMeta = new Dictionary<string, string>()
            {
                { "Name", sendItems.Name },
                { "Note", sendItems.Note }
            };
            var result = MultiInputDialog.Show("Edit Tab Name", projectMeta, (validateRes) => {
                string Name = validateRes["Name"];
                if (CheckIfNameExist?.Invoke(Name) ?? false)
                {
                    MessageBox.Show(Name + " allready exist\nPlease choose annother Name");
                    return false;
                }
                return true;
            });
            if (result != null)
            {
                string Name = result["Name"];
                
                string Note = result["Note"];
                TabNameChanged?.Invoke(sendItems.Name, Name);
                sendItems.Name = Name;
                sendItems.Note = Note;
                
            }
        }


        private void tsmiInsertDelimiterRow_Click(object sender, EventArgs e)
        {
            int index = dgv.CurrentCell?.RowIndex ?? 0;
            SendDataItem sdi = new SendDataItem();
            sdi.IsDelimiter = true;
            sendItems.items.Insert(index, sdi);
            //dgv.CurrentCell = dgv.Rows[index].Cells[1];
        }

        private void dgwContextMenu_Opening(object sender, CancelEventArgs e)
        {
            tsmiDeleteRow.Enabled = (dgv.CurrentCell != null);
     
            tsmiAddNewTab.Enabled = (TabAdded != null) && (CheckIfNameExist != null);
            tsmiRemoveTab.Enabled = (TabRemoved != null);
            tsmiEditTabName.Enabled = (TabNameChanged != null) && (CheckIfNameExist != null);

        }

        private void tsmiAddNewTab_Click(object sender, EventArgs e)
        {
            var projectMeta = new Dictionary<string, string>()
            {
                { "Name", "newTab" },
                { "Note", "" }
            };
            var result = MultiInputDialog.Show("Add New Tab", projectMeta, (validateRes) => {
                string Name = validateRes["Name"];
                if (CheckIfNameExist?.Invoke(Name) ?? false)
                {
                    MessageBox.Show(Name + " allready exist\nPlease choose annother Name");
                    return false;
                }
                return true;
            });
            if (result != null)
            {
                string Name = result["Name"];
                if (CheckIfNameExist?.Invoke(Name) ?? false)
                {
                    MessageBox.Show(Name + " allready exist\nPlease choose annother Name");
                    return;
                }
                string Note = result["Note"];
                SendDataJsonItems newTab = new SendDataJsonItems(Name, Note);
                TabAdded?.Invoke(newTab);
            }
        }

        private void tsmiRemoveTab_Click(object sender, EventArgs e)
        {

        }

        private void tsmiRemoveTabConfirm_Click(object sender, EventArgs e)
        {
            TabRemoved?.Invoke(sendItems);
        }
    }
    
}
