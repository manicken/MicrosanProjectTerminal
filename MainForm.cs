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
using WeifenLuo.WinFormsUI.Docking;

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

         
        public RichTextBoxControl rtxtCtrl;
        
        
        public RuntimeProgramming rtPrg;
        
        public ProjectData projectData = new ProjectData();

        public ConnectionController connectionCtrl = new ConnectionController();

        private DockPanel dockPanel;

        private Dictionary<string,DataGridViewSendControlDockContent> currentDgvSenderDocs = new Dictionary<string,DataGridViewSendControlDockContent>();
        private RichTextBoxControlDockContent rtxtCtrlDockContent;
        private ConnectionSettingsControlDockContent connCfgCtrlDockContent;

        /// <summary>
        /// main form constructor
        /// </summary>
		public MainForm()
		{        
			InitializeComponent();

            jsonEditForm = new JSONEditorForm();
            jsonEditForm.Save = jsonEditForm_Save;

            this.FormClosing += this_FormClosing;

			rtxtCtrl = new RichTextBoxControl("Log");

            //dgvSendCtrl = new DataGridViewSendTabbedControl(dgvSendForm_SendData);
            //dgvSendCtrl2 = new DataGridViewSendControl(dgvSendForm_SendData);
            // dgvSendCtrl3 = new DataGridViewSendControl(dgvSendForm_SendData);
            //dgvSendCtrl4 = new DataGridViewSendControl(dgvSendForm_SendData);


            //dc = new Crom.Controls.Docking.DockContainer();
            //dc.FormClosing += dc_FormClosing;
            //dc.Dock = DockStyle.Fill;
            //dc.BackColor = System.Drawing.Color.Gray;
            //panelContent.Controls.Add(dc);

            

            Microsan.Debugger.Message = rtxtCtrl.rtxt.AppendText;

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
            ApplyProjectData(ApplyProjectDataMode.Reload);
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
                        rtxtCtrl?.rtxt?.AppendText(LOG_RX_PREFIX + szData + Environment.NewLine);
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
       // Form rtxtForm = new Form();
        //Form dgvSendForm = new Form();
        //Form connSettingForm = new Form();

        /// <summary>
        /// when the form is first shown 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void this_Shown(object sender, EventArgs e)
        {
            rtPrg = new RuntimeProgramming(this);
            rtPrg.SaveAll = rtProg_SaveAll;
            rtPrg.CompiledAndRunning = rtProg_CompiledAndRunning;
            rtPrg.InitScriptEditor_IfNeeded();
            LoadAndAppyProjectJson(LoadAndAppyProjectJsonMode.FirstLoad);

            GenerateIconMap();
            //listEmbeddedResources();
            //rtxtForm.rtxt.AppendText(ConnectionController.DiscoverConnectionsAsString());
        }

        private void listEmbeddedResources()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();

            foreach (var name in resourceNames)
                rtxtCtrl.rtxt.AppendText(name+"\n");
            
        }

        Dictionary<string, Image> iconMap = new Dictionary<string, Image>();

        private void GenerateIconMap()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            rtxtCtrl.AppendLine("\nCurrent available icons:");
            
            foreach (var resName in asm.GetManifestResourceNames())
            {
                if (resName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                {
                    using (var stream = asm.GetManifestResourceStream(resName))
                    {
                        if (stream != null)
                        {
                            Image img = Image.FromStream(stream);
                            // Use the short name as key (e.g., "start" instead of full path)
                            string key = Path.GetFileNameWithoutExtension(resName);
                            key = key.Substring(key.LastIndexOf(".")+1);
                            rtxtCtrl.AppendLine(key);
                            iconMap[key] = img;
                        }
                    }
                }
            }
        }
        
        
        private void rtProg_CompiledAndRunning()
        {
            SetCodeEntryShortcuts();
        }

        public void rtProg_SaveAll()
        {
            SaveToProjectJsonFile();
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
                RefreshOpenDocuments();
                //dgvSendCtrl.SetData(projectData.sendGroups);
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

        private enum ApplyProjectDataMode
        {
            FirstStart,
            Reload
        }
        private void ApplyProjectData(ApplyProjectDataMode mode = ApplyProjectDataMode.FirstStart)
        {
            ApplyProjectName();
            projectData.window.main.ApplyTo(this);
            projectData.window.codeEdit.ApplyTo(rtPrg.srcEditContainerForm);
            projectData.window.jsonEdit.ApplyTo(jsonEditForm);
            connectionCtrl.SetData(projectData.connections);

            if (mode == ApplyProjectDataMode.FirstStart)
            {
                InitDockerSystem();
            }
            RefreshOpenDocuments();
        }

        private void InitDockerSystem()
        {
            dockPanel = new DockPanel { Dock = DockStyle.Fill };
            dockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            //dockPanel.ShowDocumentIcon = true;
            panelContent.Controls.Add(dockPanel);


            if (string.IsNullOrEmpty(projectData.dockData))
            {
                dockPanel.DockLeftPortion = 350;
                rtxtCtrlDockContent = new RichTextBoxControlDockContent(rtxtCtrl);
                connCfgCtrlDockContent = new ConnectionSettingsControlDockContent(connectionCtrl.connectionSettingsCtrl);

                connCfgCtrlDockContent.Show(dockPanel, DockState.DockLeft);
                rtxtCtrlDockContent.Show(connCfgCtrlDockContent.Pane, DockAlignment.Bottom, 0.5);
            }
            else
            {
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(projectData.dockData)))
                {
                    dockPanel.LoadFromXml(ms, DeserializeDockContent);
                }
            }
        }

        private void AddDataGridViewSendControl(SendDataJsonItems sendDataGroup)
        {
            DataGridViewSendControl dgvSendCtrl = new DataGridViewSendControl(dgvSendForm_SendData);
            dgvSendCtrl.TabNameChanged = dgvSendCtrl_TabNameChanged;
            dgvSendCtrl.TabAdded = dgvSendCtrl_TabAdded;
            dgvSendCtrl.TabRemoved = dgvSendCtrl_TabRemoved;
            dgvSendCtrl.CheckIfNameExist = dgvSendCtrl_CheckIfNameExists;
            
            dgvSendCtrl.SetData(sendDataGroup);
            var dgvSendCtrlDockContent = new DataGridViewSendControlDockContent(dgvSendCtrl, sendDataGroup.Name);
            dgvSendCtrlDockContent.Show(dockPanel);
            // keep track of current 'documents'
            currentDgvSenderDocs[sendDataGroup.Name] = dgvSendCtrlDockContent;
        }

        private bool dgvSendCtrl_CheckIfNameExists(string name)
        {
            return projectData.SendGroupsContainsName(name);
        }

        private void RefreshOpenDocuments()
        {
            foreach (DataGridViewSendControlDockContent dgvSendCtrlDockCont in currentDgvSenderDocs.Values)
            {
                dgvSendCtrlDockCont.Close();
            }
            for (int i = 0; i < projectData.sendGroups.Count; i++)
            {
                AddDataGridViewSendControl(projectData.sendGroups[i]);
            }
        }

        private void dgvSendCtrl_TabNameChanged(string from, string to)
        {
            if (!currentDgvSenderDocs.TryGetValue(from, out var currDockCont))
                return;

            currentDgvSenderDocs.Remove(from);
            currDockCont.TabText = to;
            currentDgvSenderDocs[to] = currDockCont;
        }

        private void dgvSendCtrl_TabAdded(SendDataJsonItems sendDataGroup)
        {
            projectData.sendGroups.Add(sendDataGroup);
            AddDataGridViewSendControl(sendDataGroup);
        }

        private void dgvSendCtrl_TabRemoved(SendDataJsonItems sendDataGroup)
        {
            if (!currentDgvSenderDocs.TryGetValue(sendDataGroup.Name, out var currDockCont))
                return;
            currDockCont.Close();
            projectData.sendGroups.Remove(sendDataGroup);
            currentDgvSenderDocs.Remove(sendDataGroup.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>the string that was saved to the file</returns>
        private string SaveToProjectJsonFile()
        {
            if (ReadOnlyMode) return ""; // just ignore to avoid loosing data

            projectData.window.main.GetFrom(this);
            projectData.window.codeEdit.GetFrom(rtPrg.srcEditContainerForm);
            projectData.window.jsonEdit.GetFrom(jsonEditForm);
            projectData.dockData = GetDockData();

            string jsonStr = projectData.ToJsonString();
            File.WriteAllText(ProjectData.CurrentProjectFilePath, jsonStr);
            return jsonStr;
        }
		
        /// <summary>
        /// the handler method for DataGridView SendForm
        /// </summary>
        /// <param name="data"></param>
		private void dgvSendForm_SendData(string data)
		{
            rtxtCtrl.rtxt.AppendText(LOG_TX_PREFIX + data + "\n");
            connectionCtrl.SendToCurrentConnection(data);
		}
		
		private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveToProjectJsonFile();
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
            SaveToProjectJsonFile();
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
                jsonStr = SaveToProjectJsonFile();
            }

            jsonEditForm.Show(jsonStr);
            if (jsonEditorShownOnce == false) // have this guard to avoid it to reset when reopen in same instance
            {
                jsonEditorShownOnce = true;
                projectData.window.jsonEdit.ApplyTo(jsonEditForm);
            }
        }

        private void tsmiRuntimeProgrammingRunDefault_Click(object sender, EventArgs ea)
        {
            rtPrg.SetSourceFiles(projectData.sourceFiles);
            rtPrg.ExecuteDefaultCode();
            SetCodeEntryShortcuts();
        }

        private void SetCodeEntryShortcuts()
        {
            List<MethodExecShortcutEntry> entries = rtPrg.GenerateShortcutsFromCompiledAssembly();
            if (entries.Count != 0)
                tsmiCustomEntryPoints.DropDownItems.Clear();

            for (int i = 0; i < entries.Count; i++)
            {
                MethodExecShortcutEntry ue = entries[i];
                ToolStripMenuItem tsmi = new ToolStripMenuItem(ue.DisplayName);

                if (!string.IsNullOrEmpty(ue.IconName) && iconMap.TryGetValue(ue.IconName, out var img))
                {
                    tsmi.Image = img;
                    tsmi.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                }
                tsmi.Click += (s, e2) => { ue.Execute(); };
                tsmiCustomEntryPoints.DropDownItems.Add(tsmi);

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            rtxtCtrl.AppendLine("\nCurrently docked items:");
            foreach (IDockContent idc in dockPanel.Contents)
            {
                rtxtCtrl.AppendLine(idc.DockHandler.TabText);
            }
            
        }

        private string GetDockData()
        {
            using (var ms = new MemoryStream())
            {
                dockPanel.SaveAsXml(ms, Encoding.UTF8);
                // Convert to string if you want to keep it in memory
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        private IDockContent DeserializeDockContent(string persistString)
        {
            if (persistString.StartsWith(typeof(DataGridViewSendControlDockContent).FullName))
            {
                // Extract ID (e.g. "MyApp.DgvSendDockContent:ABC123")
                var parts = persistString.Split(':');
                string id = parts.Length > 1 ? parts[1] : Guid.NewGuid().ToString();

                // Try to reuse an existing instance, or create a new one dynamically
                if (currentDgvSenderDocs.TryGetValue(id, out var existing))
                    return existing;
                
                DataGridViewSendControl dgvSendCtrl = new DataGridViewSendControl(dgvSendForm_SendData);
                SendDataJsonItems sendGrp = projectData.SendGroupByName(id);
                dgvSendCtrl.SetData(sendGrp);//.sendGroups[i]);

                var newContent = new DataGridViewSendControlDockContent(dgvSendCtrl, sendGrp.Name);
                currentDgvSenderDocs[id] = newContent;
                return newContent;
            }

            // Add other types (static or dynamic)
            if (persistString == typeof(RichTextBoxControlDockContent).FullName)
            {
                if (rtxtCtrlDockContent == null)
                    rtxtCtrlDockContent = new RichTextBoxControlDockContent(rtxtCtrl);
                return rtxtCtrlDockContent;
            }

            if (persistString == typeof(ConnectionSettingsControlDockContent).FullName)
            {
                if (connCfgCtrlDockContent == null)
                    connCfgCtrlDockContent = new ConnectionSettingsControlDockContent(connectionCtrl.connectionSettingsCtrl);
                return connCfgCtrlDockContent;
            }

            return null;
        }

        
    }
}
