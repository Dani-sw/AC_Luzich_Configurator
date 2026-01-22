using ExpressionDark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_CoRe
{
    static class App_Messages
    {


        static public void error_message(Exception e)
        {
            show_message(e.Message, "Error", DarkMessageBox.Buttons.OK, DarkMessageBox.Icon.Error);
        }



        static private void show_message(string _message, string _title, DarkMessageBox.Buttons message_buttons, DarkMessageBox.Icon message_icon)
        {

            DarkMessageBox.Show(_message, _title, message_buttons, message_icon);

        }
    }
}
