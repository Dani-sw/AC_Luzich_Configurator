using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Timers;
using System.Net.Sockets;
using ExpressionDark;
using System.Windows.Media;
using System.Windows.Threading;

namespace AC_CoRe
{
#if false
    public static class Arduino_Switch
    {
        private static SerialPort ArduinoSerialPort = new SerialPort();
        private static System.Timers.Timer ArduinoTimer = new System.Timers.Timer();
        private static string arduino_value = "none";


        public static void inizialize()
        {
            if (Global_var.Arduino_enable)
            {
                ArduinoSerialPort.PortName = Global_var.Arduino_ComPort;
                ArduinoSerialPort.BaudRate = 9600;
                ArduinoSerialPort.DtrEnable = true;

                ArduinoTimer.Interval = 100;
                ArduinoTimer.Elapsed += ArduinoTimer_Elapsed;

                try
                {
                    ArduinoSerialPort.Open();
                    ArduinoTimer.Start();
                }
                catch (Exception ex)
                {
                    //ArduinoLogger.LogOutError(ex, "Arduino Problem, check COM ports in Agent config file!");
                    DarkMessageBox.Show("Arduino Problem : check " + Global_var.Arduino_ComPort + " port if exits or is wrong", "Arduino Info", DarkMessageBox.Buttons.OK, DarkMessageBox.Icon.Error);
                    Arduino_GUI.open();

                }
            }


        }

        private static void ArduinoTimer_Elapsed(object sender, ElapsedEventArgs e)
        {


            ArduinoSerialPort.DiscardInBuffer(); //prevent memory button lack (memory of previous state when press many times the button
            ArduinoTimer.Stop(); //or index array read overflow
            try
            {

                if (!Global_var.Arduino_debug)
                {
                    try
                    {
                        arduino_value = ArduinoSerialPort.ReadLine();
                    }
                    catch (Exception ex)
                    {

                        App_Messages.error_message(ex);
                    }

                    if (arduino_value == "Start\r")
                    {
                        //Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => { CockpitButton.Close(); }), DispatcherPriority.ContextIdle);
                        //this works here for this static method
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            CockpitButton.Close();
                        }));

                        Actions.AC_Process_starts();


                    }


                    if (arduino_value == "Stop\r")
                    {


                        AC_ESC.Kill();

                    }
                }

                else
                {
                    arduino_value = ArduinoSerialPort.ReadLine();

                    if (arduino_value == "Start\r")
                    {
                        Global_var._AC_Core_GUI.Arduino_test_ON.Dispatcher.Invoke(() =>
                        {
                            Global_var._AC_Core_GUI.Arduino_test_ON.Fill = new SolidColorBrush(Color.FromArgb(100, 0, 255, 0));
                            Global_var._AC_Core_GUI.Arduino_test_OFF.Fill = new SolidColorBrush(Color.FromArgb(10, 255, 0, 0));

                        });


                    }


                    if (arduino_value == "Stop\r")
                    {

                        Global_var._AC_Core_GUI.Arduino_test_ON.Dispatcher.Invoke(() =>
                        {
                            Global_var._AC_Core_GUI.Arduino_test_ON.Fill = new SolidColorBrush(Color.FromArgb(10, 61, 255, 0));
                            Global_var._AC_Core_GUI.Arduino_test_OFF.Fill = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));

                        });
                    }
                }

                ArduinoTimer.Start();

            }




            catch (Exception ex)
            {

                DarkMessageBox.Show(ex.Message, "Arduino Read Error", DarkMessageBox.Buttons.OK, DarkMessageBox.Icon.Error);

            }

        }






    } 
#endif
}
