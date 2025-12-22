using AC_Configurator_STDL.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AC_Configurator_STDL
{
    public partial class Main_GUI : Window
    {

        Tracks_utility AC_Tracks_Function = new Tracks_utility();
        Cars_utility AC_Cars_Functions = new Cars_utility();

        public Main_GUI()
        {
            try
            {
                InitializeComponent();
                TitleBar_Color.Background = new SolidColorBrush(Color.FromRgb(96,8,6));

            }

            catch (System.Exception ex)
            {
                MessageBox_Custom.Show(ex.Message, "Application inizialize error", MessageBox_Custom.MessageType.Error);
                LogWriter.Error_Trace(ex);
            }

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                Global_var.GUI_Window = this;
                GUI_controls.Initilize();

                Preferences.Load();
                Start_button.initialize();

                List<Tracks> AC_Tracks_List = AC_Tracks_Function.Fill_AC_Tracks_List();
                List<Cars> AC_Cars_List = AC_Cars_Functions.Fill_AC_Car_List();

                AC_Tracks_Function.Fill_Tracks_Listbox_control(Track_Selection_lst);
                AC_Cars_Functions.Fill_Cars_Listbox(Car_Selection_lst, CarSkin_Selection_lst);

               
                //this.BorderBrush = Brushes.Crimson; // o new SolidColorBrush(Colors.Red)
                //this.BorderThickness = new Thickness(1);


            }

            catch (Exception ex)
            {
                MessageBox_Custom.Show(ex.Message, "Application Loading error", MessageBox_Custom.MessageType.Error);
                LogWriter.Error_Trace(ex);
                Utility.OpenSessionsFolder(Global_var.App_path + Global_var.logs_path.Trim('\\'));

            }
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

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }

 
        
    }
}