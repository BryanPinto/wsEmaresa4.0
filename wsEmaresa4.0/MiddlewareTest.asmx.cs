using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Web.Script.Services;

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
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public EstadoCotizacion Estado(int response)
        {
            try
            {
                //int respuesta;
                EstadoCotizacion json = new EstadoCotizacion();
                if (response == 2)
                {
                    //Approve
                    json.CodigoEstado = response;
                    json.DetalleRespuesta = "Aprobado";
                }
                else if (response == 1)
                {
                    //Reject
                    json.CodigoEstado = response;
                    json.DetalleRespuesta = "Rechazado";
                }
                else
                {
                    //Undefined
                    json.CodigoEstado = 0;
                    json.DetalleRespuesta = "Estado Indefinido";
                }
                
                //Serializar xml del objeto EstadoCotizacion
                var jsonRequest = JsonConvert.SerializeObject(json);
                //Escribir log
                string rutaLog = HttpRuntime.AppDomainAppPath;
                StringBuilder sb = new StringBuilder();

                sb.Append(Environment.NewLine +
                          DateTime.Now.ToShortDateString() + " " +
                          DateTime.Now.ToShortTimeString() + ": " +
                          "[Estado] -- Codigo estado: " + response + "|" + " JSON: " + jsonRequest);
                System.IO.File.AppendAllText(rutaLog + "Log.txt", sb.ToString());
                sb.Clear();
                //retornar objeto JSON
                //Context.Response.Write(jsonRequest);
                return json;
            }
            catch(Exception ex)
            {
                EstadoCotizacion json = new EstadoCotizacion();
                //Escribir log
                string rutaLog = HttpRuntime.AppDomainAppPath;
                StringBuilder sb = new StringBuilder();

                sb.Append(Environment.NewLine +
                          DateTime.Now.ToShortDateString() + " " +
                          DateTime.Now.ToShortTimeString() + ": " +
                          "[Estado] -- Mensaje error: " + ex.Message);
                System.IO.File.AppendAllText(rutaLog + "Log.txt", sb.ToString());
                sb.Clear();
                return json;
            }
            
        }

        [WebMethod]
        public string GetStatus(int response)
        {

            string json = String.Empty;
            if (response == 2)
            {
                //Approve
                //json.codigoEstado = response;
                //json.detalleRespuesta = "Aprobado";
                json = "aprobado";

            }
            else if (response == 1)
            {
                //Reject
                //    json.codigoEstado = response;
                //    json.detalleRespuesta = "Rechazado";
                json = "rechazado";
            }
            else
            {
                //Undefined
                //json.codigoEstado = 0;
                //json.detalleRespuesta = "Estado Indefinido";
                json = "desconoce";
            }
            
           
            return (json);
        }

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

                    //declarar xml de creación
                    string xmlCreacion = @"<?xml version=""1.0""?>
                                            <BizAgiWSParam>
                                                <domain>domain</domain>
                                                <userName>admon</userName>
                                                <Cases>
                                                    <Case>
                                                        <Process>CopyProcesoDeCompras</Process>
                                                        <Entities>
                                                            <ProcesodeCompras>
                                                                <NroSolicitudERP>ASD</NroSolicitudERP>
                                                                <FechaCotizacion>2018-12-11</FechaCotizacion>
                                                                <FechaSolicitud>2018-12-11</FechaSolicitud>
                                                                <Solicitante>ASD</Solicitante>
                                                                <Condp_Pago>ASD</Condp_Pago>
                                                                <Tipo_compra>ASD</Tipo_compra>
                                                                <ObservacionSolicitud>ASD</ObservacionSolicitud>
                                                                <Itemgeneral>1</Itemgeneral>
                                                                <ItemSolicitud>1</ItemSolicitud>
                                                                <TotalCotizado>1</TotalCotizado>
                                                                <DetalleCotizacion>
                                                                    <CodProducto>ASD</CodProducto>
                                                                    <Tipo_Concepto>ASD</Tipo_Concepto>
                                                                    <DescripcionAmpliada>ASD</DescripcionAmpliada>
                                                                    <NombreProveedor>ASD</NombreProveedor>
                                                                    <Cantidad>1</Cantidad>
                                                                    <UnidadMedida>ASD</UnidadMedida>
                                                                    <PrecioUnit>1</PrecioUnit>
                                                                    <Neto>1</Neto>
                                                                    <Observacion>ASD</Observacion>
                                                                    <MotivoRechazo>ASD</MotivoRechazo>
                                                                </DetalleCotizacion>
                                                            </ProcesodeCompras>
                                                        </Entities>
                                                    </Case>
                                                </Cases>
                                            </BizAgiWSParam>";
                    //crear instancia
                    BizagiCapaSOA.WorkflowEngineSOASoapClient serviceEngine = new BizagiCapaSOA.WorkflowEngineSOASoapClient();


                    string respuestaBizagi = serviceEngine.createCasesAsString(xmlCreacion);
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
    
    public class EstadoCotizacion
    {
        public int CodigoEstado { get; set; }
        public string DetalleRespuesta { get; set; }
    }
}
