using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Configurator_STDL
{
    public static class Preferences
    {

        public static void Load()
        {
            Global_var.AC_PATH = Global_var.ConfigFile.ReadString_v3("SYSTEM", "AC_PATH");

            Global_var.driver_name = Global_var.ConfigFile.ReadString_v3("USER", "DRIVER_NAME");
            Global_var.IP_PC_CLient = Global_var.ConfigFile.ReadString_v3("IP", "IP_CLIENT");

            Global_var.AC_Carpath = Global_var.AC_PATH + @"\content\cars\";
            Global_var.AC_Trackpath = Global_var.AC_PATH + @"\content\tracks\";  
            //Global_var.Assists_ini_file = new IniFile(Global_var.AC_cfg_Path + @"\assists.ini");

        }


    }
}
