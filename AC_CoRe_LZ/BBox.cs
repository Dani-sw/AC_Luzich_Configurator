using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using SharpDX.XInput;
using DeviceType = SharpDX.DirectInput.DeviceType;

namespace AC_CoRe
{
    public static class BBox
    {
        public static DirectInput directInput;
        public static Joystick joystick;
        public static DispatcherTimer timer;
        //private static IList<DeviceInstance> devices = new IList<DeviceInstance>();

        public static int Bbox_StartIndex { get; set; }
        public static int Bbox_StopIndex { get; set; }

        public static int Esc_onetime = 0;
        public static int Start_onetime = 0;

        public static  void InitJoystick_Dinput()
        {
            directInput = new DirectInput();

            // Trova il primo joystick disponibile
            var joystickGuid = Guid.Empty;

            //HACK potrebbe interferire con altri joystic , assegnare un guid personalizzato o basta il .supplemental
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            
            if (joystickGuid == Guid.Empty)
            {
                MessageBox.Show("BBOX not Found");
                return;
            }

            Bbox_StartIndex = Convert.ToInt32(Global_var.BBox_StartBtn_index)-1;
            Bbox_StopIndex = Convert.ToInt32(Global_var.BBox_StopBtn_index)-1;


            joystick = new Joystick(directInput, joystickGuid);
            joystick.Properties.BufferSize = 128;
            joystick.Acquire();

            // Legge i dati 30 volte al secondo (puoi cambiare)
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += ReadJoystick;
            timer.Start();
        }

#if false
        private static void ReadJoystick_Test(object sender, EventArgs e)
        {
            if (joystick == null) return;

            joystick.Poll();
            var state = joystick.GetCurrentState();
            var buttons = state.Buttons;
            //pulsanti da 0 il 15 in windows è 14 qui 
            int i = 9;
            if (joystick.GetCurrentState().Buttons[i])
            {
                //Global_var._AC_Core_GUI.bbox_lbl.Content = "Pulsante 10" + " premuto";
                // Debug.WriteLine($"Pulsante {i} premuto");
            }
            if (joystick.GetCurrentState().Buttons[4])
            {
                //Global_var._AC_Core_GUI.bbox_lbl.Content = "Pulsante 5" + " premuto";
            }

        } 
#endif
       

        private static void ReadJoystick(object sender, EventArgs e)
        {
            timer.Stop();
            if (joystick == null) return;

            joystick.Poll();
           // var state = joystick.GetCurrentState();
            //var buttons = state.Buttons;

            if (joystick.GetCurrentState().Buttons[Bbox_StartIndex])
            {
                //Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => { CockpitButton.Close(); }), DispatcherPriority.ContextIdle);
                //this works here for this static method
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    CockpitButton.Close();
                }));
                //onetime messi qui oer evitare avvii casuali
                //in caso di crash verrebbe disabilitato per sempre, ma un altro  Start_onetime è stato impostato
                //a 0 nella classe show dello startscreen, così che una caricata la pista lo screen rimette questa variabile a 0
                if (Start_onetime == 0)
                {
                    Start_onetime = 1;
                    Actions.AC_Process_starts();
                }


            }

           

            if (joystick.GetCurrentState().Buttons[Bbox_StopIndex])
            {

             //onetime messi perchè il comando esc era troppo veloce a volte così almeno da un esc solo
                if (Esc_onetime == 0)
                {
                    Esc_onetime = 1;
                    AC_ESC.Kill();
                }

            }
            timer.Start();
        }




        //   public static Controller controller;


#if false
        public static void InitJoystickX()
        {
            // Prova tutti gli slot XInput (0–3)
            controller = new Controller(UserIndex.One);
            if (!controller.IsConnected)
                controller = new Controller(UserIndex.Two);
            if (!controller.IsConnected)
                controller = new Controller(UserIndex.Three);
            if (!controller.IsConnected)
                controller = new Controller(UserIndex.Four);

            if (!controller.IsConnected)
            {
                MessageBox.Show("Nessun controller XInput trovato");
                return;
            }

            // Timer 16 Hz
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(16);
            timer.Tick += ReadJoystick;
            timer.Start();
        }

        private static void ReadJoystickX(object sender, EventArgs e)
        {
            if (controller == null || !controller.IsConnected)
                return;

            var state = controller.GetState();
            var buttons = state.Gamepad.Buttons;

            // Elenco dei pulsanti XInput
            // (in XInput è una flag enum)
            foreach (GamepadButtonFlags flag in Enum.GetValues(typeof(GamepadButtonFlags)))
            {
                if (flag == GamepadButtonFlags.None) continue;

                if (buttons.HasFlag(flag))
                {
                    Debug.WriteLine($"Pulsante {flag} premuto");
                }
            }

            // Se vuoi anche gli assi:
            // state.Gamepad.LeftThumbX;
            // state.Gamepad.LeftThumbY;
            // state.Gamepad.RightThumbX;
            // state.Gamepad.RightThumbY;
            // state.Gamepad.LeftTrigger;
            // state.Gamepad.RightTrigger;
        } 
#endif
        private static StringBuilder sb = new StringBuilder();
       

        public static void Show_all()
        {

            var directInput = new DirectInput();

            // Trova solo joystick, gamepad e volanti
            var devices = directInput.GetDevices(
                DeviceClass.GameControl,
                DeviceEnumerationFlags.AttachedOnly);

            foreach (var device in devices)
            {
                sb.AppendLine($"Nome: {device.InstanceName}");
                sb.AppendLine($"Tipo: {device.Type}");
                sb.AppendLine($"GUID: {device.InstanceGuid}");
                sb.AppendLine("----------------------------------");
            }

            if (sb.Length == 0)
                sb.AppendLine("Nessun joystick / gamepad / volante trovato.");

            MessageBox.Show(sb.ToString(), "Periferiche di gioco rilevate");
        } 
    }



}
