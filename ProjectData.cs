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
        public static string CurrentProjectFilePath = "";
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
        public ConnectionsData connections { get; set; } = new ConnectionsData();
        /// <summary>
        /// 
        /// </summary>
        public WindowSettings window { get; set; } = new WindowSettings();

        public string dockData { get; set; } = "";

        public List<SourceFile> sourceFiles { get; set; } = new List<SourceFile>();

        private int newItemCount = 0;

        public SendDataJsonItems SendGroupByName(string name)
        {
            for (int i=0;i<sendGroups.Count;i++)
            {
                if (sendGroups[i].Name == name) return sendGroups[i];
            }
            newItemCount++;
            SendDataJsonItems newGrp = new SendDataJsonItems("newItem"+ (newItemCount), "newitem comment");
            sendGroups.Add(newGrp);
            return newGrp;
        }
        public bool SendGroupsContainsName(string name)
        {
            for (int i = 0; i < sendGroups.Count; i++)
            {
                if (sendGroups[i].Name == name) return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ToJsonString()
        {
            string incr = "  ";
            List<string> jsonObjects = new List<string>();
            jsonObjects.Add(meta.ToJsonString(incr));
            jsonObjects.Add(SendGroupsToJsonString(incr));
            jsonObjects.Add(connections.ToJsonString(incr));
            jsonObjects.Add(SourceFilesToJsonString(incr));
            jsonObjects.Add(window.ToJsonString(incr));
            jsonObjects.Add($"{incr}\"dockData\":{JsonConvert.SerializeObject(dockData)}");

            return $"{{\n{string.Join(",\n", jsonObjects)}\n}}\n";
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

        private string SourceFilesToJsonString(string lineincr)
        {
            List<string> fileJsonObjectStrings = new List<string>();
            for (int i = 0; i < sourceFiles.Count; i++)
            {
                fileJsonObjectStrings.Add(sourceFiles[i].ToJsonString(lineincr));
            }
            string fileJsonObjectsStr = string.Join(",\n", fileJsonObjectStrings);
            return $"{lineincr}\"sourceFiles\": [\n{fileJsonObjectsStr}\n{lineincr}]";
        }


        public static ProjectData LoadFromJsonString(string jsonStr, Action<List<JsonSerializeError>> onError)
        {
            ProjectData pdTemp = null;
            var errors = new List<JsonSerializeError>();
            try
            {
                pdTemp = JsonConvert.DeserializeObject<ProjectData>(jsonStr, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Error = (sender, args) => {
                        errors.Add(new JsonSerializeError(args));
                        args.ErrorContext.Handled = true; // skip the broken object
                    }
                });
            }
            catch (Exception ex)
            {
                // top-level parse failure (e.g. completely invalid JSON)
                errors.Add(new JsonSerializeError(ex.GetType().ToString(), "(file)", ex.ToString(), -1, -1));
            }
            if (errors.Count != 0)
            {
                onError?.Invoke(errors);
            }
            return pdTemp ?? new ProjectData();
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

        public RectData codeEdit { get; set; } = new RectData();

        public RectData jsonEdit { get; set; } = new RectData();

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
            sb.Append(lineincr); sb.Append("  \"codeEdit\": "); sb.AppendLine(JsonConvert.SerializeObject(codeEdit) + ",");
            sb.Append(lineincr); sb.Append("  \"jsonEdit\": "); sb.AppendLine(JsonConvert.SerializeObject(jsonEdit) + ",");
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

    public class JsonSerializeError
    {
        public string Type { get; set; } = "";
        public string Path { get; set; } = "";
        public string Message { get; set; } = "";
        public int Line { get; set; } = 0;
        public int Column { get; set; } = 0;

        public JsonSerializeError(string type, string path, string message, int line, int column)
        {
            this.Type = type; this.Path = path; this.Message = message;
            this.Line = line; this.Column = column;
        }
        public JsonSerializeError(Newtonsoft.Json.Serialization.ErrorEventArgs args)
        {
            this.Path = args.ErrorContext.Path ?? "(unknown path)";
            this.Message = args.ErrorContext.Error.Message;
            this.Type = args.ErrorContext.Error.GetType().ToString();

            var ex = args.ErrorContext.Error;
            if (ex is JsonReaderException jrex)
            {
                this.Line = jrex.LineNumber;
                this.Column = jrex.LinePosition;
            }
            else if (ex is JsonSerializationException jsex)
            {
                this.Line = jsex.LineNumber;
                this.Column = jsex.LinePosition;
            }
            else
            {
                this.Line = this.Column = 0;
            }
        }
        public override string ToString()
        {
            return $"[{Type}] Path: {Path}, Message: {Message}, Line: {Line}, Col: {Column}";
        }
    }
}
