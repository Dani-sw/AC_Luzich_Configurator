using System;
using NationalInstruments.DAQmx;
using ExpressionDark;
using System.Timers;
using System.Windows;
using System.Windows.Media;

namespace AC_CoRe
{
    //TODO PROBLEM if NI drivers are not installed on the PC and NI is ON
#if false
    public static class NI_switch
    {
        private static string Line_Address;
        private static Task _digitalReadTask;
        private static DigitalSingleChannelReader _digitalSingleChannelReader;

        private static System.Timers.Timer NI_Timer_StartupCheck;
        private static System.Timers.Timer NI_Timer;


        public static void inizialize()
        {
            try
            {
                if (Global_var.NI_enable)
                {
                    NI_Timer_StartupCheck = new System.Timers.Timer(500);
                    NI_Timer_StartupCheck.Elapsed += NI_Timer_StartupCheck_Elapsed;

                    NI_Timer = new System.Timers.Timer(500);
                    NI_Timer.Elapsed += NI_Pool_Elapsed;


                    Line_Address = Global_var.NI_LineAddress;
                    _digitalReadTask = new Task();
                    _digitalReadTask.DIChannels.CreateChannel(Line_Address, "GT_Switch", ChannelLineGrouping.OneChannelForEachLine);
                    _digitalSingleChannelReader = new DigitalSingleChannelReader(_digitalReadTask.Stream);


                    NI_Timer_StartupCheck.Start();
                }
            }
            catch (Exception ex)
            {
                DarkMessageBox.Show("NI INFO: " + ex.Message, "Error", DarkMessageBox.Buttons.OK, DarkMessageBox.Icon.Error);
                NI_GUI.open();
            }


        }

        private static void NI_Timer_StartupCheck_Elapsed(object sender, ElapsedEventArgs e)
        {
            NI_Timer_StartupCheck.Stop();

            if (!_digitalSingleChannelReader.ReadSingleSampleSingleLine())
            {
                Global_var.NI_button_fault = true; //one time
                DarkMessageBox.Show("the Start Cockpit button is pressed, please release!", "NI warning", DarkMessageBox.Buttons.OK, DarkMessageBox.Icon.Warning);
                NI_Timer_StartupCheck.Start();
            }

            else
            {

                if (Global_var.NI_button_fault) //necessary one time to give my released message (or multiply infinity window message)
                {
                    NI_Timer_StartupCheck.Stop();
                    DarkMessageBox.Show("Button released!", "Successful", DarkMessageBox.Buttons.OK, DarkMessageBox.Icon.Info);
                    NI_Timer.Start();

                }

                else
                {
                    NI_Timer_StartupCheck.Stop();
                    NI_Timer.Start();
                }

            }

        }

        private static void NI_Pool_Elapsed(object sender, ElapsedEventArgs e)
        {
            //TODO Debug NI on GUI
            try
            {


                if (!Global_var.NI_debug)
                {

                    if (!_digitalSingleChannelReader.ReadSingleSampleSingleLine())
                    {


                        /*  Global_var._AC_Core_GUI.test_lbl.Dispatcher.Invoke(() =>
                          {
                              Global_var._AC_Core_GUI.test_lbl.Content = "ON";
                          });*/

                        Actions.AC_Process_starts();
                    }


                    else
                    {
                        AC_ESC.Kill();
                        //Global_var.NI_button_fault = false;
                        /* Global_var._AC_Core_GUI.test_lbl.Dispatcher.Invoke(() =>
                         {
                             Global_var._AC_Core_GUI.test_lbl.Content = "OFF";
                         });*/

                    }
                }

                else //debug on
                {
                    if (!_digitalSingleChannelReader.ReadSingleSampleSingleLine())
                    {


                        Global_var._AC_Core_GUI.NI_Debug_testbutton.Dispatcher.Invoke(() =>
                         {
                             Global_var._AC_Core_GUI.NI_Debug_testbutton.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0)); ;
                         });

                    }


                    else
                    {

                        Global_var._AC_Core_GUI.NI_Debug_testbutton.Dispatcher.Invoke(() =>
                        {
                            Global_var._AC_Core_GUI.NI_Debug_testbutton.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0)); ;
                        });

                    }
                }

            }




            catch (DaqException ex)
            {
                /*string message = String.Format("An error occurred initializing the NI DAQ device for the line '{0}'.", lineAddress);
                _messager.ShowMessage("Error initializing NI DAQ", message, MessageType.Error);
                _logger.Log(ex, Priority.High, TraceEventType.Critical);
                _initialized = false; //was false //qui il problema se si mette false*/
                DarkMessageBox.Show(ex.Message, "NI Error", DarkMessageBox.Buttons.OK, DarkMessageBox.Icon.Error);

            }
        }

        private static void NI_check_buttondown()
        {

        }
    } 
#endif
}
