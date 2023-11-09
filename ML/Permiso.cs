using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Permiso
    {
        public ML.Usuario? Usuario { get; set; }
        public ML.Estado? Estado { get; set; }
        public List<object> Permisos { get; set; }

    }
}
