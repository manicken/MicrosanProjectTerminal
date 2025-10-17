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
        private const string DEFAULT_JSON_PROJECT_FILENAME = "MicrosanProjectTerminal.json";
        
        /** used to signal load errors so that the project is not saved back to a empty one */
        private bool ReadOnlyMode = false; 
        private const string LOG_TX_PREFIX = ">> ";
        private const string LOG_RX_PREFIX = "<< ";
        /// <summary> The main entry point for the application. </summary>
        [STAThread]
        static void Main(string[] args) 
        {
            
            if (args.Length > 0 && File.Exists(args[0]))
            {
                ProjectData.CurrentProjectFilePath = args[0];
            }
            else
            {
                ProjectData.CurrentProjectFilePath = DEFAULT_JSON_PROJECT_FILENAME;
            }
            Application.Run(new MainForm());
        }

        // Note here, the reason that all here is
        // public is that then they are accessable from
        // RuntimeProgramming

        public JSONEditorForm jsonEditForm;

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
			InitializeComponent();

            jsonEditForm = new JSONEditorForm();
            jsonEditForm.Save = jsonEditForm_Save;

            this.FormClosing += this_FormClosing;

			rtxtForm = new RichTextBoxForm("Log");
            
            dgvSendForm = new DataGridViewSendForm(dgvSendForm_SendData);
 
            

            dc.FormClosing += dc_FormClosing;
            
            Microsan.Debugger.Message = rtxtForm.rtxt.AppendText;

            connectionCtrl.DataReceived += connectionCtrl_DataReceived;
        }
        /// <summary>
        /// note the following gets only called when the json is valid
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="pdTemp"></param>
        private void jsonEditForm_Save(string contents, ProjectData pdTemp)
        {
            File.WriteAllText(ProjectData.CurrentProjectFilePath, contents);
            projectData = pdTemp;
            ReadOnlyMode = false;
            tsbtnReload.Enabled = true;
            tsbtnSave.Enabled = true;
            ApplyProjectData();
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
                        rtxtForm?.rtxt?.AppendText(LOG_RX_PREFIX + szData + Environment.NewLine);
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
            dfi_rtxtForm = dc.Add(rtxtForm, zAllowedDock.All, rtxtForm.GetType().GUID);
            dfi_dgvSendForm = dc.Add(dgvSendForm, zAllowedDock.All, dgvSendForm.GetType().GUID);
            dfi_connectionCfgForm = dc.Add(connectionCtrl.connectionSettingsForm, zAllowedDock.All, connectionCtrl.connectionSettingsForm.GetType().GUID);

            rtPrg = new RuntimeProgramming(this);
            rtPrg.InitScriptEditor_IfNeeded();
            LoadAndAppyProjectJson(LoadAndAppyProjectJsonMode.FirstLoad);

            // have the following here,
            // as currently restoring the dock cfg from json is not possible
            dc.DockForm(dfi_dgvSendForm, DockStyle.Fill, zDockMode.Inner);
            dc.DockForm(dfi_connectionCfgForm, DockStyle.Left, zDockMode.None);
            dc.DockForm(dfi_rtxtForm, dfi_connectionCfgForm, DockStyle.Bottom, zDockMode.None);

            rtxtForm.MaximizeBox = false;
            dgvSendForm.MaximizeBox = false;

            connectionCtrl.connectionSettingsForm.Show();

            //rtxtForm.rtxt.AppendText(ConnectionController.DiscoverConnectionsAsString());
        }

        public enum LoadProjectStatus
        {
            Success,
            FileNotFound,
            FileIOError,
            JsonSerializeError
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdTemp">Note this allways returns a valid non null object</param>
        /// <param name="onError"></param>
        /// <returns></returns>
        private LoadProjectStatus LoadProjectJson(out ProjectData pdTemp, Action<string> onError)
        {
            if (File.Exists(ProjectData.CurrentProjectFilePath) == false)
            {
                pdTemp = new ProjectData();
                return LoadProjectStatus.FileNotFound; 
            }
            string jsonStr = "";
            
            try
            {
                jsonStr = File.ReadAllText(ProjectData.CurrentProjectFilePath);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex.ToString());
                pdTemp = new ProjectData();
                return LoadProjectStatus.FileIOError;
            }
            LoadProjectStatus res = LoadProjectStatus.Success;
            pdTemp = ProjectData.LoadFromJsonString(jsonStr, (errorList) => {
                res = LoadProjectStatus.JsonSerializeError;
                jsonEditForm.Show(jsonStr, errorList);
            });
            if (pdTemp == null)
                pdTemp = new ProjectData();
            return res;
        }

        private bool LoadDgvSenderDataOnly()
        {
            ProjectData pdTemp = null;
            LoadProjectStatus res = LoadProjectJson(out pdTemp, (errStr) => {
                DebugLogForm.ShowOnceWithMessageAndAckButton(errStr, "continue in readonle mode");
            });
            if (res != LoadProjectStatus.Success)
            {
                ReadOnlyMode = true;
                tsbtnSave.Enabled = false;
            }
            else
            {
                ReadOnlyMode = false;
                tsbtnSave.Enabled = true;
            }

            ApplyProjectName(); // this updates the title to include the read only note
            if (res == LoadProjectStatus.Success)
            {
                projectData.sendGroups = pdTemp.sendGroups;
                dgvSendForm.SetData(projectData.sendGroups);
            }
            return (res == LoadProjectStatus.Success);
        }

        private enum LoadAndAppyProjectJsonMode
        {
            FirstLoad,
            Reload
        }
        /// <summary>
        /// init all docked forms
        /// </summary>
        private void LoadAndAppyProjectJson(LoadAndAppyProjectJsonMode mode)
		{
            ProjectData pdTemp;
            LoadProjectStatus res = LoadProjectJson(out pdTemp, (errStr) => {
                DebugLogForm.ShowOnceWithMessageAndAckButton(errStr, "open in readonly mode");
            });

            // A missing project file is not an error — we initialize a new empty project instead
            bool validLoad = (res == LoadProjectStatus.Success) || (res == LoadProjectStatus.FileNotFound);

            if (!validLoad)
            {
                ReadOnlyMode = true;
                tsbtnSave.Enabled = false;
                tsbtnReload.Enabled = false;
            }
            else
            {
                ReadOnlyMode = false;
                tsbtnSave.Enabled = true;
                tsbtnReload.Enabled = true;
            }

            if (mode == LoadAndAppyProjectJsonMode.FirstLoad || validLoad)
            {
                projectData = pdTemp;
            }

            ApplyProjectData();
        }

        private void ApplyProjectData()
        {
            ApplyProjectName();
            projectData.window.main.ApplyTo(this);
            projectData.window.codeEdit.ApplyTo(rtPrg.srcEditContainerForm);
            projectData.window.jsonEdit.ApplyTo(jsonEditForm);
            projectData.window.connections.ApplyTo(connectionCtrl.connectionSettingsForm);
            projectData.window.log.ApplyTo(rtxtForm);
            projectData.window.dgvSend.ApplyTo(dgvSendForm);
            dgvSendForm.SetData(projectData.sendGroups);
            connectionCtrl.SetData(projectData.connections);
        }

        private void SaveToProjectJson()
        {
            if (ReadOnlyMode) return; // just ignore to avoid loosing data

            projectData.window.main.GetFrom(this);
            projectData.window.codeEdit.GetFrom(rtPrg.srcEditContainerForm);
            projectData.window.jsonEdit.GetFrom(jsonEditForm);
            projectData.window.connections.GetFrom(connectionCtrl.connectionSettingsForm);
            projectData.window.log.GetFrom(rtxtForm);
            projectData.window.dgvSend.GetFrom(dgvSendForm);

            string jsonStr = projectData.ToJsonString();
            File.WriteAllText(ProjectData.CurrentProjectFilePath, jsonStr);
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
		
		private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveToProjectJson();
        }
        bool codeEditorHasBeenShown = false;
        private void tsbtnShowCodeEditor_Click(object sender, EventArgs e)
        {
            rtPrg.ShowScriptEditor(projectData.sourceFiles);
            if (codeEditorHasBeenShown == false) // have this guard to avoid it to reset when reopen in same instance
            {
                codeEditorHasBeenShown = true;
                projectData.window.codeEdit.ApplyTo(rtPrg.srcEditContainerForm);
            }
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
            string newTitle = exeNameWithoutExt;
            if ( projectData.meta.projectName.Trim().Length != 0)
                newTitle += $" - {projectData.meta.projectName}";

            if (ReadOnlyMode) newTitle += " (*** READ ONLY MODE ***)";

            this.Text = newTitle;
        }

        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            SaveToProjectJson();
        }

        private void tsbtnReload_Click(object sender, EventArgs e)
        {
            LoadDgvSenderDataOnly();
        }

        bool jsonEditorShownOnce = false;
        private void tsbtnShowJsonEditor_Click(object sender, EventArgs e)
        {
            string jsonStr = "";
            if (ReadOnlyMode)
            {
                jsonStr = File.ReadAllText(ProjectData.CurrentProjectFilePath);
            } 
            else // allways get from current state when in normal mode
            {
                jsonStr = projectData.ToJsonString();
            }

            jsonEditForm.Show(jsonStr);
            if (jsonEditorShownOnce == false) // have this guard to avoid it to reset when reopen in same instance
            {
                jsonEditorShownOnce = true;
                projectData.window.jsonEdit.ApplyTo(jsonEditForm);
            }
        }
    }
}
