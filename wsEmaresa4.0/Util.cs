using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace wsEmaresa4._0
{
    public static class Util
    {
        public static bool EscribirLog(string titulo, string mensaje)
        {
            bool valido = true;

            try
            {
                //Escribir log
                string rutaLog = HttpRuntime.AppDomainAppPath;

                StringBuilder sb = new StringBuilder();

                sb.Append(Environment.NewLine +
                          DateTime.Now.ToShortDateString() + " " +
                          DateTime.Now.ToShortTimeString() + ": " +
                          titulo + ": " + mensaje);

                System.IO.File.AppendAllText(rutaLog + "Log.txt", sb.ToString());

                sb.Clear();
            }
            catch (Exception ex)
            {
                valido = false;
            }
            return (valido);
        }
    }
}