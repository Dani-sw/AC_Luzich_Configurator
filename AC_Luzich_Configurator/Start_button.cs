using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using AC_Configurator_STDL.Controls;

namespace AC_Configurator_STDL
{
    public static class Start_button
    {

        public static void Set()
        {
            Global_var.GUI_Window.Start_btn_rect.MouseEnter += Start_btn_MouseEnter;
            Global_var.GUI_Window.Start_btn_rect.MouseLeave += Start_btn_MouseLeave;
            Global_var.GUI_Window.Start_btn_rect.MouseDown += Start_btn_MouseDown;
        }

        private static void Start_btn_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {//TODO understand WHY this resolve the" ini error missing value" on AC configurator restarting(with ac_core open)
                File.Delete(Global_var.AC_cfg_Path + "\\configurator_parameters.ini");
                Configurator_file.Write_Tracks_param();
                Configurator_file.Write_Car_param();

                Network.Copy_Files();

            }
            catch (Exception ex)
            {

                MessageBox_Custom.Show(ex.Message, "Application Loading error", MessageBox_Custom.MessageType.Error);
                LogWriter.Error_Trace(ex);
                Utility.OpenSessionsFolder(Global_var.App_path + Global_var.logs_path.Trim('\\'));
            }


        }


        private static void Start_btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Global_var.GUI_Window.Start_btn_img.Source = new BitmapImage(new Uri("pack://application:,,/Resources/start_OFF.png"));
        }

        private static void Start_btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Global_var.GUI_Window.Start_btn_img.Source = new BitmapImage(new Uri("pack://application:,,/Resources/start_ON.png"));
        }
    }
    
}
