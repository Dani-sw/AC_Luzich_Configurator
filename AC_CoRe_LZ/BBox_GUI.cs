using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace AC_CoRe
{
    public static class BBox_GUI
    {
        public static SolidColorBrush Brush_Blue { get; private set; } = new SolidColorBrush(Color.FromArgb(255, 0, 15, 255));
        public static SolidColorBrush Brush_Trasparent { get; private set; } = new SolidColorBrush(Color.FromArgb(0, 0, 15, 255));
        public static SolidColorBrush Brush_Red { get; private set; } = new SolidColorBrush(Colors.Red);
        public static SolidColorBrush Brush_Green { get; private set; } = new SolidColorBrush(Colors.LimeGreen);


        public static int SetBtn_Flag { get; set; } = 0; //0 stop 1 start


        public static IEnumerable<Rectangle> bboxRects { get; private set; }

        public static void btn_stroke_blue()
        {

          
            foreach (Rectangle rect in bboxRects)
            {

                rect.Stroke = Brush_Blue;
                ToolTipService.SetInitialShowDelay(rect, 0);
                if (SetBtn_Flag == 1)
                {
                    rect.ToolTip = "click to select the Start button";
                }
                else
                {
                    rect.ToolTip = "click to select the Stop button";
                }
            }


        }



       private static void BBox_Mapping()
        {
            //da schema windows bbox , pero poi nell'app bisogna scalare di uno perche i joybutton partono da 0

            Global_var._AC_Core_GUI.bbox_2F_1_rect.Tag = 1;
            Global_var._AC_Core_GUI.bbox_24h_2_rect.Tag = 2;
            Global_var._AC_Core_GUI.bbox_Cockpit_3_rect.Tag = 3;
            Global_var._AC_Core_GUI.bbox_Master_4_rect.Tag = 4;
            Global_var._AC_Core_GUI.bbox_Ign_5_rect.Tag = 5;
            Global_var._AC_Core_GUI.bbox_Crank_6_rect.Tag = 6;
            Global_var._AC_Core_GUI.bbox_M2_7_rect.Tag = 7;
            Global_var._AC_Core_GUI.bbox_Pbx_8_rect.Tag = 8;
            Global_var._AC_Core_GUI.bbox_M1_9_rect.Tag = 9;
            Global_var._AC_Core_GUI.bbox_Defrost_10_rect.Tag = 10;
            Global_var._AC_Core_GUI.bbox_M3_11_rect.Tag = 11;
            Global_var._AC_Core_GUI.bbox_Std_12_rect.Tag = 12;
            Global_var._AC_Core_GUI.bbox_Marshall_13_rect.Tag = 13;
            Global_var._AC_Core_GUI.bbox_Start_14_rect.Tag = 14;
            Global_var._AC_Core_GUI.bbox_Fire_15_rect.Tag = 15;
            //16 missing WTF!?
            Global_var._AC_Core_GUI.bbox_Wet_17_rect.Tag = 17;
            Global_var._AC_Core_GUI.bbox_Rain_18_rect.Tag = 18;
        }



        public static void reset()
        {           
            foreach (Rectangle bbox_button in bboxRects)
            {
                bbox_button.Stroke = Brush_Trasparent;

                if (bbox_button.Tag.ToString() == Global_var.BBox_StartBtn_index)
                {
                    bbox_button.Stroke = Brush_Green;
                }
                if(bbox_button.Tag.ToString() == Global_var.BBox_StopBtn_index)
                { 
                    bbox_button.Stroke = Brush_Red;
                }

                bbox_button.MouseDown -= Rect_MouseDown;
                bbox_button.ToolTip = "";
            }

        }


        public static void set()
        {

            bboxRects = Global_var._AC_Core_GUI.BBox_btn_grid.Children.OfType<Rectangle>();

           
            Global_var._AC_Core_GUI.BBox_StartSim_btn.Click += BBox_SetSim_btn_Click;
            Global_var._AC_Core_GUI.BBox_StopSim_btn.Click += BBox_SetSim_btn_Click;

            BBox_Mapping();

            reset();
        }

        private static void BBox_SetSim_btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button Set_btn=sender as Button;
            if (Set_btn.Name == "BBox_StartSim_btn")
            {
                SetBtn_Flag = 1;
            }
            else
            {
                SetBtn_Flag = 0;
            }
            btn_stroke_blue();
            foreach (Rectangle rect in bboxRects)
            {
                rect.MouseDown += Rect_MouseDown;
            }
        }

        private static void Rect_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Rectangle BBox_btn_rect = sender as Rectangle;
            if (SetBtn_Flag == 1)
            {
                BBox_btn_rect.Stroke = Brush_Green;
                Global_var.BBox_StartBtn_index = BBox_btn_rect.Tag.ToString();
                BBox.Bbox_StartIndex = Convert.ToInt32(Global_var.BBox_StartBtn_index) - 1;
            }
            else
            {
                BBox_btn_rect.Stroke = Brush_Red;
                Global_var.BBox_StopBtn_index = BBox_btn_rect.Tag.ToString();
                BBox.Bbox_StopIndex = Convert.ToInt32(Global_var.BBox_StopBtn_index) - 1;
            }

            reset();
        }
    }
}
