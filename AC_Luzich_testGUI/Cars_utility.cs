using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Windows.Media.Imaging;


namespace AC_Configurator_STD
{
    class Cars_utility
    {

        public Cars AC_Car = new Cars();
        public List<Cars> AC_Cars_List = new List<Cars>();
        public List<Cars> AC_Cars_Search_List = new List<Cars>();
        //public List<AC_Client_TAB> _AC_Client_List = new List<AC_Client_TAB>();

        //private ListBox _Skins_Listbox = new ListBox();


        public List<Cars> Fill_AC_Car_List()
        {


            string[] dirs = Directory.GetDirectories(Global_var.AC_Carpath, "*", SearchOption.TopDirectoryOnly);


            foreach (string dir in dirs)
            {
                Cars AC_Cars = new Cars();
                //tracks ac name fill
                var dirName = new DirectoryInfo(dir).Name;
                AC_Cars.ACname = dirName;

                //Read json file for Name/Slot
                try
                {
                    if (File.Exists(Global_var.AC_Carpath + AC_Cars.ACname + @"\\ui\ui_car.json"))
                    {
                        using (StreamReader file = File.OpenText(Global_var.AC_Carpath + AC_Cars.ACname + @"\\ui\ui_car.json"))
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            JObject ac_ui_json = (JObject)JToken.ReadFrom(reader);
                            AC_Cars.Name = (string)ac_ui_json["name"];
                            AC_Cars.Brand = (string)ac_ui_json["brand"];
                            AC_Cars.Bhp = (string)ac_ui_json["specs"]["bhp"];
                            AC_Cars.Torque = (string)ac_ui_json["specs"]["torque"];
                            AC_Cars.Weight = (string)ac_ui_json["specs"]["weight"];
                            AC_Cars.TopSpeed = (string)ac_ui_json["specs"]["topspeed"];
                            AC_Cars.Acceleration = (string)ac_ui_json["specs"]["acceleration"];
                            AC_Cars.PWratio = (string)ac_ui_json["specs"]["pwratio"];
                            AC_Cars.Range = (string)ac_ui_json["specs"]["range"];


                        }

                        //AC_track.Tracks_image_path = Global_var.AC_PATH + @"\\content\tracks\" + AC_track.ACname + @"\\ui\preview.png";
                        AC_Cars.Car_Badge_path = Global_var.AC_Carpath + AC_Cars.ACname + @"\\ui\badge.png";
                       

                        Car_Skins_List(AC_Cars); //for each car


                        AC_Cars_List.Add(AC_Cars);

                    }
                }
                catch (Exception e)
                {
                    //App_Messages.error_message(e);

                }
            }
            AC_Cars_List = AC_Cars_List.OrderBy(Cars => Cars.Name).ToList();
            //AC_Cars_List = AC_Cars_List.Where(Cars => Cars.Brand.Contains("B")).ToList();


            return AC_Cars_List; ;
        }

        public void Fill_Cars_Listbox(ListBox Cars_Listbox, ListBox Skins_Listbox)
        {

            foreach (var AC_Car in AC_Cars_List)
            {

                Cars_Listbox.Items.Add(AC_Car);

            }

            Cars_Listbox.DisplayMemberPath = "Name";

            Cars_Listbox.ScrollIntoView(Cars_Listbox.Items[0]); //scrollup for w10

            Cars_Listbox.SelectionChanged += Cars_radio_checked;

            if (Cars_Listbox.Items.Count > 0)
            {
                Cars_Listbox.SelectedIndex = 0;
                Cars_radio_checked(Cars_Listbox, null);
            }

        }

        //Use for searching list

        public void Car_Skins_List(Cars _AC_Cars)
        {
            string[] dirs = Directory.GetDirectories(Global_var.AC_Carpath + _AC_Cars.ACname + "\\skins", "*", SearchOption.TopDirectoryOnly);

            int count = 0;

            foreach (string Skin in dirs)
            {
                var Skin_Name = new DirectoryInfo(Skin).Name;
                _AC_Cars.Car_skins[count] = Skin_Name;
                count++;
            }
        }

        public void Cars_radio_checked(object sender, RoutedEventArgs e)
        {
            //bisogna disregistrare il metodo perchè con il clear si attiva subito il selectionchanged e succede che arriva al metodo
            //Cars_Skin_checked alla if=....
            //senza il selectitem dando errore object not set...null 
            Global_var.GUI_Window.CarSkin_Selection_lst.SelectionChanged -= Cars_Skin_checked;
            Global_var.GUI_Window.CarSkin_Selection_lst.Items.Clear(); //qui deve stare
            
            ListBox Cars_list = sender as ListBox;
            AC_Car = Cars_list.SelectedItem as Cars;
           
            int count = 0;

            


            //_Skins_Listbox.

            foreach (string Car_Skin in AC_Car.Car_skins)
            {
                if (AC_Car.Car_skins[count] != null)
                {
                    /* RadioButton Skin_rb = new RadioButton();
                     Skin_rb.Content = AC_Car.Car_skins[count];
                     Skin_rb.Checked += Cars_Skin_checked;
                     Skin_rb.Tag = AC_Car; //oggetto car


                     Skin_rb.Height = 25; //to remove the under highlights
                     Skin_rb.Margin = new Thickness(-2, 0, 0, 0);//to remove the under highlights*/



                    Global_var.GUI_Window.CarSkin_Selection_lst.Items.Add(AC_Car.Car_skins[count]);
                    AC_Car.Car_image_path[count] = Global_var.AC_Carpath + AC_Car.ACname + "\\skins\\" + AC_Car.Car_skins[count] + "\\preview.jpg";

                    //first start checked and start tracks image
                  /*if (_Skins_Listbox.Items.Count == 1)
                    {
                       // _Skins_Listbox.Items(0);
                        Cars_Skin_checked(sender, null);
                    }*/

                    count++;

                }

            }

            Global_var.GUI_Window.CarSkin_Selection_lst.Tag = AC_Car;

            Global_var.GUI_Window.CarSkin_Selection_lst.ScrollIntoView(Global_var.GUI_Window.CarSkin_Selection_lst.Items[0]); //scrollup for w10
            Global_var.GUI_Window.CarSkin_Selection_lst.SelectionChanged += Cars_Skin_checked;


            if (Global_var.GUI_Window.CarSkin_Selection_lst.Items.Count > 0)
            {
                Global_var.GUI_Window.CarSkin_Selection_lst.SelectedIndex = 0;
                Cars_Skin_checked(Global_var.GUI_Window.CarSkin_Selection_lst,null);
            }

            //Global_var.GUI_Window.rank_listBox.Items.Clear();
            //Global_var.Lap_list_order.Clear();
            //CSV.Load();

            //TODO new button
            //Global_var.GUI_Window.Start.IsEnabled = false;
            // Global_var.GUI_Window.Start_Menu.StartLanRace_IsEnable = false;

        }

        private void Cars_Skin_checked(object sender, RoutedEventArgs e)
        {
            ListBox Skin_listbox = sender as ListBox;
            AC_Car = Skin_listbox.Tag as Cars;
            int count = 0;

            foreach (var Car_skin in AC_Car.Car_skins)
            {
                try
                {
                    if (Car_skin == Skin_listbox.SelectedItem.ToString())
                    {

                        /*Uri Car_Skin_resourceUri = new Uri(AC_Car.Car_image_path[count], UriKind.Absolute);
                        Global_var.GUI_Window.CarsSkin_Preview_Image.Source = new BitmapImage(Car_Skin_resourceUri);*/

                        BitmapImage CarSkin_PreviewImage = new BitmapImage();


                        // BitmapImage.UriSource must be in a BeginInit/EndInit block

                        //new method for release the bitmap , before it still locked and the directory is impossible to move
                        CarSkin_PreviewImage.BeginInit();
                        CarSkin_PreviewImage.UriSource = new Uri(AC_Car.Car_image_path[count], UriKind.Absolute);
                        CarSkin_PreviewImage.CacheOption = BitmapCacheOption.OnLoad; //this is the key
                        CarSkin_PreviewImage.EndInit();

                        Global_var.GUI_Window.CarsSkin_Preview_Image.Source = CarSkin_PreviewImage;


                        Global_var._AC_Cars = AC_Car;
                        Global_var.AC_CarSkin = Car_skin;

                    }
                    count++;
                }
                catch (Exception ex)
                {
                  
                    
                }
            }

            // Global_var.GUI_Window.Start.IsEnabled = false;
            //Global_var.GUI_Window.Start_Menu.StartLanRace_IsEnable = false;

        }



    }
}
