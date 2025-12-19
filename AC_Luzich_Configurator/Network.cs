using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AC_Configurator_STDL.Controls;

namespace AC_Configurator_STDL
{
    public static class Network
    {
        //TODO aggiungere logwriter e messagebox custom
        public static void Copy_Files()
        {
            try
            {
                string targetPath = $@"\\"+Global_var.IP_PC_CLient+ "\\Assetto Corsa\\cfg\\configurator_parameters.ini";
                string sourcePath = @"System\\configurator_parameters.ini";

                // Verifica che la cartella di destinazione esista
                string targetDir = Path.GetDirectoryName(targetPath);
                if (!Directory.Exists(targetDir))
                {
                    throw new DirectoryNotFoundException($"Directory not found: {targetDir}");
                }

                // Copia direttamente con overwrite (non serve Delete)
                File.Copy(sourcePath, targetPath, overwrite: true);

               
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox_Custom.Show(ex.Message, "Network Error", MessageBox_Custom.MessageType.Error);
                //LogWriter.Error_Trace(ex);
               // Utility.OpenSessionsFolder(Global_Var.App_path + Global_Var.logs_path.Trim('\\'));
            }
            catch (IOException ex)
            {
                MessageBox_Custom.Show(ex.Message, "Network Error", MessageBox_Custom.MessageType.Error);
            }
            catch (Exception ex)
            {
                MessageBox_Custom.Show(ex.Message, "Network Error", MessageBox_Custom.MessageType.Error);
            }

        }

    }
}
