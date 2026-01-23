using AC_CoRe.Dialog;
using System.Windows.Forms;

namespace AC_CoRe
{
    public static  class Mouse_XY
    {
        private static  UserActivityHook actHook;

        public static  void set()
        {
            Global_var._AC_Core_GUI.Esc_calibrate_btn.Click += Esc_calibrate_btn_Click;
        }


        public static  void start_calibration()
        {
            actHook = new UserActivityHook();
            actHook.OnMouseActivity += new MouseEventHandler(MouseMoved);
            actHook.Start();

        }

        private static void MouseMoved(object sender, MouseEventArgs e)
        {
           
            if (e.Button == MouseButtons.Right)
            {
                actHook.Stop();

                Global_var.configfile.WriteString_v2("ESC_MOUSE_XY", "ESC_MOUSE_X", e.X.ToString());
                Global_var.configfile.WriteString_v2("ESC_MOUSE_XY", "ESC_MOUSE_Y", e.Y.ToString());

                //DarkMessageBox.Show("XY coordinates are been setted: RESTART AC CORE PLEASE!", "Coordinate Acquired",DarkMessageBox.Buttons.OK, DarkMessageBox.Icon.Info);
                MessageBox_Custom.Show("XY coordinates are been setted: RESTART AC CORE PLEASE!", "Coordinate Acquired", MessageBox_Custom.MessageType.Error);
                



            }
        }

        private static void Esc_calibrate_btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Advise_Mousexy mousexy_dialog = new Advise_Mousexy();
            mousexy_dialog.Owner = System.Windows.Application.Current.MainWindow;
            mousexy_dialog.Show();
            mousexy_dialog.Focus();
            
        }


    }
}
