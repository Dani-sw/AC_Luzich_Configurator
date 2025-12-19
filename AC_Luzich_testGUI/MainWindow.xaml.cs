using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using AC_Configurator_STD;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {

        Tracks_utility AC_Tracks_Function = new Tracks_utility();
        Cars_utility AC_Cars_Functions = new Cars_utility();

        public MainWindow()
        {
            InitializeComponent();
           
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Global_var.GUI_Window = this;

            List<Tracks> AC_Tracks_List = AC_Tracks_Function.Fill_AC_Tracks_List();
            List<Cars> AC_Cars_List = AC_Cars_Functions.Fill_AC_Car_List();

            AC_Tracks_Function.Fill_Tracks_Listbox_control(Track_Selection_lst);
            AC_Cars_Functions.Fill_Cars_Listbox(Car_Selection_lst, CarSkin_Selection_lst);
        }

        // Permette di trascinare la finestra dalla barra del titolo
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                // Doppio click per massimizzare/ripristinare
                MaximizeRestore();
            }
            else
            {
                // Trascina la finestra
                DragMove();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            MaximizeRestore();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MaximizeRestore()
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            startOn_btn_img.Visibility = Visibility.Visible;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            startOn_btn_img.Visibility = Visibility.Hidden;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(Global_var.AC_CarSkin + " " + Global_var._AC_Cars.ACname+"  "+Global_var.AC_Track.Tracks_info.ACname);
        }
    }
}