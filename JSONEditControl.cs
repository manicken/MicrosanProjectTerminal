/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 2018-03-29
 * Time: 10:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Microsan
{
	/// <summary>
	/// Description of SourceCodeEditControl.
	/// </summary>
	public partial class JSONEditControl : UserControl
	{
        public Action<string> Save = null;
        public Action SaveAll = null;
        public Action Execute = null;

        TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));
        TextStyle KeyStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle StringStyle = new TextStyle(Brushes.Brown, null, FontStyle.Regular);
        TextStyle NumberStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        TextStyle BoolStyle = new TextStyle(Brushes.DarkCyan, null, FontStyle.Bold);
        TextStyle NullStyle = new TextStyle(Brushes.Gray, null, FontStyle.Italic);
        TextStyle PunctuationStyle = new TextStyle(Brushes.DimGray, null, FontStyle.Regular);

        const string ERROR_TABLE_COLNAME_TYPE = "Type";
        const string ERROR_TABLE_COLNAME_PATH = "Path";
        const string ERROR_TABLE_COLNAME_LINE = "Line";
        const string ERROR_TABLE_COLNAME_COL = "Col";
        const string ERROR_TABLE_COLNAME_NOTE = "Description";

        private DataTable dtLog;
        
        MenuItem menuItemPaste;

        public bool docked = false;
        
        private bool init = false;
        
        private List<SourceFile> sourceFilesRef;

       // public Autocomplete ac;
        
        public JSONEditControl() : this(Language.Custom)
		{
			
		}
		
		public JSONEditControl(Language language)
		{
			InitializeComponent();
            
            fctb.Language = language;

            dtLog = new DataTable();
            dtLog.Columns.Add(ERROR_TABLE_COLNAME_TYPE, typeof(string));
            dtLog.Columns.Add(ERROR_TABLE_COLNAME_PATH, typeof(string));
            dtLog.Columns.Add(ERROR_TABLE_COLNAME_LINE, typeof(int));
            dtLog.Columns.Add(ERROR_TABLE_COLNAME_COL, typeof(int));
            dtLog.Columns.Add(ERROR_TABLE_COLNAME_NOTE, typeof(string));
            

            dgv.DataSource = dtLog;
            fastColoredTextBox_setRightClickContextMenu();
            
            dgv.CellClick += dgv_CellClick;

            fctb.TextChanged += fastColoredTextBox_TextChanged;

            //ac = new Autocomplete(fctb);
            // ac.Debug = ac_Debug;

        }

        public void Update(string jsonStr, List<JsonSerializeError> errorList)
        {
            Update(errorList);
            fctb.Text = jsonStr;
        }
        public void Update(string jsonStr)
        {
            splitContainer1.Panel2Collapsed = true;
            fctb.Text = jsonStr;
        }

        public void Update(List<JsonSerializeError> errorList)
        {
            splitContainer1.Panel2Collapsed = false;
            dtLog.Rows.Clear();
            for (int i = 0; i < errorList.Count; i++)
            {
                JsonSerializeError jse = errorList[i];
                DataRow dr = dtLog.NewRow();
                dr[ERROR_TABLE_COLNAME_TYPE] = jse.Type;
                dr[ERROR_TABLE_COLNAME_PATH] = jse.Path;
                dr[ERROR_TABLE_COLNAME_LINE] = jse.Line;
                dr[ERROR_TABLE_COLNAME_COL] = jse.Column;
                dr[ERROR_TABLE_COLNAME_NOTE] = jse.Message;
                dtLog.Rows.Add(dr);
            }
        }

        private void ac_Debug(string text)
        {
            //rtxtLog.AppendLine_ThreadSafe(text, true);
        }
		
		private ContextMenu GetTabPageCM(string title, int index)
        {
            ContextMenu cm = new ContextMenu();
           // MenuItem mi = new MenuItem("close file", tcContextMenuCloseFile_Click);
          //  mi.Tag = index;
          //  cm.MenuItems.Add(mi);
            return cm;
        }


        public void ClearLog()
        {
            splitContainer1.Panel2Collapsed = true;
            dtLog.Rows.Clear();
            rtxtLog.Clear();
            ResetSelections();
        }

        public void SelectCharAtPos(int linePos, int colPos)
        {
            try
            {
                FastColoredTextBoxNS.Range range = fctb.GetLine(linePos);
                range.Start = new FastColoredTextBoxNS.Place(colPos - 1, linePos - 1);
                range.End = new FastColoredTextBoxNS.Place(colPos, linePos - 1);
                fctb.Selection = range;

                fctb.DoSelectionVisible(); // scroll to selection
            }
            catch (Exception ex)
            {
                rtxtLog.AppendText("exception @ SelectCharAtPos: line=" + linePos + ", col=" + colPos + "\r\n");
                //rtxtLog.AppendText(ex.ToString() + "\r\n");
            }
        }

        public void ResetSelections()
        {
            fctb.Selection = new Range(fctb, Place.Empty, Place.Empty);
        }

        public void AppendLineToLog(string logMessage)
        {
            rtxtLog.AppendText(logMessage + "\n");
        }

        private void tsBtnSave_Click(object sender, EventArgs e)
        {
            
        }
        
        public void AppendText(string text)
        {
            fctb.SelectedText = text;
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            rtxtLog.Clear();
            rtxtLog.ClearUndo();
        }

        private void fastColoredTextBox_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            fctb.LeftBracket = '{';
            fctb.RightBracket = '}';
            fctb.LeftBracket2 = '[';
            fctb.RightBracket2 = ']';

            // 🧹 clear previous styles
            e.ChangedRange.ClearStyle(KeyStyle, StringStyle, NumberStyle, BoolStyle, NullStyle, PunctuationStyle);

            // 🧱 order matters! match keys BEFORE general strings
            // JSON key pattern: "key": 
            //e.ChangedRange.SetStyle(KeyStyle, @"(?<=""|\b)(?<key>""[^""\\]*(?:\\.[^""\\]*)*""(?=\s*:))");
            e.ChangedRange.SetStyle(KeyStyle, @"(?<key>""[^""\\]*(?:\\.[^""\\]*)*"")(?=\s*:)");


            // normal strings (values) — ensure not to re-match keys
            e.ChangedRange.SetStyle(StringStyle, @"(?<=:\s*)(?<value>""[^""\\]*(?:\\.[^""\\]*)*"")");

            // numbers
            e.ChangedRange.SetStyle(NumberStyle, @"(?<=:\s*)(?<num>-?\d+(\.\d+)?([eE][\+\-]?\d+)?)");

            // booleans
            e.ChangedRange.SetStyle(BoolStyle, @"(?<=:\s*)(true|false)\b");

            // null
            e.ChangedRange.SetStyle(NullStyle, @"(?<=:\s*)null\b");

            // punctuation
            e.ChangedRange.SetStyle(PunctuationStyle, @"[\{\}\[\],:]");

            // folding markers
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("{", "}");
            e.ChangedRange.SetFoldingMarkers(@"\[", @"\]");
        }

        private void JsonSyntaxHighlight(TextChangedEventArgs e)
        {
            
        }



        private void fastColoredTextBox_setRightClickContextMenu()
        {
            ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
            MenuItem menuItem;
                
            menuItem = new MenuItem("Redo");
            menuItem.Click += delegate (object sender, EventArgs e) { fctb.Redo(); };
            contextMenu.MenuItems.Add(menuItem);
            menuItem = new MenuItem("Undo");
            menuItem.Click += delegate (object sender, EventArgs e) { fctb.Undo(); };
            contextMenu.MenuItems.Add(menuItem);
                
            contextMenu.MenuItems.Add(new MenuItem("-"));

            menuItem = new MenuItem("Increase Line Indent");
            menuItem.Click += delegate (object sender, EventArgs e) { fctb.IncreaseIndent(); };
            contextMenu.MenuItems.Add(menuItem);
            menuItem = new MenuItem("Decrease Line Indent");
            menuItem.Click += delegate (object sender, EventArgs e) { fctb.DecreaseIndent(); };
            contextMenu.MenuItems.Add(menuItem);

            contextMenu.MenuItems.Add(new MenuItem("-"));

            menuItem = new MenuItem("Cut");
            menuItem.Click += delegate (object sender, EventArgs e) { fctb.Cut(); };
            contextMenu.MenuItems.Add(menuItem);
                
            menuItem = new MenuItem("Copy");
            menuItem.Click += delegate (object sender, EventArgs e) { fctb.Copy(); };
            contextMenu.MenuItems.Add(menuItem);
                
            menuItemPaste = new MenuItem("Paste");
            menuItemPaste.Click += delegate (object sender, EventArgs e) { fctb.Paste(); };
            contextMenu.MenuItems.Add(menuItemPaste);
                
            contextMenu.MenuItems.Add(new MenuItem("-"));
                
            menuItem = new MenuItem("Delete");
            menuItem.Click += delegate (object sender, EventArgs e) { fctb.SelectedText = ""; };
            contextMenu.MenuItems.Add(menuItem);

            contextMenu.Popup += new EventHandler(ContextMenu_Popup_Action);
            fctb.ContextMenu = contextMenu;
        }

		void ContextMenu_Popup_Action(object s, EventArgs ea)
		{
			menuItemPaste.Enabled = Clipboard.ContainsText();
		}
       
        

        private void fastColoredTextBox_SelectionChangedDelayed(object sender, EventArgs e)
        {
            fctb.VisibleRange.ClearStyle(SameWordsStyle);
            if (!fctb.Selection.IsEmpty)
                return;//user selected diapason

            //get fragment around caret
            var fragment = fctb.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
                return;
            //highlight same words
            var ranges = fctb.VisibleRange.GetRanges("\\b" + text + "\\b").ToArray();
            if (ranges.Length > 1)
                foreach (var r in ranges)
                    r.SetStyle(SameWordsStyle);
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectLogItem(e.RowIndex);
        }

        public void SelectLogItem(int rowIndex)
        {
            int textRow = (int)dtLog.Rows[rowIndex][ERROR_TABLE_COLNAME_LINE];
            int textCol = (int)dtLog.Rows[rowIndex][ERROR_TABLE_COLNAME_COL];
            rtxtLog.Text = (string)dtLog.Rows[rowIndex][ERROR_TABLE_COLNAME_NOTE];
            if ((textRow < 1) || (textCol < 1))
                return;

            SelectCharAtPos(textRow, textCol);
        }
        
        
   
        private void SelectFile(string fileName)
        {
            fileName = fileName.ToLower();
            for (int i = 0; i < sourceFilesRef.Count; i++)
            {
                if (sourceFilesRef[i].FileName.ToLower() == fileName)
                {

                    if (fctb.Tag != null)
                    {
                        SourceFile sf = (SourceFile)fctb.Tag;
                        sf.Contents = fctb.Text;

                        sf.editorSelectionStart = fctb.SelectionStart;
                        sf.editorSelectionLength = fctb.SelectionLength;

                        sf.editorVerticalScrollValue = fctb.VerticalScroll.Value;
                        sf.editorHorizontalScrollValue = fctb.HorizontalScroll.Value;
                        
                    }
                    
                    fctb.Text = sourceFilesRef[i].Contents;
                    fctb.Tag = sourceFilesRef[i];
                    fctb.SelectionStart = sourceFilesRef[i].editorSelectionStart;
                    fctb.SelectionLength = sourceFilesRef[i].editorSelectionLength;
                    
                    fctb.HorizontalScroll.Value = sourceFilesRef[i].editorHorizontalScrollValue;
                    fctb.VerticalScroll.Value = sourceFilesRef[i].editorVerticalScrollValue;
                    fctb.UpdateScrollbars();

                    
                    //rtxtLog.AppendText("ok @ SelectFile: fileName=" + fileName + "\r\n");
                    return;
                }
            }
            rtxtLog.AppendText("error @ SelectFile: fileName=" + fileName + "\r\n");
        }
        private void fastColoredTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //tc.Enabled = false;
            //tsslblMain.Text =  "(text changed, you need to save before switch tabs)";
        }
        
        
    }
}
