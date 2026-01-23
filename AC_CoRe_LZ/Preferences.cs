using AC_CoRe.Dialog;
using Ini.Net;
using System;

namespace AC_CoRe
{
    public static class Preferences
    {

        public static void Load()
        {
            try
            {
                Global_var.configfile = new IniFile("System\\AC_CoRe_LZ.ini");

                Global_var.AC_Path = Global_var.configfile.ReadString_v3("START CONFIGURATION", "AC_PATH");
                Global_var.AC_PRO = Global_var.configfile.ReadString_v3("START CONFIGURATION", "AC_PRO");
                Global_var.AC_cfg_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Assetto Corsa\cfg";

                Global_var.AC_car_Path = Global_var.AC_Path + @"\content\cars";

                //NI
                /*Global_var.NI_enable = Convert.ToBoolean(Convert.ToInt32(Global_var.configfile.ReadString_v2("NI", "NI_ENABLE")));
                Global_var.NI_Name = Global_var.configfile.ReadString_v3("NI", "NI_NAME");
                Global_var.NI_Port = Global_var.configfile.ReadString_v3("NI", "NI_PORT");
                Global_var.NI_Line = Global_var.configfile.ReadString_v3("NI", "NI_LINE");
                Global_var.NI_LineAddress = Global_var.NI_Name +"/port" + Global_var.NI_Port + "/line" + Global_var.NI_Line;
                Global_var.NI_debug = Convert.ToBoolean(Convert.ToInt32(Global_var.configfile.ReadString_v3("NI", "NI_DEBUG")));
                //Arduino

                Global_var.Arduino_enable = Convert.ToBoolean(Convert.ToInt32(Global_var.configfile.ReadString_v2("ARDUINO", "ARDUINO_ENABLE")));
                Global_var.Arduino_ComPort = Global_var.configfile.ReadString_v3("ARDUINO", "ARDUINO_COMPORT");
                Global_var.Arduino_debug = Convert.ToBoolean(Convert.ToInt32(Global_var.configfile.ReadString_v3("ARDUINO", "ARDUINO_DEBUG")));*/

                //Global_var.Esc_Enable = Global_var.configfile.ReadString_v2("ESC_MOUSE_XY", "ESC_ENABLE");
                Global_var.ESC_METHOD_MOUSEX = Convert.ToInt32(Global_var.configfile.ReadString_v3("ESC_MOUSE_XY", "ESC_MOUSE_X"));
                Global_var.ESC_METHOD_MOUSEY = Convert.ToInt32(Global_var.configfile.ReadString_v3("ESC_MOUSE_XY", "ESC_MOUSE_Y"));

                Global_var.BBox_Enable = Global_var.configfile.ReadString_v3("BBOX", "ENABLE");
                Global_var.BBox_StartBtn_index = Global_var.configfile.ReadString_v3("BBOX", "START_BTN_INDEX");
                Global_var.BBox_StopBtn_index = Global_var.configfile.ReadString_v3("BBOX", "STOP_BTN_INDEX");


                /*Global_var.Background_image = Global_var.configfile.ReadString_v2("THEME", "BACKGROUND_IMAGE");
                 Global_var.Opacity = Convert.ToDouble(Global_var.configfile.ReadString_v2("THEME", "OPACITY"));
                 Global_var.Style = Global_var.configfile.ReadString_v2("THEME", "STYLE");*/


                /*Skins_template.Add_Skins();
                Skins_template.Load_Skins(Global_var.Background_image);*/

                //ThemeManager.Current.ChangeTheme(Global_var.GUI_Window, Global_var.Style);



            }
            catch (Exception ex)
            {
                //DarkMessageBox.Show("Preference Load Issue: "+ex.Message,"Error",DarkMessageBox.Buttons.OK,DarkMessageBox.Icon.Error);
                MessageBox_Custom.Show(ex.Message, "Preference Load Issue", MessageBox_Custom.MessageType.Error);
                LogWriter.Error_Trace(ex);

            }
            
        }

        public static void Save() {

            /* Global_var.configfile.WriteString_v2("ARDUINO", "ARDUINO_ENABLE",(Convert.ToInt32(Global_var.Arduino_enable)).ToString());
             Global_var.configfile.WriteString_v2("NI", "NI_ENABLE", (Convert.ToInt32(Global_var.NI_enable)).ToString());

             Global_var.configfile.WriteString_v2("ARDUINO", "ARDUINO_DEBUG", (Convert.ToInt32(Global_var.Arduino_debug)).ToString());
             Global_var.configfile.WriteString_v2("NI", "NI_DEBUG", (Convert.ToInt32(Global_var.NI_debug)).ToString());

             Global_var.configfile.WriteString_v2("ARDUINO", "ARDUINO_COMPORT", Global_var._AC_Core_GUI.Arduino_Comport_cmbbox.SelectedValue.ToString());*/

            Global_var.configfile.WriteString_v2("BBOX", "START_BTN_INDEX", Global_var.BBox_StartBtn_index);
            Global_var.configfile.WriteString_v2("BBOX", "STOP_BTN_INDEX", Global_var.BBox_StopBtn_index);
        }


    }
}
