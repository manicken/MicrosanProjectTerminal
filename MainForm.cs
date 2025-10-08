using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net.Sockets ;
using VAkos;
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
        private const string LOG_TX_PREFIX = ">> ";
        private const string LOG_RX_PREFIX = "<< ";
        /// <summary> The main entry point for the application. </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new MainForm());
        }

        public Socket m_socWorker;

        public bool socketConnected = false;

        private string projectName = "";
        
        //public IniFile iniSettingFile;
        public Xmlconfig xmlConfig;
        
        public TCPClientSettingForm tcpClientCfgForm;
        public DockableFormInfo dfi_tcpClientCfgForm;
        
        public DataGridViewSendForm dgvSendForm;
        public DockableFormInfo dfi_dgvSendForm;
        
        public RichTextBoxForm rtxtForm;
        public DockableFormInfo dfi_rtxtForm;
        
        
        public RuntimeProgramming rtPrg;
        
        /// <summary> current host ip </summary>
        public string hostIp = "";
        /// <summary> current host port </summary>
        public string hostPort = "";
        /// <summary> current messageStartId </summary>
        public string messageStartId = "";
        /// <summary> current messageStopId </summary>
        public string messageStopId = "";
        
        public string execName;
        public string settingsFileName;

        public MainRootDataStructures mainRootDataStructures = new MainRootDataStructures();

        /// <summary>
        /// main form constructor
        /// </summary>
		public MainForm()
		{        
            execName = System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath);
            settingsFileName = execName + ".ini.xml";
            
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
            InitAllStuff();
            
            //rtPrg.ShowScriptEditor();
        }
        
        /// <summary>
        /// Read the program settings file
        /// </summary>
        public void ReadSettingsFile()
        {
            if (!System.IO.File.Exists(settingsFileName))
                return;

            xmlConfig = new Xmlconfig(settingsFileName, true);

            projectName = xmlConfig.Settings["ProjectName"].Value;
            ApplyProjectName();
            tstxtProjectName.Text = projectName;
            
            hostIp = xmlConfig.Settings["TcpClient/Ip"].Value;
            hostPort = xmlConfig.Settings["TcpClient/Port"].Value;
            messageStartId = xmlConfig.Settings["TcpClient/MessageStart"].Value;
            messageStopId = xmlConfig.Settings["TcpClient/MessageStop"].Value;
            
            this.Width = xmlConfig.Settings["MainForm/Width"].intValue;
            this.Height = xmlConfig.Settings["MainForm/Height"].intValue;
            
            dgvSendForm.Width = xmlConfig.Settings["DataGridViewSendForm/Width"].intValue;
            dgvSendForm.Height = xmlConfig.Settings["DataGridViewSendForm/Height"].intValue;
            
            
            int test = xmlConfig.Settings["DataGridViewSendForm/Top"].intValue;
            if (test != 0)
            {
                dgvSendForm.Top = xmlConfig.Settings["DataGridViewSendForm/Top"].intValue;
                dgvSendForm.Left = xmlConfig.Settings["DataGridViewSendForm/Left"].intValue;
            }

            test = xmlConfig.Settings["rtxtForm/Width"].intValue;
            if (test != 0)
            {
                rtxtForm.Width = xmlConfig.Settings["rtxtForm/Width"].intValue;
                rtxtForm.Height = xmlConfig.Settings["rtxtForm/Height"].intValue;
                rtxtForm.Top = xmlConfig.Settings["rtxtForm/Top"].intValue;
                rtxtForm.Left = xmlConfig.Settings["rtxtForm/Left"].intValue;
            }

            test = xmlConfig.Settings["TCPClientSettingForm/Width"].intValue;
            if (test != 0)
            {
                tcpClientCfgForm.Width = xmlConfig.Settings["TCPClientSettingForm/Width"].intValue;
                tcpClientCfgForm.Height = xmlConfig.Settings["TCPClientSettingForm/Height"].intValue;
                tcpClientCfgForm.Top = xmlConfig.Settings["TCPClientSettingForm/Top"].intValue;
                tcpClientCfgForm.Left = xmlConfig.Settings["TCPClientSettingForm/Left"].intValue;
            }
        }
        
        /// <summary>
        /// init all docked forms
        /// </summary>
		private void InitAllStuff()
		{
            ReadSettingsFile();

            tcpClientCfgForm.txtHostIP.Text = hostIp;
            tcpClientCfgForm.txtHostPort.Text = hostPort;
            tcpClientCfgForm.txtMessageStartId.Text = messageStartId;
            tcpClientCfgForm.txtMessageStopId.Text = messageStopId;
            
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
                hostIp = tcpClientCfgForm.txtHostIP.Text;
                hostPort = tcpClientCfgForm.txtHostPort.Text;
                messageStartId = tcpClientCfgForm.txtMessageStartId.Text;
                messageStopId = tcpClientCfgForm.txtMessageStopId.Text;
                
		      try
              {
                //create a new client socket ...
                m_socWorker = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                String szIPSelected  = hostIp;
                String szPort = hostPort;
                int  alPort = System.Convert.ToInt16 (szPort,10);
            
                System.Net.IPAddress    remoteIPAddress     = System.Net.IPAddress.Parse(szIPSelected);
                System.Net.IPEndPoint    remoteEndPoint = new System.Net.IPEndPoint(remoteIPAddress, alPort);
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
            dgvSendForm.SaveAllXml();
            if (m_socWorker != null)
            {
                m_socWorker.Close();
            }
            if (xmlConfig == null)
                xmlConfig = new Xmlconfig(execName + ".ini.xml", true);
            xmlConfig["ProjectName"].Value = projectName;
            xmlConfig["TcpClient/Ip"].Value = hostIp;
            xmlConfig["TcpClient/Port"].Value = hostPort;
            xmlConfig["TcpClient/MessageStart"].Value = messageStartId;
            xmlConfig["TcpClient/MessageStop"].Value = messageStopId;
            
            xmlConfig["MainForm/Width"].intValue = this.Width;
            xmlConfig["MainForm/Height"].intValue = this.Height;
            
            xmlConfig["DataGridViewSendForm/Top"].intValue = dgvSendForm.Top;
            xmlConfig["DataGridViewSendForm/Left"].intValue = dgvSendForm.Left;
            xmlConfig["DataGridViewSendForm/Width"].intValue = dgvSendForm.Width;
            xmlConfig["DataGridViewSendForm/Height"].intValue = dgvSendForm.Height;
            
            xmlConfig["rtxtForm/Top"].intValue = rtxtForm.Top;
            xmlConfig["rtxtForm/Left"].intValue = rtxtForm.Left;
            xmlConfig["rtxtForm/Width"].intValue = rtxtForm.Width;
            xmlConfig["rtxtForm/Height"].intValue = rtxtForm.Height;

            xmlConfig.Settings["TCPClientSettingForm/Top"].Value = tcpClientCfgForm.Top.ToString();
            xmlConfig.Settings["TCPClientSettingForm/Left"].Value = tcpClientCfgForm.Left.ToString();
            //xmlConfig.Settings["TCPClientSettingForm/Width"].Value = tcpClientCfgForm.Width.ToString();
            //xmlConfig.Settings["TCPClientSettingForm/Height"].Value = tcpClientCfgForm.Height.ToString();
            //xmlConfig.Settings["TCPClientSettingForm/Visible"].boolValue = tcpClientCfgForm.Visible;

            xmlConfig.Commit();
            if (System.IO.File.Exists(execName + ".ini"))
                System.IO.File.Delete(execName + ".ini");
        }

        /// <summary>
        /// sends a message as: messageStartId + message + messageStopId
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(string message)
		{
            message = messageStartId + message + messageStopId;
            
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

        private void tsBtnProjectNameApply_Click(object sender, EventArgs e)
        {
            projectName = tstxtProjectName.Text;
            ApplyProjectName();
        }

        private void ApplyProjectName()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string exeNameWithoutExt = Path.GetFileNameWithoutExtension(exePath);
            if (projectName.Trim().Length != 0) 
                this.Text = $"{exeNameWithoutExt} - {projectName}";
            else
                this.Text = $"{exeNameWithoutExt}";
        }

        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            if (mainRootDataStructures.sendItemsTabs.Count == 0)
            {
                rtxtForm.rtxt.AppendTextLine("mainRootDataStructures.sendItemsTabs.Count == 0", HorizontalAlignment.Left);
                for (int i = 0; i < dgvSendForm.openJsonFiles.Count; i++)
                {
                    SendDataJsonItems sdji = new SendDataJsonItems();
                    sdji.Name = dgvSendForm.openJsonFiles[i].GetFileName();
                    mainRootDataStructures.sendItemsTabs.Add(sdji);
                    for (int j = 0; j < dgvSendForm.openJsonFiles[i].data.Count; j++)
                    {
                        sdji.items.Add(dgvSendForm.openJsonFiles[i].data[j]);
                    }

                }

                
            } else
            {
                rtxtForm.rtxt.AppendTextLine("using existing structure", HorizontalAlignment.Left);
            }
            mainRootDataStructures.Save("test.json");
        }

        private void tsbtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonStr = File.ReadAllText("test.json");
                mainRootDataStructures = JsonConvert.DeserializeObject<MainRootDataStructures>(jsonStr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
