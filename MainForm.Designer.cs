/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-04-13
 * Time: 14:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Microsan
{
    /// <summary>
    /// Description of MainForm_Designer.
    /// </summary>
    partial class MainForm
    {
        private System.ComponentModel.BackgroundWorker bgwReceive;
        private System.ComponentModel.IContainer components;
        
         public Crom.Controls.Docking.DockContainer dc;
         
         /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.bgwReceive = new System.ComponentModel.BackgroundWorker();
            this.dc = new Crom.Controls.Docking.DockContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnShowCodeEditor = new System.Windows.Forms.ToolStripButton();
            this.tstxtProjectName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsBtnProjectNameApply = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bgwReceive
            // 
            this.bgwReceive.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwReceive_DoWork);
            this.bgwReceive.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwReceive_RunWorkerCompleted);
            // 
            // dc
            // 
            this.dc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dc.Location = new System.Drawing.Point(0, 27);
            this.dc.Name = "dc";
            this.dc.Size = new System.Drawing.Size(536, 479);
            this.dc.TabIndex = 13;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnShowCodeEditor,
            this.toolStripSeparator1,
            this.tsBtnProjectNameApply,
            this.tstxtProjectName,
            this.toolStripLabel1,
            this.tsbtnSave,
            this.tsbtnLoad});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(536, 25);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnShowCodeEditor
            // 
            this.tsbtnShowCodeEditor.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnShowCodeEditor.Image")));
            this.tsbtnShowCodeEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnShowCodeEditor.Name = "tsbtnShowCodeEditor";
            this.tsbtnShowCodeEditor.Size = new System.Drawing.Size(121, 22);
            this.tsbtnShowCodeEditor.Text = "Show Code Editor";
            this.tsbtnShowCodeEditor.Click += new System.EventHandler(this.tsbtnShowCodeEditor_Click);
            // 
            // tstxtProjectName
            // 
            this.tstxtProjectName.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tstxtProjectName.Name = "tstxtProjectName";
            this.tstxtProjectName.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(79, 22);
            this.toolStripLabel1.Text = "ProjectName:";
            // 
            // tsBtnProjectNameApply
            // 
            this.tsBtnProjectNameApply.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnProjectNameApply.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnProjectNameApply.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnProjectNameApply.Image")));
            this.tsBtnProjectNameApply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnProjectNameApply.Name = "tsBtnProjectNameApply";
            this.tsBtnProjectNameApply.Size = new System.Drawing.Size(42, 22);
            this.tsBtnProjectNameApply.Text = "Apply";
            this.tsBtnProjectNameApply.Click += new System.EventHandler(this.tsBtnProjectNameApply_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSave.Image")));
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(72, 22);
            this.tsbtnSave.Text = "save test";
            this.tsbtnSave.Click += new System.EventHandler(this.tsbtnSave_Click);
            // 
            // tsbtnLoad
            // 
            this.tsbtnLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnLoad.Image")));
            this.tsbtnLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnLoad.Name = "tsbtnLoad";
            this.tsbtnLoad.Size = new System.Drawing.Size(72, 22);
            this.tsbtnLoad.Text = "load test";
            this.tsbtnLoad.Click += new System.EventHandler(this.tsbtnLoad_Click);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(536, 506);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainForm";
            this.Text = "Simple TCP client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.this_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnShowCodeEditor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstxtProjectName;
        private System.Windows.Forms.ToolStripButton tsBtnProjectNameApply;
        private System.Windows.Forms.ToolStripButton tsbtnSave;
        private System.Windows.Forms.ToolStripButton tsbtnLoad;
    }
}
