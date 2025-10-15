using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Microsan
{
    public partial class JSONEditorForm : Form
    {

        public Action<string, ProjectData> Save;
        public JSONEditorForm()
        {
            InitializeComponent();

            tsBtnSave.Click += tsBtnSave_Click;
            this.FormClosing += this_FormClosing;
        }

        private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }

        private void tsBtnSave_Click(object sender, EventArgs e)
        {
            // first validate the json
            bool valid = true;
            string jsonStr = jsonEditControl.fctb.Text;
            ProjectData pdTemp = ProjectData.LoadFromJsonString(jsonStr, (errorList) => {
                valid = false;
                jsonEditControl.Update(errorList);
            });
            if (valid)
            {
                jsonEditControl.ClearLog();
                Save?.Invoke(jsonStr, pdTemp);
            }
                                
        }

        public void Show(string jsonStr, List<JsonSerializeError> errorList)
        {
            this.Show();
            jsonEditControl.Update(jsonStr, errorList);
            
        }
        public void Show(string jsonStr)
        {
            this.Show();
            jsonEditControl.Update(jsonStr);
        }
    }
}
