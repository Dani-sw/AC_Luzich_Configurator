using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AC_Configurator_STDL.Controls;


namespace AC_Configurator_STDL
{
    public static class Utility
    {
        public static void OpenSessionsFolder(string appFolder)
        {
            if (Directory.Exists(appFolder))
            {
                // Apre Esplora File sulla cartella
                Process.Start("explorer.exe", appFolder);
            }
            else
            {
                MessageBox_Custom.Show(appFolder + " not exists!", "Utility class error", MessageBox_Custom.MessageType.Error);
            }
        }

        public static void Check_dblInstance()
        {

            // Nome del processo corrente
            string processName = Process.GetCurrentProcess().ProcessName;

            // Conta quante istanze esistono dello stesso EXE
            int count = Process.GetProcessesByName("TelemetryLab").Length;

            if (count > 1)
            {
                MessageBox_Custom.Show("The application is already running.", "Double instance", MessageBox_Custom.MessageType.Error);
                LogWriter.Message_Trace("Application is already running; the second launched instance has been closed.");
                Application.Current.Shutdown();  // chiudi tutta l'app
            }
        }

        public static int ParseLaptime(string laptime)
        {
            try
            {
                var parts = laptime.Split('_');
                if (parts.Length == 3)
                {
                    int minutes = int.Parse(parts[0]);
                    int seconds = int.Parse(parts[1]);
                    int milliseconds = int.Parse(parts[2]);

                    return (minutes * 60 * 1000) + (seconds * 1000) + milliseconds;
                }
            }
            catch { }
            return int.MaxValue; // In caso di errore, metti il file alla fine
        }

        public static string tostring_timeformat(string laptime)
        {
            string[] _laptime;
            string laptime_timeformat;
            try
            {
                _laptime = laptime.Split('_');
                laptime_timeformat = _laptime[0] + ":" + _laptime[1] + "." + _laptime[2];
                return laptime_timeformat;
            }
            catch { }
            return "-:--.---";
        }
    }
}
