using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC_CoRe.Dialog;

namespace AC_CoRe
{
    static public  class CockpitButton
    {
        private static PushButton_Alert _pushButton_dialog;

        public static void Show()
        {
            _pushButton_dialog = new PushButton_Alert();
            _pushButton_dialog.ShowDialog();
            Global_var.is_PushStartbtn_Show = 1;
            //questo è stato introdotto per evitare che venga disabilitato  l'avvio se per caso crasha ac all'avvio e il 
            //ontime nel bbbox è già stato messo a 1 , qui quando reinvio la pista lo start screen riabilita il pulsante per lo start
            BBox.Start_onetime = 0;
        }

        public static void Close()
        {
         if(_pushButton_dialog!=null)
                _pushButton_dialog.Close();
            Global_var.is_PushStartbtn_Show = 0;
        }
    }
}
