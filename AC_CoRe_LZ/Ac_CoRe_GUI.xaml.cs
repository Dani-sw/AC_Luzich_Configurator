using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using ExpressionDark;
using System.Drawing;
using AC_CoRe.Dialog;
using System.Windows.Forms;

namespace AC_CoRe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AC_CoRe_GUI : MetroWindow
    {



        FileSystemWatcher watcher = new FileSystemWatcher();
       

        //HACK introdurre tcp per segnalare ac nel client e prevenire il doppio invio da configurator

       

        public AC_CoRe_GUI()
        {
            InitializeComponent();               
        }


       


        private void AC_CoRe_GUI_Loaded(object sender, RoutedEventArgs e)
        {

            Culture.set();

            Global_var._AC_Core_GUI = this;
            Title = Version.set();
            Preferences.Load();

            WPF_to_TrayBar.inizialize();

            Mouse_XY.set();
            BBox_GUI.set();
            BBox.InitJoystick_Dinput();
            //Bbox.Show_all();

            //non c'è più necessità perchè il controllo dell'esc viene fatta dalla routine della bbox
           // AC_ESC.Esc_Listener();


            try
            {
                watcher.Path = Global_var.AC_cfg_Path;
            }
            catch (Exception)
            {
                DarkMessageBox.Show("Incorrect Assetto Corsa Documents path\n", "Error", DarkMessageBox.Buttons.OK,DarkMessageBox.Icon.Error);
                this.Close();

            }

            watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite |
            NotifyFilters.Security | NotifyFilters.Size;

            watcher.Changed += Changed_a_file_event;
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

        }

    

        private void Changed_a_file_event(object sender, FileSystemEventArgs e)
        {
            
           
            if (e.Name == "configurator_parameters.ini")
            {
                watcher.EnableRaisingEvents = false;

                while (Utility.IsFileLocked(new FileInfo(Global_var.AC_cfg_Path + "\\configurator_parameters.ini")))
                {
                    //waiting time 
                    //essential for working, File.copy from server ,has to terminate on this file before Actions
                }

                

                Actions.DoTheDirtyJobOnFiles();

                /* if (!Global_var.NI_enable && !Global_var.Arduino_enable)
                 {
                     Actions.AC_Process_starts();          //from configurator    
                 }
                 else
                 {

                   Dispatcher.Invoke(() => CockpitButton.Show()); //arduino/ni pushbutton dialog

                 }*/
                if (Actions.IS_AC_OFF()) Dispatcher.Invoke(() => CockpitButton.Show()); //arduino/ni pushbutton dialog

                watcher.EnableRaisingEvents = true;

            }

            
        }

        private void AC_CoRe_GUI_Closed(object sender, EventArgs e)
        {

            System.Windows.Application.Current.Shutdown();

        }

        private void save_settings_btn_Click(object sender, RoutedEventArgs e)
        {
            Preferences.Save();
        }

       
    }
}
