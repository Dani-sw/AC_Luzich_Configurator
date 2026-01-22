using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ini.Net;

namespace AC_CoRe
{
 //TODO popup tray at start
 //TODO Debug and save messagebox "restart"
    public static class Global_var
    {
        static public AC_CoRe_GUI  _AC_Core_GUI= new AC_CoRe_GUI();
        static public IniFile configfile;


        //BBOX
        static public string BBox_StartBtn_index = "1";
        static public string BBox_StopBtn_index = "1";

        //static public Tracks_Functions AC_Track = new Tracks_Functions();

        static public int MonitorON = 1;
        static public string AC_cfg_Path = "";
        static public string AC_Path = "";
        static public string AC_car_Path = "";
        static public string AC_PRO = "";

        static public string AC_CarName = "";
        static public string AC_Trackname = "";
        static public string AC_TrackLayout = "";


        static public string Background_image = "";
        static public double Opacity = 0.2;
        static public string Style = "";

       
        public static int ESC_METHOD_MOUSEX = 2556;//960; //2556
        public static int ESC_METHOD_MOUSEY = 878;//878

        public static int AFTER_ACS_START_X = 0;
        public static int AFTER_ACS_START_Y = 0;

        static public Collection<string> SectionNames = new Collection<string>();
        static public List<KeyValuePair<string, string>> File_And_Path = new List<KeyValuePair<string, string>>();

        //LAP

        static public string BestLap_laptime="0";
        static public string BestLap_sector1 = "0";
        static public string BestLap_sector2 = "0";
        static public string BestLap_sector3 = "0";

        //ESC 
        public static int is_PushStartbtn_Show = 0;
        //static public string Esc_Enable = "0";


    }
}
