using AC_Configurator_STDL.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Configurator_STDL
{
    public static class Race_Condtions
    {

        public static int Tyre_Wear { get; set; }
        public static int Tyre_Cond { get; set; }
        public static int Fuel_Cons { get; set; }
        public static int Track_Grip { get; set; }
        public static int Fuel_Load { get; set; }



        public static void checkboxes(object sender , bool e)
        {
            Custom_Checkbox RaceCond_chkbox = sender as Custom_Checkbox;

            //RaceCond_chkbox.isDeselectionDisabled = true;
            switch (RaceCond_chkbox.Name)
            {
                case "TyreCond_New_chkbox":
                    Global_var.GUI_Window.TyreCond_Used_chkbox.IsChecked = false;
                    Tyre_Cond = 1;
                    break;
                case "TyreCond_Used_chkbox":
                    Global_var.GUI_Window.TyreCond_New_chkbox.IsChecked = false;
                    Tyre_Cond = 0;
                    break;
                case "TyreWear_ON_chkbox":
                    Global_var.GUI_Window.TyreWear_OFF_chkbox.IsChecked = false;
                    Tyre_Wear = 1;
                    break;
                case "TyreWear_OFF_chkbox":
                    Global_var.GUI_Window.TyreWear_ON_chkbox.IsChecked = false;
                    Tyre_Wear = 0;
                    break;
                case "FuelCons_ON_chkbox":
                    Global_var.GUI_Window.FuelCons_OFF_chkbox.IsChecked = false;
                    Fuel_Cons = 1;
                    break;
                case "FuelCons_OFF_chkbox":
                    Global_var.GUI_Window.FuelCons_ON_chkbox.IsChecked = false;
                    Fuel_Cons = 0;
                    break;
                case "TrackGrip_Race_chkbox":
                    Global_var.GUI_Window.TrackGrip_Green_chkbox.IsChecked = false;
                    Track_Grip = 1;
                    break;
                case "TrackGrip_Green_chkbox":
                    Global_var.GUI_Window.TrackGrip_Race_chkbox.IsChecked = false;
                    Track_Grip = 0;
                    break;
            }

        }


        public static void Fuel_Load_sld(object sender,double e)
        {
            Fuel_Load = Convert.ToInt32(e);
            Global_var.GUI_Window.FuelLoad_Value_lbl.Content = Fuel_Load.ToString() + "L";

        }


    }
}
