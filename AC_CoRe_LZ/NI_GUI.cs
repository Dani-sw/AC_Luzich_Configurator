using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace AC_CoRe
{
#if false
    public static class NI_GUI
    {
        private static TabControl _Tabcontrol_Main = new TabControl();
        private static ToggleSwitch _NI_Enable_sw = new ToggleSwitch();
        private static ToggleSwitch _NI_Debug_sw = new ToggleSwitch();
        private static Grid _NI_Debug_grid = new Grid();
        private static Grid _NI_Tab_grid = new Grid();
        private static Label _NI_Debug_lbl = new Label();
        private static Rectangle _NI_Debug_testbutton = new Rectangle();


        public static void Load()
        {
            _NI_Enable_sw = Global_var._AC_Core_GUI.NI_Enable_sw;
            _NI_Debug_sw = Global_var._AC_Core_GUI.NI_Debug_sw;
            _NI_Debug_grid = Global_var._AC_Core_GUI.NI_Debug_Grid;
            _NI_Debug_lbl = Global_var._AC_Core_GUI.NI_Debug_lbl;
            _NI_Debug_testbutton = Global_var._AC_Core_GUI.NI_Debug_testbutton;
            //_NI_Tab_grid = Global_var._AC_Core_GUI.NI_Grid;
            _Tabcontrol_Main = Global_var._AC_Core_GUI.tabControl_main;
            _Tabcontrol_Main.Loaded += _NI_Tab_grid_Loaded;

            _NI_Debug_sw.Toggled += NI_Debug_sw_Toggled;
            _NI_Enable_sw.Toggled += NI_Enable_sw_Toggled;


        }

        public static void open()
        {
            WPF_to_TrayBar.open_GUI();
            _Tabcontrol_Main.SelectedIndex = 0;
        }

        private static void _NI_Tab_grid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

            if (Global_var.NI_enable)
            {
                _NI_Enable_sw.IsOn = true;
            }
            else
            {
                _NI_Enable_sw.IsOn = false;
            }
            NI_Enable_sw_Toggled(null, null);

        }

        private static void NI_Enable_sw_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {

            if (_NI_Enable_sw.IsOn)
            {
                Global_var.NI_enable = true;
                _NI_Debug_grid.IsEnabled = true;


                _NI_Debug_lbl.Opacity = 0.1;
                _NI_Debug_testbutton.Opacity = 0.2;

            }
            else
            {
                Global_var.NI_enable = false;
                _NI_Debug_grid.IsEnabled = false;
                _NI_Debug_sw.IsOn = false;

                _NI_Debug_lbl.Opacity = 0.1;
                _NI_Debug_testbutton.Opacity = 0.2;

            }
        }

        private static void NI_Debug_sw_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {

            if (_NI_Debug_sw.IsOn)
            {
                Global_var.NI_debug = true;

                _NI_Debug_lbl.Opacity = 1.0;
                _NI_Debug_testbutton.Opacity = 1.0;
            }
            else
            {
                Global_var.NI_debug = false;

                _NI_Debug_lbl.Opacity = 0.1;
                _NI_Debug_testbutton.Opacity = 0.2;
            }
        }
    } 
#endif
}
