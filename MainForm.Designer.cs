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
            this.dc = new Crom.Controls.Docking.DockContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnReload = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnShowCodeEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnProjectNameEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnShowJsonEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dc
            // 
            this.dc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dc.BackColor = System.Drawing.Color.Gray;
            this.dc.Location = new System.Drawing.Point(0, 42);
            this.dc.Name = "dc";
            this.dc.Size = new System.Drawing.Size(549, 464);
            this.dc.TabIndex = 13;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.tsbtnSave,
            this.toolStripSeparator2,
            this.tsbtnReload,
            this.toolStripSeparator4,
            this.tsbtnShowCodeEditor,
            this.toolStripSeparator1,
            this.tsBtnProjectNameEdit,
            this.toolStripSeparator5,
            this.tsbtnShowJsonEditor});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(549, 36);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 36);
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSave.Image")));
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(36, 33);
            this.tsbtnSave.Text = "Save";
            this.tsbtnSave.Click += new System.EventHandler(this.tsbtnSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 36);
            // 
            // tsbtnReload
            // 
            this.tsbtnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnReload.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnReload.Image")));
            this.tsbtnReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnReload.Name = "tsbtnReload";
            this.tsbtnReload.Size = new System.Drawing.Size(36, 33);
            this.tsbtnReload.Text = "Reload dgv send data";
            this.tsbtnReload.Click += new System.EventHandler(this.tsbtnReload_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 36);
            // 
            // tsbtnShowCodeEditor
            // 
            this.tsbtnShowCodeEditor.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnShowCodeEditor.Image")));
            this.tsbtnShowCodeEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnShowCodeEditor.Name = "tsbtnShowCodeEditor";
            this.tsbtnShowCodeEditor.Size = new System.Drawing.Size(137, 33);
            this.tsbtnShowCodeEditor.Text = "Show Code Editor";
            this.tsbtnShowCodeEditor.Click += new System.EventHandler(this.tsbtnShowCodeEditor_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 36);
            // 
            // tsBtnProjectNameEdit
            // 
            this.tsBtnProjectNameEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnProjectNameEdit.Image")));
            this.tsBtnProjectNameEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnProjectNameEdit.Name = "tsBtnProjectNameEdit";
            this.tsBtnProjectNameEdit.Size = new System.Drawing.Size(154, 33);
            this.tsBtnProjectNameEdit.Text = "Change Projectname";
            this.tsBtnProjectNameEdit.Click += new System.EventHandler(this.tsBtnProjectNameEdit_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 36);
            // 
            // tsbtnShowJsonEditor
            // 
            this.tsbtnShowJsonEditor.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnShowJsonEditor.Image")));
            this.tsbtnShowJsonEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnShowJsonEditor.Name = "tsbtnShowJsonEditor";
            this.tsbtnShowJsonEditor.Size = new System.Drawing.Size(94, 33);
            this.tsbtnShowJsonEditor.Text = "Edit JSON";
            this.tsbtnShowJsonEditor.Click += new System.EventHandler(this.tsbtnShowJsonEditor_Click);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(549, 506);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dc);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MicrosanProjectTerminal";
            this.Shown += new System.EventHandler(this.this_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnShowCodeEditor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsBtnProjectNameEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbtnReload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbtnShowJsonEditor;
    }
}
