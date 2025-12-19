using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Configurator_STD
{
    public class Cars
    {
        public string Name { get; set; } = "";
        public string ACname = "";
        public string[] Car_skins = new string[100];

        public string Brand = "";
        public string Bhp = "";
        public string Torque = "";
        public string Weight = "";
        public string TopSpeed = "";
        public string Acceleration = "";
        public string PWratio = "";
        public string Range = "";

        public string[] Car_image_path = new string[100];
        public string Car_Badge_path = "";



        public string AutoBlip = "0";
        public string Auto_Shifter = "0";
        public string IdealLine = "0";
    }
}
