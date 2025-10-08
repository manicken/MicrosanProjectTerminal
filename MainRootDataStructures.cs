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
    public class MainRootDataStructures
    {
        /// <summary>
        /// 
        /// </summary>
        public List<SendDataJsonItems> sendItemsTabs { get; set; } = new List<SendDataJsonItems>();
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
            sb.AppendLine("  \"sendItemsTabs\": [");
            for (int i=0;i<sendItemsTabs.Count;i++)
            {
                sb.Append(sendItemsTabs[i].ToJsonString("  "));
                if (i < sendItemsTabs.Count - 1)
                    sb.AppendLine(",");
                else
                    sb.AppendLine();
            }
            sb.AppendLine("  ],");
            sb.AppendLine(socket.ToJsonString("  ") + ",");
            sb.AppendLine(window.ToJsonString("  "));
            sb.AppendLine("}");
            File.WriteAllText(path, sb.ToString());
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
        public int x { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int y { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int height { get; set; }
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
            sb.Append(lineincr); sb.Append("  \"socket\": ");  sb.AppendLine(JsonConvert.SerializeObject(socket) + ",");
            sb.Append(lineincr); sb.Append("  \"dgvSend\": "); sb.AppendLine(JsonConvert.SerializeObject(dgvSend) + ",");
            sb.Append(lineincr); sb.Append("  \"log\": ");     sb.AppendLine(JsonConvert.SerializeObject(log));
            sb.Append(lineincr + "}");
            return sb.ToString();
        }
    }
}
