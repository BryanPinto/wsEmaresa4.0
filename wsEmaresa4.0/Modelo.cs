using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsEmaresa4._0
{


    public class EstadoCotizacion
    {
        public int CodigoEstado { get; set; }
        public string DetalleRespuesta { get; set; }
    }
    public class CamposBusqueda
    {
        public string EMPRESA { get; set; }
        public string NUDO { get; set; }
        public string TIDO { get; set; }

    }
    public class RootObject
    {
        public CamposBusqueda Raiz { get; set; }
    }
    public class Retorno
    {
        public RootObject root { get; set; }
        public string MensajeSalida { get; set; }
    }
}