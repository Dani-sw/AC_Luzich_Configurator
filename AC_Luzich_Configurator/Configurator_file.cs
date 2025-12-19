using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Configurator_STDL
{
    public static class Configurator_file
    {
        static public void Write_Car_param()
        {
            Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "RACE.MODEL.cfg", Global_var._AC_Cars.ACname);
            Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "CAR_0.SKIN.cfg", Global_var.AC_CarSkin);
            if (Global_var.GhostCar == "1")
            {
                Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "GHOST_CAR.RECORDING.cfg", "1");
                Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "GHOST_CAR.PLAYING.cfg", "1");
                Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "GHOST_CAR.LOAD.cfg", "1");
            }
            else
            {
                Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "GHOST_CAR.RECORDING.cfg", "0");
                Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "GHOST_CAR.PLAYING.cfg", "0");
                Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "GHOST_CAR.LOAD.cfg", "0");
            }




        }

        static public void Write_Tracks_param()//Tracks_utility _AC_Tracks)
        {
            Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "RACE.TRACK.cfg", Global_var.AC_Track.Tracks_info.ACname);
            Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "RACE.CONFIG_TRACK.cfg", "");
            //Extra Layout
            if (Global_var.AC_Track.Tracks_info.Layout[0] != "null")
            {
                foreach (var layout in Global_var.AC_Track.Tracks_info.Layout)
                {
                    if (layout != null)
                    {

                        Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "RACE.CONFIG_TRACK.cfg", layout);


                    }
                }
            }

            Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "REMOTE.ACTIVE.cfg", "0");

            if (Global_var.session_hotlap)
            {
                Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "SESSION_0.NAME.cfg", "Hotlap");
                Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "SESSION_0.TYPE.cfg", "4");
                Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "SESSION_0.DURATION_MINUTES.cfg", "0");
                Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "SESSION_0.SPAWN_SET.cfg", "HOTLAP_START");
            }
            else
            {
                Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "SESSION_0.NAME.cfg", "Practice");
                Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "SESSION_0.TYPE.cfg", "1");
                Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "SESSION_0.DURATION_MINUTES.cfg", "0");
                Global_var.Configurator_Parameters_ini_file.WriteString_v2("RACE", "SESSION_0.SPAWN_SET.cfg", "PIT");
            }



        }

        static public void Write_option()
        {
            /* Global_var.AC_desktopapp_ini_file.WriteString_v2("HEADER", "DESKTOP_SELECTED", "0");
             Global_var.AC_desktopapp_ini_file.WriteString_v2("DESK_0_FORM_RUNNING_TIME", "POSX", Global_var.RunningApp_POSY);
             Global_var.AC_desktopapp_ini_file.WriteString_v2("DESK_0_FORM_RUNNING_TIME", "POSY", Global_var.RunningApp_POSX);
             Global_var.AC_desktopapp_ini_file.WriteString_v2("DESK_0_FORM_RUNNING_TIME", "VISIBLE", Global_var.RunningApp_VISIBLE);
             Global_var.AC_desktopapp_ini_file.WriteString_v2("DESK_0_FORM_RUNNING_TIME", "BLOCKED", "0");
             Global_var.AC_desktopapp_ini_file.WriteString_v2("DESK_0_FORM_RUNNING_TIME", "SCALE", Global_var.RunningApp_SCALE);*/

            //ASSISTS
            //Assists.Write();

        }
    }
}
