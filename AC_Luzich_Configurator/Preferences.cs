using AC_Configurator_STDL.Controls;
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
            try
            {
                Global_var.AC_PATH = Global_var.ConfigFile.ReadString_v3("SYSTEM", "AC_PATH");

                Global_var.driver_name = Global_var.ConfigFile.ReadString_v3("USER", "DRIVER_NAME");
                Global_var.IP_PC_CLient = Global_var.ConfigFile.ReadString_v3("IP", "IP_CLIENT");

                Global_var.AC_Carpath = Global_var.AC_PATH + @"\content\cars\";
                Global_var.AC_Trackpath = Global_var.AC_PATH + @"\content\tracks\";


                SetupLoad();
            }
            catch (Exception ex)
            {
                MessageBox_Custom.Show(ex.Message, "Load Preference error", MessageBox_Custom.MessageType.Error);
                LogWriter.Error_Trace(ex);
            }


        }


        public static void SetupLoad()
        {

            if (Global_var.ConfigFile.ReadString_v3("RACE_SETUP", "TYRE_CONDITION") == "0")
            {
                Global_var.GUI_Window.TyreCond_Used_chkbox.TriggerCheckedClick = 1;
            }
            else
            {
                Global_var.GUI_Window.TyreCond_New_chkbox.TriggerCheckedClick = 1;
            }

            if (Global_var.ConfigFile.ReadString_v3("RACE_SETUP", "TYRE_WEAR") == "0")
            {
                Global_var.GUI_Window.TyreWear_OFF_chkbox.TriggerCheckedClick = 1;
            }
            else
            {
                Global_var.GUI_Window.TyreWear_ON_chkbox.TriggerCheckedClick = 1;
            }

            if (Global_var.ConfigFile.ReadString_v3("RACE_SETUP", "FUEL_CONSUMPTION") == "0")
            {
                Global_var.GUI_Window.FuelCons_OFF_chkbox.TriggerCheckedClick = 1;
            }
            else
            {
                Global_var.GUI_Window.FuelCons_ON_chkbox.TriggerCheckedClick = 1;
            }

            if (Global_var.ConfigFile.ReadString_v3("RACE_SETUP", "IDEAL_LINE") == "0")
            {
                Global_var.GUI_Window.IdealLine_OFF_chkbox.TriggerCheckedClick = 1;
            }
            else
            {
                Global_var.GUI_Window.IdealLine_ON_chkbox.TriggerCheckedClick = 1;
            }

            if (Global_var.ConfigFile.ReadString_v3("RACE_SETUP", "TRACK_GRIP") == "0")
            {
                Global_var.GUI_Window.TrackGrip_Green_chkbox.TriggerCheckedClick = 1;
            }
            else
            {
                Global_var.GUI_Window.TrackGrip_Race_chkbox.TriggerCheckedClick = 1;
            }


            Global_var.GUI_Window.FuelLoad_Sld.SetValue(Convert.ToInt32(Global_var.ConfigFile.ReadString_v3("RACE_SETUP", "FUEL_LOAD")));
            Global_var.GUI_Window.StabiltyCtrl_Sld.SetValue(Convert.ToInt32(Global_var.ConfigFile.ReadString_v3("RACE_SETUP", "STABILITY_CTRL")));


        }



        public static void Save_RaceCondition()
        {
            Global_var.ConfigFile.WriteString_v2("RACE_SETUP", "TYRE_CONDITION", Race_Condtions.Tyre_Cond.ToString());
            Global_var.ConfigFile.WriteString_v2("RACE_SETUP", "TYRE_WEAR", Race_Condtions.Tyre_Wear.ToString());
            Global_var.ConfigFile.WriteString_v2("RACE_SETUP", "FUEL_CONSUMPTION", Race_Condtions.Fuel_Cons.ToString());
            Global_var.ConfigFile.WriteString_v2("RACE_SETUP", "TRACK_GRIP", Race_Condtions.Track_Grip.ToString());
            Global_var.ConfigFile.WriteString_v2("RACE_SETUP", "IDEAL_LINE", Race_Condtions.Ideal_Line.ToString());
            
            Global_var.ConfigFile.WriteString_v2("RACE_SETUP", "FUEL_LOAD", Race_Condtions.Fuel_Load.ToString());
            Global_var.ConfigFile.WriteString_v2("RACE_SETUP", "STABILITY_CTRL", Race_Condtions.Stability_Ctrl.ToString());
        }




    }
}
