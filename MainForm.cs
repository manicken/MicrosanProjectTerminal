using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net.Sockets ;
using Microsan;
using Crom.Controls.Docking;
using Crom.Controls.TabbedDocument;
using System.Text;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace Microsan
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class MainForm : System.Windows.Forms.Form
	{
        private const string JSON_PROJECT_FILENAME = "MicrosanProjectTerminal.json";
        private const string LOG_TX_PREFIX = ">> ";
        private const string LOG_RX_PREFIX = "<< ";
        /// <summary> The main entry point for the application. </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new MainForm());
        }

        // Note here, the reason that all here is
        // public is that then they are accessable from
        // RuntimeProgramming

        public DataGridViewSendForm dgvSendForm;
        public DockableFormInfo dfi_dgvSendForm;
        
        public RichTextBoxForm rtxtForm;
        public DockableFormInfo dfi_rtxtForm;

        public DockableFormInfo dfi_connectionCfgForm;
        
        public RuntimeProgramming rtPrg;
        
        public ProjectData projectData = new ProjectData();

        public ConnectionController connectionCtrl = new ConnectionController();

        /// <summary>
        /// main form constructor
        /// </summary>
		public MainForm()
		{        
            //execName = System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath);
            //settingsFileName = execName + ".ini.xml";
            
			InitializeComponent();
			
			this.FormClosing += this_FormClosing;

			rtxtForm = new RichTextBoxForm("Log");
            
            dgvSendForm = new DataGridViewSendForm(dgvSendForm_SendData);
 
            dfi_rtxtForm = dc.Add(rtxtForm, zAllowedDock.All, rtxtForm.GetType().GUID);
            dfi_dgvSendForm = dc.Add(dgvSendForm, zAllowedDock.All, dgvSendForm.GetType().GUID);
           

            dc.FormClosing += dc_FormClosing;
            
            Microsan.Debugger.Message = rtxtForm.rtxt.AppendText;


            connectionCtrl.DataReceived += connectionCtrl_DataReceived;

            //this.Text = ".NET version:" + Environment.Version;
        }

        private void connectionCtrl_DataReceived(byte[] data)
        {
            string szData = Encoding.UTF8.GetString(data, 0, data.Length);

            if (this.IsHandleCreated && !this.IsDisposed)
            {
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    try
                    {
                        rtxtForm?.rtxt?.AppendText(LOG_RX_PREFIX + szData);
                    }
                    catch { /* ignore dispose race */ }
                }));
            }
        }

        /// <summary>
        /// called when any docked form closes, now it cancels all requests
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dc_FormClosing(object sender, DockableFormClosingEventArgs e)
        {
            e.Cancel = true;
        }
        
        /// <summary>
        /// when the form is first shown 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void this_Shown(object sender, EventArgs e)
        {
            dfi_connectionCfgForm = dc.Add(connectionCtrl.connectionSettingsForm, zAllowedDock.All, connectionCtrl.connectionSettingsForm.GetType().GUID);

            rtPrg = new RuntimeProgramming(this);
            rtPrg.InitScriptEditor_IfNeeded();
            LoadAndAppyProjectJson();

            connectionCtrl.connectionSettingsForm.Show();
        }

        private void LoadProjectJson()
        {
            try
            {
                string jsonStr = File.ReadAllText(JSON_PROJECT_FILENAME);
                projectData = JsonConvert.DeserializeObject<ProjectData>(jsonStr, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadDgvSenderDataOnly()
        {
            try
            {
                string jsonStr = File.ReadAllText(JSON_PROJECT_FILENAME);
                ProjectData pdTemp = JsonConvert.DeserializeObject<ProjectData>(jsonStr, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
                projectData.sendGroups = pdTemp.sendGroups;
                dgvSendForm.SetData(projectData.sendGroups);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        
        /// <summary>
        /// init all docked forms
        /// </summary>
		private void LoadAndAppyProjectJson()
		{
            if (File.Exists(JSON_PROJECT_FILENAME))
                LoadProjectJson();

            ApplyProjectName();

            rtxtForm.MaximizeBox = false;
            dgvSendForm.MaximizeBox = false;
            
            dc.DockForm(dfi_dgvSendForm, DockStyle.Fill, zDockMode.Inner);
            
            dc.DockForm(dfi_connectionCfgForm, DockStyle.Left, zDockMode.None);
            dc.DockForm(dfi_rtxtForm, dfi_connectionCfgForm, DockStyle.Bottom, zDockMode.None);
            
            //dc.DockForm(dfi_rtxtForm, DockStyle.Left, zDockMode.Outer);
            //dc.DockForm(dfi_tcpClientCfgForm, dfi_rtxtForm, DockStyle.Top, zDockMode.Outer);

            //dc.GetFormsDecorator(dfi_tcpClientCfgForm).Height =  208;
            //dc.GetFormsDecorator(dfi_tcpClientCfgForm).SetFormsPanelBounds();

            projectData.window.main.ApplyTo(this);
            
            projectData.window.connections.ApplyTo(connectionCtrl.connectionSettingsForm);
            projectData.window.log.ApplyTo(rtxtForm);
            projectData.window.dgvSend.ApplyTo(dgvSendForm);

            dgvSendForm.SetData(projectData.sendGroups);
            connectionCtrl.SetData(projectData.connections);
        }

        private void SaveToProjectJson()
        {
            projectData.window.main.GetFrom(this);
            projectData.window.connections.GetFrom(connectionCtrl.connectionSettingsForm);
            projectData.window.log.GetFrom(rtxtForm);
            projectData.window.dgvSend.GetFrom(dgvSendForm);

            projectData.Save(JSON_PROJECT_FILENAME);
        }
		
        /// <summary>
        /// the handler method for DataGridView SendForm
        /// </summary>
        /// <param name="data"></param>
		private void dgvSendForm_SendData(string data)
		{
            rtxtForm.rtxt.AppendText(LOG_TX_PREFIX + data + "\n");
            connectionCtrl.SendToCurrentConnection(data);
		}
		
        /// <summary>
        /// before this form closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveToProjectJson();
        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void tsbtnShowCodeEditor_Click(object sender, EventArgs e)
        {
            rtPrg.ShowScriptEditor();
        }

        private void tsBtnProjectNameEdit_Click(object sender, EventArgs e)
        {
            var projectMeta = new Dictionary<string, string>()
            {
                { "Project name", projectData.meta.projectName }
            };
            var result = MultiInputDialog.Show("Edit Project Name", projectMeta);
            if (result != null)
            {
                projectData.meta.projectName = result["Name"];
            }
            ApplyProjectName();
        }

        private void ApplyProjectName()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string exeNameWithoutExt = Path.GetFileNameWithoutExtension(exePath);
            if ( projectData.meta.projectName.Trim().Length != 0) 
                this.Text = $"{exeNameWithoutExt} - {projectData.meta.projectName}";
            else
                this.Text = $"{exeNameWithoutExt}";
        }

        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            SaveToProjectJson();
        }

        private void tsbtnReload_Click(object sender, EventArgs e)
        {
            LoadDgvSenderDataOnly();
        }
    }
}
