using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AC_Configurator_STDL
{
    public class Tracks
    {
        // PROPRIETÀ (funziona con DisplayMemberPath)
        public string Name { get; set; } = "";
        public string ACname = "";
        public string[] Layout = new string[10];
        public string Desc = "";
        public string Country = "";
        public string City = "";
        public string Length = "";
        public string Width = "";
        public string Slots = "";
        public string Run = "";
        public string Record = "";
        public string Tracks_image_path = "";
        public string Tracks_Layout_path = "";
        public BitmapImage Tracks_image = new BitmapImage();
        public BitmapImage Tracks_image_layout = new BitmapImage();

    }

     
}
