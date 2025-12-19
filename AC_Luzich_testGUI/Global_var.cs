
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1;


namespace AC_Configurator_STD
{
     public static class Global_var
    {
        static public MainWindow GUI_Window = new MainWindow(); 
        static public Tracks_utility AC_Track = new Tracks_utility();
        static public Cars _AC_Cars = new Cars();
        

        static public string AC_PRO = "0";
        static public string AC_PATH = @"C:\AC PRO";
        static public string AC_cfg_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Assetto Corsa\cfg";

       
        static public string AC_Carpath = @"C:\AC PRO\content\cars\";
        static public string AC_Trackpath = @"C:\AC PRO\content\tracks\";
        static public string AC_Server_Carpath = "";
        static public string AC_Server_Trackpath = "";
        static public string AC_Replaypath = "";
        static public string AC_CarSkin = "";

       

        static public string RunningApp_POSY = "";
        static public string RunningApp_POSX = "";
        static public string RunningApp_VISIBLE = "";
        static public string RunningApp_SCALE = "";

        //ASSISTS
        static public string Ideal_line = "1";
        static public string Stability_control = "50";

        //SESSIONS
        static public string GhostCar= "0";
        static public bool session_practice = false;
        static public bool session_hotlap = true;



        public static string start_time = "0";
        public static string end_time = "0";



        //LAPTIME
        public static bool AC_isON = false;

        public static string Best_laptime = "--.--.---";
        public static int iBest_laptime = 0;

        public static string[] Best_sectors = new string[3];
        public static string[] sectors = new string[3];

        public static int[] iBest_sectors = new int[3];
        public static int[] isectors = new int[3];



        //USER
        public static string driver_name { get; set; }

        //IP CLIENT
        static public string IP_PC_CLient = "0.0.0.0";

    }
}
