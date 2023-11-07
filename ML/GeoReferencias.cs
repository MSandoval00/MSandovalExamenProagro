using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class GeoReferencias
    {
        public int IdGeorreferencia { get; set; }
        public ML.Estado Estado { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
    }
}
