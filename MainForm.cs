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

        public Socket m_socWorker;

        public bool socketConnected = false;

        public TCPClientSettingForm tcpClientCfgForm;
        public DockableFormInfo dfi_tcpClientCfgForm;
        
        public DataGridViewSendForm dgvSendForm;
        public DockableFormInfo dfi_dgvSendForm;
        
        public RichTextBoxForm rtxtForm;
        public DockableFormInfo dfi_rtxtForm;
        
        
        public RuntimeProgramming rtPrg;
        
        public ProjectData projectData = new ProjectData();

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
            tcpClientCfgForm = new TCPClientSettingForm(_TcpConnect);
            
            dfi_tcpClientCfgForm = dc.Add(tcpClientCfgForm, zAllowedDock.All, tcpClientCfgForm.GetType().GUID);
            dfi_rtxtForm = dc.Add(rtxtForm, zAllowedDock.All, rtxtForm.GetType().GUID);
            dfi_dgvSendForm = dc.Add(dgvSendForm, zAllowedDock.All, dgvSendForm.GetType().GUID);

            

            dc.FormClosing += dc_FormClosing;
            
            Microsan.Debugger.Message = rtxtForm.rtxt.AppendText;

            


            //this.Text = ".NET version:" + Environment.Version;
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
            rtPrg = new RuntimeProgramming(this);
            rtPrg.InitScriptEditor_IfNeeded();
            LoadAndAppyProjectJson();

            rtxtForm.ShowIcon = false;
            dgvSendForm.ShowIcon = false;
            tcpClientCfgForm.ShowIcon = false;

        }

        private void LoadProjectJson()
        {
            try
            {
                string jsonStr = File.ReadAllText(JSON_PROJECT_FILENAME);
                projectData = JsonConvert.DeserializeObject<ProjectData>(jsonStr);
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

            tcpClientCfgForm.txtHostIP.Text = projectData.socket.ip;
            tcpClientCfgForm.txtHostPort.Text = projectData.socket.port.ToString();
            tcpClientCfgForm.txtMessageStartId.Text = projectData.socket.msgPrefix;
            tcpClientCfgForm.txtMessageStopId.Text = projectData.socket.msgPostfix;
            
            tcpClientCfgForm.MaximizeBox = false;
            rtxtForm.MaximizeBox = false;
            dgvSendForm.MaximizeBox = false;
            
            //tcpClientCfgForm.Height = 208;
            //rtxtForm.Width = tcpClientCfgForm.Width;
            
            dc.DockForm(dfi_dgvSendForm, DockStyle.Fill, zDockMode.Inner);
            
            dc.DockForm(dfi_tcpClientCfgForm, DockStyle.Left, zDockMode.None);
            dc.DockForm(dfi_rtxtForm, dfi_tcpClientCfgForm, DockStyle.Bottom, zDockMode.None);

            //dc.DockForm(dfi_rtxtForm, DockStyle.Left, zDockMode.Outer);
            //dc.DockForm(dfi_tcpClientCfgForm, dfi_rtxtForm, DockStyle.Top, zDockMode.Outer);

            //dc.GetFormsDecorator(dfi_tcpClientCfgForm).Height =  208;
            //dc.GetFormsDecorator(dfi_tcpClientCfgForm).SetFormsPanelBounds();


            projectData.window.main.ApplyTo(this);
            projectData.window.socket.ApplyTo(tcpClientCfgForm);
            projectData.window.log.ApplyTo(rtxtForm);
            projectData.window.dgvSend.ApplyTo(dgvSendForm);


            dgvSendForm.SetData(projectData.sendGroups);
        }

        private void SaveToProjectJson()
        {
            projectData.window.main.GetFrom(this);
            projectData.window.socket.GetFrom(tcpClientCfgForm);
            projectData.window.log.GetFrom(rtxtForm);
            projectData.window.dgvSend.GetFrom(dgvSendForm);

            projectData.Save(JSON_PROJECT_FILENAME);
        }
		
        /// <summary>
        /// local _TcpConnect 
        /// </summary>
        /// <param name="connectState">true if a connection should be estabilazed</param>
		private void _TcpConnect(bool connectState)
		{
            TcpConnect(connectState);
		}
		
        /// <summary>
        /// Public TcpConnect that have a return value
        /// </summary>
        /// <param name="connectState"></param>
        /// <returns>true if the connection was completed</returns>
		public bool TcpConnect(bool connectState)
		{
		    socketConnected = false;
		    
		    if (connectState)
		    {
                projectData.socket.ip = tcpClientCfgForm.txtHostIP.Text;
                projectData.socket.port = Convert.ToInt32(tcpClientCfgForm.txtHostPort.Text);
                projectData.socket.msgPrefix = tcpClientCfgForm.txtMessageStartId.Text;
                projectData.socket.msgPostfix = tcpClientCfgForm.txtMessageStopId.Text;
                
		      try
              {
                //create a new client socket ...
                m_socWorker = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

                System.Net.IPAddress    remoteIPAddress     = System.Net.IPAddress.Parse(projectData.socket.ip);
                System.Net.IPEndPoint    remoteEndPoint = new System.Net.IPEndPoint(remoteIPAddress, projectData.socket.port);
                m_socWorker.Connect(remoteEndPoint);
                
                if (m_socWorker.Connected)
                {
                    socketConnected = true;
                    bgwReceive.WorkerSupportsCancellation = true;
                    bgwReceive.RunWorkerAsync();
                }
              }
              catch (System.Net.Sockets.SocketException se)
              {
                rtxtForm.rtxt.AppendText(se.Message + "\n");
              }
		    }
		    else
		    {
		        try {
                bgwReceive.CancelAsync();
                }
		        catch( Exception ex)
		        {
		            rtxtForm.rtxt.AppendText(ex.Message + "\n");
		        }
                m_socWorker.Close ();
		    }
		    tcpClientCfgForm.SetConnectedState(socketConnected);
            return socketConnected;
		}
		
        /// <summary>
        /// the handler method for DataGridView SendForm
        /// </summary>
        /// <param name="data"></param>
		private void dgvSendForm_SendData(string data)
		{
            rtxtForm.rtxt.AppendText(LOG_TX_PREFIX + data + "\n");
            SendMessage(data);
		}
		
        /// <summary>
        /// before this form closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            socketConnected = false;
            if (m_socWorker != null)
            {
                m_socWorker.Close();
            }
            SaveToProjectJson();
        }

        /// <summary>
        /// sends a message as: messageStartId + message + messageStopId
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(string message)
		{
            message = projectData.socket.msgPrefix + message + projectData.socket.msgPostfix;
            
            if ((m_socWorker == null) || (m_socWorker.Connected == false))
		    {
		        if (!TcpConnect(true))
		        {
                    rtxtForm.rtxt.AppendText("was not able to connect @ SendRaw(" + message + ")");
                    return;
		        }
            }
		    
            message = message.Replace("\\n", "\n").Replace("\\r", "\r");
			try
			{
				Object objData = message;
				byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString ());
				m_socWorker.Send (byData);
			}
			catch(System.Net.Sockets.SocketException se)
			{
				rtxtForm.rtxt.AppendText(se.Message + "/n");
			}
		}

        /// <summary>
        /// the receive worker that runs in seperate thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwReceive_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            try
            {
                while (socketConnected && !worker.CancellationPending)
                {
                    byte[] buffer = new byte[1024];
                    int iRx = m_socWorker.Receive(buffer);
                    if (iRx <= 0) break; // graceful disconnect

                    string szData = Encoding.UTF8.GetString(buffer, 0, iRx);

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
            }
            catch (SocketException se)
            {
                // Ignore exceptions caused by shutdown
                if (worker.CancellationPending || !socketConnected)
                    return;

                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.BeginInvoke((MethodInvoker)(() =>
                    {
                        try
                        {
                            rtxtForm?.rtxt?.AppendText("\n" + se.Message + "\n");
                        }
                        catch { /* ignore */ }
                    }));
                }
            }
            catch (ObjectDisposedException)
            {
                // Expected when closing
            }
        }


        /// <summary>
        /// is called when the connection closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwReceive_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!rtxtForm.IsHandleCreated || rtxtForm.IsDisposed)
                return;

            try
            {
                rtxtForm.rtxt.AppendText("\nDisconnected\n");
            }
            catch (ObjectDisposedException)
            {
                // Form is gone; ignore.
            }
            catch (InvalidOperationException)
            {
                // Handle already destroyed; ignore.
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            socketConnected = false;
            if (bgwReceive.IsBusy)
                bgwReceive.CancelAsync();

            try
            {
                m_socWorker?.Shutdown(SocketShutdown.Both);
                m_socWorker?.Close();
            }
            catch { }
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
    }
}
