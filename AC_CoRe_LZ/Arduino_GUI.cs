using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AC_CoRe
{
#if false
    public static class Arduino_GUI
    {
        private static ToggleSwitch _Arduino_Enable_sw = new ToggleSwitch();
        private static ToggleSwitch _Arduino_Debug_sw = new ToggleSwitch();
        private static Grid _Arduino_grid = new Grid();
        private static Grid _Arduino_debug_grid = new Grid();
        private static ComboBox _Arduino_comport_cmbbox = new ComboBox();



        public static void Load()
        {
            _Arduino_Enable_sw = Global_var._AC_Core_GUI.Arduino_Enable_sw;
            _Arduino_Debug_sw = Global_var._AC_Core_GUI.Arduino_Debug_sw;
            _Arduino_grid = Global_var._AC_Core_GUI.Arduino_grid;
            _Arduino_debug_grid = Global_var._AC_Core_GUI.Arduino_Debug_Grid;
            _Arduino_comport_cmbbox = Global_var._AC_Core_GUI.Arduino_Comport_cmbbox;

            _Arduino_Enable_sw.Toggled += _Arduino_Enable_sw_Toggled;
            _Arduino_Debug_sw.Toggled += _Arduino_Debug_sw_Toggled;
            _Arduino_grid.Loaded += _Arduino_grid_Loaded;
            _Arduino_grid_Loaded(null, null);
        }

        private static void _Arduino_grid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Global_var.Arduino_enable)
            {
                _Arduino_Enable_sw.IsOn = true;
            }
            else
            {
                _Arduino_Enable_sw.IsOn = false;
            }

            if (Global_var.Arduino_debug)
            {
                _Arduino_Debug_sw.IsOn = true;
            }
            else
            {
                _Arduino_Debug_sw.IsOn = false;
            }

            //_Arduino_comport_cmbbox.Items.Add(Global_var.Arduino_ComPort);


            _Arduino_Enable_sw_Toggled(null, null);
            _Arduino_Debug_sw_Toggled(null, null);

            set_COMPORT();
        }

        private static void _Arduino_Enable_sw_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_Arduino_Enable_sw.IsOn)
            {
                Global_var.Arduino_enable = true;
                _Arduino_debug_grid.IsEnabled = true;


            }
            else
            {
                Global_var.Arduino_enable = false;
                _Arduino_debug_grid.IsEnabled = false;
            }
        }

        private static void _Arduino_Debug_sw_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {   //TODO if debug on advise because ac don't start

            if (_Arduino_Debug_sw.IsOn)
            {
                Global_var.Arduino_debug = true;

                //_Ard.Opacity = 1.0;
                //_NI_Debug_testbutton.Opacity = 1.0;
            }
            else
            {
                Global_var.Arduino_debug = false;

                //_NI_Debug_lbl.Opacity = 0.1;
                //_NI_Debug_testbutton.Opacity = 0.2;
            }

        }

        public static void open()
        {
            WPF_to_TrayBar.open_GUI();
            Global_var._AC_Core_GUI.tabControl_main.SelectedIndex = 1;
        }

        public static void set_COMPORT()
        {

            try
            {

                foreach (var coms_port in SerialPort.GetPortNames())
                {
                    _Arduino_comport_cmbbox.Items.Add(coms_port);


                }

                for (int i = 0; i < _Arduino_comport_cmbbox.Items.Count; i++)
                {
                    if (Global_var.Arduino_ComPort == _Arduino_comport_cmbbox.Items[i].ToString())
                    {
                        _Arduino_comport_cmbbox.SelectedIndex = i;

                    }

                }
            }
            catch (Exception ex)
            {

                App_Messages.error_message(ex);
            }

        }

    } 
#endif
}
