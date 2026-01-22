using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AC_CoRe
{
    static public class FocusON
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        static public void clickOnAC()
        {
            IntPtr hWnd = FindWindow("acsW", "Assetto Corsa");
            SetForegroundWindow(hWnd);
            Utility.leftClick(new System.Drawing.Point(10, 10));
        }

        static public void clickOnAC_ESC()
        {
            IntPtr hWnd = FindWindow("acsW", "Assetto Corsa");
            SetForegroundWindow(hWnd);
            Utility.leftClick(new System.Drawing.Point(Global_var.ESC_METHOD_MOUSEX, Global_var.ESC_METHOD_MOUSEY));
        }



    }
}
