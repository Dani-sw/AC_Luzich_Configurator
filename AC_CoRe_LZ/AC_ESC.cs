using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AC_CoRe
{
    public static class AC_ESC
    {
        #region Legacy Windows Method

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        #endregion
        private static UserActivityHook Esc_Hook;

        //usato questo dalla bbbox
        //HACK Se parte e crasha senza passare di qui le variabili onetime non vengono messe a zero e AC non parte più
        public static void Kill()
        {
            GameController.SendEscAndClick(Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY));
            
            BBox.Esc_onetime = 0;
            BBox.Start_onetime = 0;
        }






        public static void Kill_old()
        {
            IntPtr hWnd = FindWindow("acsW", "Assetto Corsa"); //così funzia da verificare con il PRO il nome
            if (hWnd != IntPtr.Zero)
            {
                SetForegroundWindow(hWnd);
                SendKeys.SendWait("{ESC}");
                Thread.Sleep(500);
                FocusON.clickOnAC_ESC();
                Utility.leftClick(new Point(Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY)));
                 /*Cursor.Position = new Point(Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY));
                
                 mouse_event(MOUSEEVENTF_LEFTDOWN, Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY), 0, 0);
                 Thread.Sleep(200);
                 mouse_event(MOUSEEVENTF_LEFTUP, Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY), 0, 0);
                 mouse_event(MOUSEEVENTF_LEFTDOWN, Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY), 0, 0);
                 Thread.Sleep(200);
                 mouse_event(MOUSEEVENTF_LEFTUP, Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY), 0, 0);
                 mouse_event(MOUSEEVENTF_LEFTDOWN, Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY), 0, 0);
                 Thread.Sleep(200);
                 mouse_event(MOUSEEVENTF_LEFTUP, Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY), 0, 0);*/


            }

        }

#if false
        public static void Kill2()
        {
            IntPtr hWnd = FindWindow("acsW", "Assetto Corsa"); //così funzia da verificare con il PRO il nome
            if (hWnd != IntPtr.Zero)
            {
                //TODO scoprire perchè il left mouse click dentro a focus on non va
                SetForegroundWindow(hWnd);
                //SendKeys.SendWait("{ESC}");
                //FocusON.clickOnAC_ESC();
                Thread.Sleep(500);
                Utility.leftClick(new Point(Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY)));
                /* Cursor.Position = new Point(Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY));
                 int MOUSEEVENTF_LEFTDOWN = 0x02;
                 int MOUSEEVENTF_LEFTUP = 0x04;
                 mouse_event(MOUSEEVENTF_LEFTDOWN, Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY), 0, 0);
                 Thread.Sleep(200);
                 mouse_event(MOUSEEVENTF_LEFTUP, Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY), 0, 0);
                 mouse_event(MOUSEEVENTF_LEFTDOWN, Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY), 0, 0);
                 Thread.Sleep(200);
                 mouse_event(MOUSEEVENTF_LEFTUP, Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY), 0, 0);
                 mouse_event(MOUSEEVENTF_LEFTDOWN, Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY), 0, 0);
                 Thread.Sleep(200);
                 mouse_event(MOUSEEVENTF_LEFTUP, Convert.ToInt32(Global_var.ESC_METHOD_MOUSEX), Convert.ToInt32(Global_var.ESC_METHOD_MOUSEY), 0, 0);*/

            }


        }

        public static void Esc_Listener()
        {
            Esc_Hook = new UserActivityHook();
            Esc_Hook.KeyDown += Esc_Hook_KeyDown;
            Esc_Hook.Start();

        }

        private static void Esc_Hook_KeyDown(object sender, KeyEventArgs e)
        {
            if (Global_var.Esc_Enable == "1")
            {
                Esc_Hook.Stop();
                if (e.KeyCode == Keys.Escape)
                {
                    Kill2();
                }
                Esc_Hook.Start();
            }

        }

#endif
    }
}
