using AC_CoRe.Dialog;
using Ini.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Threading;

namespace AC_CoRe
{
    public static class Actions
    {
        public static Process[] acs_pro_process, acs_steam_process;
        public static DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public static void AC_Process_starts()
        {
            
           

            if (IS_AC_OFF())
            {
                AC_Start();

            }
            else
            {

                if (Global_var.AC_PRO == "1") acs_pro_process[0].Kill();
                else acs_steam_process[0].Kill();

                AC_Start();


            }

        }

       public static bool IS_AC_OFF()
        {
            

            acs_pro_process = Process.GetProcessesByName("acs_pro");
            acs_steam_process = Process.GetProcessesByName("acs");

            if (acs_pro_process.Length == 0 && acs_steam_process.Length == 0) return true;
            else return false;
        }

        private static void AC_Start()
        {
            if (Global_var.AC_PRO == "1")
            {
                ProcessStartInfo infostart = new ProcessStartInfo();
                infostart.WorkingDirectory = Global_var.AC_Path;  //FONDAMENTALE PER FUNZIONARE
                if (File.Exists(Global_var.AC_Path + "\\acs_pro.exe"))
                {
                    infostart.FileName = Global_var.AC_Path + "\\acs_pro.exe";
                    infostart.Arguments = "-autodrive";
                }
                Process.Start(infostart);
            }

            else
            {
                ProcessStartInfo infostart = new ProcessStartInfo();
                infostart.WorkingDirectory = Global_var.AC_Path;  //FONDAMENTALE PER FUNZIONARE
                if (File.Exists(Global_var.AC_Path + "\\acs.exe"))
                {

                    infostart.FileName = Global_var.AC_Path + "\\acs.exe";
                    infostart.Arguments = "-autodrive";
                }

                Process.Start(infostart);
            }
        }



        public static void DoTheDirtyJobOnFiles()
        {

            try
            {
                //string userdocumentspath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                IniFile configurator_param_file = new IniFile(Global_var.AC_cfg_Path + "\\configurator_parameters.ini");

                string[] section_line_array = new string[10];
                string sectionname_prev = "none";

                Global_var.AC_CarName = configurator_param_file.ReadString_v3("race", "RACE.MODEL.cfg");

                Global_var.SectionNames = configurator_param_file.GetAllSectionName();
                List<KeyValuePair<string, string>> section_line_list = new List<KeyValuePair<string, string>>();

                Global_var.File_And_Path.Clear();

                

                foreach (string sectionname in Global_var.SectionNames)
                {
                    section_line_list = configurator_param_file.ReadSection_v2(sectionname);

                    IniFile filetomodify = null;

                    foreach (var section_line in section_line_list)
                    {




                        section_line_array = section_line.Key.Split(new char[] { '.' });

                        if (sectionname != sectionname_prev)
                        {

                            if (section_line_array[2] == "cfg") { filetomodify = new IniFile(Global_var.AC_cfg_Path + "\\" + sectionname + ".ini"); }
                            if (section_line_array[2] == "car") { filetomodify = new IniFile(Global_var.AC_car_Path + "\\" + Global_var.AC_CarName + "\\data\\" + sectionname + ".ini"); }

                            sectionname_prev = sectionname;

                        }

                        try
                        {
                            filetomodify.WriteString_v2(section_line_array[0], section_line_array[1], section_line.Value);
                        }
                        catch (Exception ex)
                        {
                            //DarkMessageBox.Show("configurator parameters writing issue: " + ex.Message);
                            MessageBox_Custom.Show(ex.Message, "ActionFile Error: Configurator Parameters writing issue", MessageBox_Custom.MessageType.Error);
                            LogWriter.Error_Trace(ex);
                            throw;
                        }




                        //Globalvar.File_And_Path.Add(new KeyValuePair<string, string>(sectionname, ArrayString[2]));
                    }



                }

            }
            catch (Exception ex)
            {

                //DarkMessageBox.Show(ex.Message, "ActionFile Error", DarkMessageBox.Buttons.OK, DarkMessageBox.Icon.Error);
                MessageBox_Custom.Show(ex.Message, "ActionFile Error", MessageBox_Custom.MessageType.Error);
                LogWriter.Error_Trace(ex);
            }
        }

    }
}
