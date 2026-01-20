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
            Global_var.GUI_Window.TyreCond_New_chkbox.CheckedClick += RaceCond_CheckedChanged;
            Global_var.GUI_Window.TyreCond_Used_chkbox.CheckedClick += RaceCond_CheckedChanged;
            Global_var.GUI_Window.TyreWear_ON_chkbox.CheckedClick += RaceCond_CheckedChanged;
            Global_var.GUI_Window.TyreWear_OFF_chkbox.CheckedClick += RaceCond_CheckedChanged;
            Global_var.GUI_Window.FuelCons_ON_chkbox.CheckedClick += RaceCond_CheckedChanged;
            Global_var.GUI_Window.FuelCons_OFF_chkbox.CheckedClick += RaceCond_CheckedChanged;
            Global_var.GUI_Window.TrackGrip_Race_chkbox.CheckedClick += RaceCond_CheckedChanged;
            Global_var.GUI_Window.TrackGrip_Green_chkbox.CheckedClick += RaceCond_CheckedChanged;
            Global_var.GUI_Window.IdealLine_ON_chkbox.CheckedClick += RaceCond_CheckedChanged;
            Global_var.GUI_Window.IdealLine_OFF_chkbox.CheckedClick += RaceCond_CheckedChanged;

            Global_var.GUI_Window.FuelLoad_Sld.ValueChanged += RaceCond_sld_ValueChanged;
            Global_var.GUI_Window.StabiltyCtrl_Sld.ValueChanged += RaceCond_sld_ValueChanged;

            Race_Condtions.Fuel_Load=Convert.ToInt32(Global_var.GUI_Window.FuelLoad_Sld.Value);


        }

        private static void RaceCond_CheckedChanged(object sender, bool e)
        {
            Race_Condtions.Checkboxes(sender, e);

        }

      
       
        private static void RaceCond_sld_ValueChanged(object sender, double e)
        {

            Race_Condtions.Sliders(sender, e);
        }
    }
}
