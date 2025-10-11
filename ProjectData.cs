using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace Microsan
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectData
    {
        /// <summary>
        /// 
        /// </summary>
        public Metadata meta { get; set; } = new Metadata();
        /// <summary>
        /// 
        /// </summary>
        public List<SendDataJsonItems> sendGroups { get; set; } = new List<SendDataJsonItems>();
        /// <summary>
        /// 
        /// </summary>
        public SocketConnectionSettings socket { get; set; } = new SocketConnectionSettings();
        /// <summary>
        /// 
        /// </summary>
        public WindowSettings window { get; set; } = new WindowSettings();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            var sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine(meta.ToJsonString("  ") + ",");
            sb.AppendLine(SendGroupsToJsonString("  ") + ",");
            sb.AppendLine(socket.ToJsonString("  ") + ",");
            sb.AppendLine(window.ToJsonString("  "));
            sb.AppendLine("}");
            File.WriteAllText(path, sb.ToString());
        }

        private string SendGroupsToJsonString(string lineincr)
        {
            var sb = new StringBuilder();
            sb.AppendLine(lineincr + "\"sendGroups\": [");
            for (int i = 0; i < sendGroups.Count; i++)
            {
                sb.Append(sendGroups[i].ToJsonString(lineincr));
                if (i < sendGroups.Count - 1)
                    sb.AppendLine(",");
                else
                    sb.AppendLine();
            }
            sb.Append(lineincr + "]");
            return sb.ToString();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class SocketConnectionSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string ip { get; set; } = "127.0.0.1";
        /// <summary>
        /// 
        /// </summary>
        public int port { get; set; } = 9000;
        /// <summary>
        /// 
        /// </summary>
        public string msgPrefix { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>

        public string msgPostfix { get; set; } = "\r\n";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineincr"></param>
        /// <returns></returns>
        public string ToJsonString(string lineincr)
        {
            var sb = new StringBuilder();
            sb.Append(lineincr);
            sb.Append("\"socket\":");
            sb.Append(JsonConvert.SerializeObject(this, Formatting.Indented).Replace("\n","\n  "));
            return sb.ToString();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class RectData
    {
        /// <summary>
        /// 
        /// </summary>
        public int x { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        public int y { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        public int width { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        public int height { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        public RectData() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        public RectData(Rectangle r)
        {
            x = r.X; y = r.Y; width = r.Width; height = r.Height;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="f">the windows form</param>
        public void ApplyTo(System.Windows.Forms.Form f)
        {
            //System.Windows.Forms.MessageBox.Show(JsonConvert.SerializeObject(this));
            f.Left = x;
            f.Top = y;
            if (width > 100)
                f.Width = width;
            if (height > 50)
                f.Height = height;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="f">the windows form</param>
        public void GetFrom(System.Windows.Forms.Form f)
        {
            x = f.Left;
            y = f.Top;
            width = f.Width;
            height = f.Height;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Rectangle ToRectangle() => new Rectangle(x, y, width, height);
    }

    /// <summary>
    /// 
    /// </summary>
    public class WindowSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public RectData main { get; set; } = new RectData();

        /// <summary>
        /// 
        /// </summary>
        public RectData socket { get; set; } = new RectData();
        /// <summary>
        /// 
        /// </summary>
        public RectData dgvSend { get; set; } = new RectData();
        /// <summary>
        /// 
        /// </summary>
        public RectData log { get; set; } = new RectData();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineincr"></param>
        /// <returns></returns>
        public string ToJsonString(string lineincr)
        {
            var sb = new StringBuilder();
            sb.Append(lineincr); sb.AppendLine("\"window\": {");
            sb.Append(lineincr); sb.Append("  \"main\": "); sb.AppendLine(JsonConvert.SerializeObject(main) + ",");
            sb.Append(lineincr); sb.Append("  \"socket\": ");  sb.AppendLine(JsonConvert.SerializeObject(socket) + ",");
            sb.Append(lineincr); sb.Append("  \"dgvSend\": "); sb.AppendLine(JsonConvert.SerializeObject(dgvSend) + ",");
            sb.Append(lineincr); sb.Append("  \"log\": ");     sb.AppendLine(JsonConvert.SerializeObject(log));
            sb.Append(lineincr + "}");
            return sb.ToString();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Metadata
    {
        /// <summary>
        /// 
        /// </summary>
        public string projectName { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public string created { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        /// <summary>
        /// 
        /// </summary>
        public string modified { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        /// <summary>
        /// 
        /// </summary>
        public int version { get; set; } = 1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineincr"></param>
        /// <returns></returns>
        public string ToJsonString(string lineincr)
        {
            modified = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // update on save
            var sb = new StringBuilder();
            sb.Append(lineincr); sb.AppendLine("\"meta\": {");
            sb.Append(lineincr); sb.Append("\"projectName\":"); sb.AppendLine(JsonConvert.SerializeObject(projectName) + ",");
            sb.Append(lineincr); sb.Append("\"created\":"); sb.AppendLine(JsonConvert.SerializeObject(created) + ",");
            sb.Append(lineincr); sb.Append("\"modified\":"); sb.AppendLine(JsonConvert.SerializeObject(modified) + ",");
            sb.Append(lineincr); sb.Append("\"version\":"); sb.AppendLine(JsonConvert.SerializeObject(version));
            sb.Append(lineincr + "}");
            return sb.ToString();
        }
    }
}
