using System;
using System.Collections.Generic;

namespace DL;

public partial class GeoReferencia
{
    public int IdGeorreferencia { get; set; }

    public int? IdEstado { get; set; }

    public string Latitud { get; set; } = null!;

    public string Longitud { get; set; } = null!;

    public virtual Estado? IdEstadoNavigation { get; set; }
}
