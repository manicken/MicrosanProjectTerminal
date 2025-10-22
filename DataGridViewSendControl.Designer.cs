/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-05-05
 * Time: 23:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Microsan
{
    partial class DataGridViewSendControl
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public System.Windows.Forms.DataGridView dgv;
        
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataGridViewSendControl));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.dgwContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertNewRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiInsertDelimiterRow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteRowConfirm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiEditTabName = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAddNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRemoveTab = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveTabConfirm = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.dgwContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.ContextMenuStrip = this.dgwContextMenu;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(434, 412);
            this.dgv.TabIndex = 0;
            // 
            // dgwContextMenu
            // 
            this.dgwContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewRowToolStripMenuItem,
            this.insertNewRowToolStripMenuItem,
            this.toolStripMenuItem2,
            this.tsmiInsertDelimiterRow,
            this.toolStripMenuItem1,
            this.tsmiDeleteRow,
            this.toolStripMenuItem3,
            this.tsmiEditTabName,
            this.toolStripMenuItem4,
            this.tsmiAddNewTab,
            this.toolStripMenuItem5,
            this.tsmiRemoveTab});
            this.dgwContextMenu.Name = "dgwContextMenu";
            this.dgwContextMenu.Size = new System.Drawing.Size(177, 188);
            this.dgwContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.dgwContextMenu_Opening);
            // 
            // addNewRowToolStripMenuItem
            // 
            this.addNewRowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addNewRowToolStripMenuItem.Image")));
            this.addNewRowToolStripMenuItem.Name = "addNewRowToolStripMenuItem";
            this.addNewRowToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.addNewRowToolStripMenuItem.Text = "Add new row";
            this.addNewRowToolStripMenuItem.Click += new System.EventHandler(this.addNewRowToolStripMenuItem_Click);
            // 
            // insertNewRowToolStripMenuItem
            // 
            this.insertNewRowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("insertNewRowToolStripMenuItem.Image")));
            this.insertNewRowToolStripMenuItem.Name = "insertNewRowToolStripMenuItem";
            this.insertNewRowToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.insertNewRowToolStripMenuItem.Text = "Insert new row";
            this.insertNewRowToolStripMenuItem.Click += new System.EventHandler(this.insertNewRowToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(173, 6);
            // 
            // tsmiInsertDelimiterRow
            // 
            this.tsmiInsertDelimiterRow.Name = "tsmiInsertDelimiterRow";
            this.tsmiInsertDelimiterRow.Size = new System.Drawing.Size(176, 22);
            this.tsmiInsertDelimiterRow.Text = "Insert delimiter row";
            this.tsmiInsertDelimiterRow.Click += new System.EventHandler(this.tsmiInsertDelimiterRow_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(173, 6);
            // 
            // tsmiDeleteRow
            // 
            this.tsmiDeleteRow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDeleteRowConfirm});
            this.tsmiDeleteRow.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDeleteRow.Image")));
            this.tsmiDeleteRow.Name = "tsmiDeleteRow";
            this.tsmiDeleteRow.Size = new System.Drawing.Size(176, 22);
            this.tsmiDeleteRow.Text = "Delete row";
            // 
            // tsmiDeleteRowConfirm
            // 
            this.tsmiDeleteRowConfirm.Name = "tsmiDeleteRowConfirm";
            this.tsmiDeleteRowConfirm.Size = new System.Drawing.Size(118, 22);
            this.tsmiDeleteRowConfirm.Text = "Confirm";
            this.tsmiDeleteRowConfirm.Click += new System.EventHandler(this.confirmDeleteRowToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(173, 6);
            // 
            // tsmiEditTabName
            // 
            this.tsmiEditTabName.Name = "tsmiEditTabName";
            this.tsmiEditTabName.Size = new System.Drawing.Size(176, 22);
            this.tsmiEditTabName.Text = "Edit Tab Name";
            this.tsmiEditTabName.Click += new System.EventHandler(this.editTextToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(173, 6);
            // 
            // tsmiAddNewTab
            // 
            this.tsmiAddNewTab.Name = "tsmiAddNewTab";
            this.tsmiAddNewTab.Size = new System.Drawing.Size(176, 22);
            this.tsmiAddNewTab.Text = "Add New Tab";
            this.tsmiAddNewTab.Click += new System.EventHandler(this.tsmiAddNewTab_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(173, 6);
            // 
            // tsmiRemoveTab
            // 
            this.tsmiRemoveTab.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRemoveTabConfirm});
            this.tsmiRemoveTab.Name = "tsmiRemoveTab";
            this.tsmiRemoveTab.Size = new System.Drawing.Size(176, 22);
            this.tsmiRemoveTab.Text = "Remove Tab";
            this.tsmiRemoveTab.Click += new System.EventHandler(this.tsmiRemoveTab_Click);
            // 
            // tsmiRemoveTabConfirm
            // 
            this.tsmiRemoveTabConfirm.Name = "tsmiRemoveTabConfirm";
            this.tsmiRemoveTabConfirm.Size = new System.Drawing.Size(180, 22);
            this.tsmiRemoveTabConfirm.Text = "Confirm";
            this.tsmiRemoveTabConfirm.Click += new System.EventHandler(this.tsmiRemoveTabConfirm_Click);
            // 
            // DataGridViewSendControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.dgv);
            this.Name = "DataGridViewSendControl";
            this.Size = new System.Drawing.Size(434, 412);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.dgwContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.ContextMenuStrip dgwContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addNewRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertNewRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteRow;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteRowConfirm;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmiInsertDelimiterRow;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditTabName;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddNewTab;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveTab;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveTabConfirm;
    }
}
