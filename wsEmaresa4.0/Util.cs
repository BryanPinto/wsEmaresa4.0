using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace wsEmaresa4._0
{
    /// <summary>
    /// Funciones útiles para el envío de solicitudes REST.
    /// Hecho con ♥.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Escribe una fila en el archivo de logs.
        /// </summary>
        /// <param name="titulo">Primer campo de la fila</param>
        /// <param name="mensaje">Mensaje del log</param>
        /// <returns></returns>
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



        /// <summary>
        /// Asigna campos a una solicitud REST.
        /// Se convierte todo el objeto a JSON y se asigna.
        /// </summary>
        /// <param name="solicitudREST">Solicitud WebRequest</param>
        /// <param name="body">Objeto que se va a asignar</param>
        /// <returns></returns>
        public static WebRequest AsignarBodyREST(WebRequest solicitudREST, Object body)
        {
            try
            {
                // Convertir a JSON
                string json = JsonConvert.SerializeObject(body);

                // Log trace
                EscribirLog("[AsignarBodyREST] -- Json a random:", json);

                // Enviar solicitud
                using (var streamWriter = new StreamWriter(solicitudREST.GetRequestStream()))
                {
                    // Agregar parametros a enviar
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            catch (Exception error)
            {
                EscribirLog("[AsignarBodyREST] -- Asignar campos: ", error.Message);
            }
            return (solicitudREST);
        }



        /// <summary>
        /// Lee las respuestas de RANDOM.
        /// Puede leer las respuestas correctas y las excepciones de respuestas.
        /// </summary>
        /// <param name="solicitudREST"></param>
        /// <returns></returns>
        public static string LeerRespuestaREST(WebRequest solicitudREST)
        {
            string respuesta = string.Empty;
            try
            {
                // Leer respuesta
                using (WebResponse response = solicitudREST.GetResponse())
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        respuesta = streamReader.ReadToEnd();
                    }
                }
            }
            // Excepcion de error desde RANDOM
            catch (WebException error)
            {
                // Leer mensaje de error de RANDOM
                using (WebResponse response = error.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        respuesta = reader.ReadToEnd();
                    }
                }
            }

            // Log trace
            EscribirLog("[Respuesta RANDOM] -- Mensaje:", respuesta);

            return (respuesta);
        }
    }
}