using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace AC_CoRe.Dialog
{
    /// <summary>
    /// Interaction logic for advise_mousexy.xaml
    /// </summary>
    public partial class Advise_Mousexy : MetroWindow
    {
        public Advise_Mousexy()
        {
            InitializeComponent();
        }

        private void Advise_Mousexy_Closed(object sender, EventArgs e)
        {
            Mouse_XY.start_calibration();
        }
    }
}
