using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC_CoRe.Dialog;

namespace AC_CoRe
{
    public static class LogWriter
    {
        //directory create from build events in project properties
        static string datetime = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
        static string logfilePath = Global_var.logs_path + datetime + ".log";


        static public void Error_Trace(Exception ex)
        {

            try
            {
                using (StreamWriter writer = new StreamWriter(logfilePath, true))
                {
                    writer.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine);
                    writer.WriteLine("Date :" + DateTime.Now.ToString() + Environment.NewLine);
                    writer.WriteLine("Message :" + ex.Message + " " + Environment.NewLine);
                    writer.WriteLine("------------------------------------------------------------------------------------------------" + Environment.NewLine);
                    writer.WriteLine("inner Exception" + ex.InnerException + Environment.NewLine);
                    writer.WriteLine("------------------------------------------------------------------------------------------------" + Environment.NewLine);
                    writer.WriteLine("StackTrace :" + ex.StackTrace + Environment.NewLine);
                    writer.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine);
                }

            }
            catch (Exception e)
            {
                MessageBox_Custom.Show(ex.Message, "Logger folder not found!", MessageBox_Custom.MessageType.Error);

            }


        }

        static public void Message_Trace(string Message)
        {

            using (StreamWriter writer = new StreamWriter(logfilePath, false))
            {
                writer.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine);
                writer.WriteLine("Date :" + DateTime.Now.ToString() + Environment.NewLine);
                writer.WriteLine("Message :" + Message + "" + Environment.NewLine);
                writer.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine);
            }

        }
    }
}
