using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Estado
    {
        public int? IdEstado { get; set; }
        public string? NombreEstado { get; set; }
        public string? Siglas { get; set; }
        public List<object> Estados { get; set; }
    }
}
