using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC_Configurator_STDL.Controls;

namespace AC_Configurator_STDL
{
    public static class GUI_controls
    {

        public static void Initilize()
        {
            Global_var.GUI_Window.FuelLoad_Sld.ValueChanged += FuelLoad_Sld_ValueChanged;
        }

        private static void FuelLoad_Sld_ValueChanged(object sender, double e)
        {
            int value = Convert.ToInt32(e);
            Global_var.GUI_Window.FuelLoad_Value_lbl.Content = value.ToString()+"L";
        }
    }
}
