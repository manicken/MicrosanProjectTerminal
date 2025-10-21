using System;
using System.Text;
using System.Windows.Forms;
using Microsan;

//using OxyPlot;
//using OxyPlot.Series;
//using OxyPlot.WindowsForms;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO.Ports;

namespace RuntimeProgrammingNamespace
{ 
    public class RootClass
    {
        private static object RootObject = null;
        
        /// <summary> The main entry point for the runtime compile code. </summary>

        public static void Main(object rootObject)
        {
            RootObject = rootObject;
            
            NewClass.NewMethod(RootObject);
        }
        
        [Shortcut("Start Test1","start")]
        public static void Test1()
        {
            MessageBox.Show("Test1");
        }
        [Shortcut("Start Test2","start2")]
        public static void Test2()
        {
            MessageBox.Show("Test2");
        }
        [Shortcut("Start Test3","stop")]
        public static void Test3()
        {
            MessageBox.Show("Test3");
        }
    }
}