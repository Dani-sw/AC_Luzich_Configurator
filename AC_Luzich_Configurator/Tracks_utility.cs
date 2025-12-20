
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AC_Configurator_STDL
{
    public class Tracks_utility
    {

        public Tracks Tracks_info = new Tracks();
        public List<Tracks> AC_Tracks_List = new List<Tracks>();

        public List<Tracks> Fill_AC_Tracks_List()
        {
            string[] dirs = Directory.GetDirectories(Global_var.AC_Trackpath, "*", SearchOption.TopDirectoryOnly);

            foreach (string dir in dirs)
            {
                Tracks AC_track = new Tracks();
                //tracks ac name fill
                var dirName = new DirectoryInfo(dir).Name;
                AC_track.ACname = dirName;

                //Read json file for Name/Slot
                try
                {
                    if (File.Exists(Global_var.AC_Trackpath + AC_track.ACname + @"\\ui\ui_track.json"))
                    {
                        using (StreamReader file = File.OpenText(Global_var.AC_Trackpath + AC_track.ACname + @"\\ui\ui_track.json"))
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            JObject ac_ui_json = (JObject)JToken.ReadFrom(reader);
                            AC_track.Name = (string)ac_ui_json["name"];
                            AC_track.Desc = (string)ac_ui_json["description"];
                            AC_track.Country = (string)ac_ui_json["country"];
                            AC_track.City = (string)ac_ui_json["city"];
                            AC_track.Length = (string)ac_ui_json["length"];
                            AC_track.Width = (string)ac_ui_json["width"];
                            AC_track.Slots = (string)ac_ui_json["pitboxes"];
                            AC_track.Run = (string)ac_ui_json["run"];

                            //DarkMessageBox.Show(Slots[count], "Output");
                        }
                        AC_track.Layout[0] = "null";
                        AC_track.Tracks_image_path = Global_var.AC_Trackpath + AC_track.ACname + @"\\ui\preview.png";
                        AC_track.Tracks_Layout_path = Global_var.AC_Trackpath + AC_track.ACname + @"\\ui\outline.png";
                        AC_Tracks_List.Add(AC_track);

                    }

                    else
                    {
                        //Read Extra Layout

                        string[] layout_dirs = Directory.GetDirectories(Global_var.AC_Trackpath + AC_track.ACname + @"\ui", "*", SearchOption.TopDirectoryOnly);
                        int count2 = 0;


                        foreach (var layout_name in layout_dirs)
                        {
                            Tracks temp = new Tracks();
                            temp.ACname = AC_track.ACname;
                            temp.Layout[count2] = new DirectoryInfo(layout_name).Name;


                            using (StreamReader file = File.OpenText(Global_var.AC_Trackpath + temp.ACname + @"\\ui\" + temp.Layout[count2] + @"\\ui_track.json"))
                            using (JsonTextReader reader = new JsonTextReader(file))
                            {
                                JObject ac_ui_json = (JObject)JToken.ReadFrom(reader);
                                temp.Name = (string)ac_ui_json["name"];
                                temp.Desc = (string)ac_ui_json["description"];
                                temp.Country = (string)ac_ui_json["country"];
                                temp.City = (string)ac_ui_json["city"];
                                temp.Length = (string)ac_ui_json["length"];
                                temp.Width = (string)ac_ui_json["width"];
                                temp.Slots = (string)ac_ui_json["pitboxes"];
                                temp.Run = (string)ac_ui_json["run"];

                                temp.Tracks_image_path = Global_var.AC_PATH + @"\content\tracks\" + temp.ACname + "\\ui\\" + temp.Layout[count2] + "\\preview.png";
                                temp.Tracks_Layout_path = Global_var.AC_PATH + @"\content\tracks\" + temp.ACname + "\\ui\\" + temp.Layout[count2] + "\\outline.png";
                            }

                            count2++;
                            AC_Tracks_List.Add(temp);
                        }

                    }

                }
                catch (Exception e)
                {
                    //App_Messages.error_message(e);

                }
            }
            AC_Tracks_List = AC_Tracks_List.OrderBy(Cars => Cars.Name).ToList();
            return AC_Tracks_List;
        }

        public void Fill_Tracks_Listbox_control(ListBox Tracks_Listbox)
        {
            // Aggiungi direttamente gli oggetti Tracks alla ListBox
            foreach (var AC_track in AC_Tracks_List)
            {
                //style nello xaml
                Tracks_Listbox.Items.Add(AC_track);
            }

            // Configura come visualizzare gli oggetti Tracks
            Tracks_Listbox.DisplayMemberPath = "Name"; // Mostra la proprietà Name

            // Seleziona il primo item
            if (Tracks_Listbox.Items.Count > 0)
            {
                Tracks_Listbox.SelectedIndex = 5;
            }

            // Aggiungi l'evento SelectionChanged alla ListBox
            Tracks_Listbox.SelectionChanged += Tracks_ListBox_SelectionChanged;

            // Seleziona il primo item
            if (Tracks_Listbox.Items.Count > 0)
            {
                Tracks_Listbox.SelectedIndex = 0;
                Tracks_ListBox_SelectionChanged(Tracks_Listbox, null);
            }

            Tracks_Listbox.ScrollIntoView(Tracks_Listbox.Items[0]); // scrollup for w10
        }

        private void Tracks_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (listBox.SelectedItem != null)
            {
                Tracks_info = listBox.SelectedItem as Tracks;
                Global_var.AC_Track.Tracks_info = Tracks_info;

                try
                {
                    BitmapImage track_PreviewImage = new BitmapImage();
                    BitmapImage track_LayoutImage = new BitmapImage();

                    track_PreviewImage.BeginInit();
                    track_LayoutImage.BeginInit();
                    track_PreviewImage.UriSource = new Uri(Tracks_info.Tracks_image_path, UriKind.Absolute);
                    track_LayoutImage.UriSource = new Uri(Tracks_info.Tracks_Layout_path, UriKind.Absolute);
                    track_PreviewImage.CacheOption = BitmapCacheOption.OnLoad;
                    track_LayoutImage.CacheOption = BitmapCacheOption.OnLoad;
                    track_PreviewImage.EndInit();
                    track_LayoutImage.EndInit();

                    Global_var.GUI_Window.tracks_image_box.Source = track_PreviewImage;
                    Global_var.GUI_Window.tracks_Layout_box.Source = track_LayoutImage;
                }
                catch (Exception ex)
                {
                    //App_Messages.error_message(ex);
                }
            }
        }

    }
}
