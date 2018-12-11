﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;
using Newtonsoft.Json;

namespace wsEmaresa4._0
{
    /// <summary>
    /// Descripción breve de MiddlewareTest
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class MiddlewareTest : System.Web.Services.WebService
    {
        [WebMethod]
        public string RetornarOK(string jsonString)
        {
            try
            {
                string respuesta = "OK";
                //variable para lanzar número al azar entre 0 y 10
                var NumAzar = new Random().Next(0, 10);

                if (NumAzar == 1 || NumAzar == 8)
                {
                    throw new Exception("Error en respuesta");
                }
                else
                {
                    //Replicar formato JSON para la respuesta del método
                    string salida = "{\"Respuesta\":\"" + respuesta + "\"}";
                    //Escribir log
                    string rutaLog = HttpRuntime.AppDomainAppPath;
                    StringBuilder sb = new StringBuilder();

                    sb.Append(Environment.NewLine +
                              DateTime.Now.ToShortDateString() + " " +
                              DateTime.Now.ToShortTimeString() + ": " +
                              "[RetornaOK] -- Entrada: " + jsonString + " | " + "Salida: "+ salida);
                    System.IO.File.AppendAllText(rutaLog + "Log.txt", sb.ToString());
                    sb.Clear();
                    //retornar salida
                    return salida;
                }
            }
            catch (Exception ex)
            {
                //Replicar formato JSON para la respuesta error del método
                string salida = "{\"Error\":\"" + ex.Message + "\"}";
                //Escribir log
                string rutaLog = HttpRuntime.AppDomainAppPath;
                StringBuilder sb = new StringBuilder();

                sb.Append(Environment.NewLine +
                          DateTime.Now.ToShortDateString() + " " +
                          DateTime.Now.ToShortTimeString() + ": " +
                          "[RetornaOK] -- Entrada: " + jsonString + " | " + "Salida: " + salida);
                System.IO.File.AppendAllText(rutaLog + "Log.txt", sb.ToString());
                sb.Clear();
                //retornar salida
                return salida;
            }
        }


        [WebMethod]
        //Recibir xml
        public string ConvertirXMLaJSON(string xml)
        {
            try
            {
                //Declarar variable vacía para convertir el xml a json 
                string json = string.Empty;
                //Generar un nuevo documento XML
                XmlDocument doc = new XmlDocument();
                //Asignar al documento el XML enviado
                doc.LoadXml(xml);
                //Utilizar variable json para realizar conversión
                json = JsonConvert.SerializeXmlNode(doc);
                //Escribir log
                string rutaLog = HttpRuntime.AppDomainAppPath;
                StringBuilder sb = new StringBuilder();

                sb.Append(Environment.NewLine +
                          DateTime.Now.ToShortDateString() + " " +
                          DateTime.Now.ToShortTimeString() + ": " +
                          "[ConvertirXMLaJSON] -- XML: " + xml + " | " + "Salida JSON: " + json);
                System.IO.File.AppendAllText(rutaLog + "Log.txt", sb.ToString());
                sb.Clear();
                //retornar salida

                return (json);
            }
            catch(Exception ex)
            {
                //Replicar formato JSON para la respuesta error del método
                string salida = "{\"Error\":\"" + ex.Message + "\"}";
                //Escribir log
                string rutaLog = HttpRuntime.AppDomainAppPath;
                StringBuilder sb = new StringBuilder();

                sb.Append(Environment.NewLine +
                          DateTime.Now.ToShortDateString() + " " +
                          DateTime.Now.ToShortTimeString() + ": " +
                          "[ConvertirXMLaJSON] -- Salida: " + salida);
                System.IO.File.AppendAllText(rutaLog + "Log.txt", sb.ToString());
                sb.Clear();
                //retornar salida
                return salida;
            }
        }

        [WebMethod]
        //Recibir xml
        public string ConvertirJSONaXML(string json)
        {
            try
            {
                var xmlNode = JsonConvert.DeserializeXmlNode(json).OuterXml;
                //Escribir log
                string rutaLog = HttpRuntime.AppDomainAppPath;
                StringBuilder sb = new StringBuilder();

                sb.Append(Environment.NewLine +
                          DateTime.Now.ToShortDateString() + " " +
                          DateTime.Now.ToShortTimeString() + ": " +
                          "[ConvertirJSONaXML] -- JSON: " + json + " | " + "XML: " + xmlNode);
                System.IO.File.AppendAllText(rutaLog + "Log.txt", sb.ToString());
                sb.Clear();
                return xmlNode;
            }
            catch (Exception ex)
            {
                //Replicar formato JSON para la respuesta error del método
                string salida = "{\"Error\":\"" + ex.Message + "\"}";
                //Escribir log
                string rutaLog = HttpRuntime.AppDomainAppPath;
                StringBuilder sb = new StringBuilder();

                sb.Append(Environment.NewLine +
                          DateTime.Now.ToShortDateString() + " " +
                          DateTime.Now.ToShortTimeString() + ": " +
                          "[ConvertirJSONaXML] -- Salida: " + salida);
                System.IO.File.AppendAllText(rutaLog + "Log.txt", sb.ToString());
                sb.Clear();
                //retornar salida
                return salida;
            }
        }
    }
}