using System;
using System.Collections.Generic;

namespace DL;

public partial class Estado
{
    public int IdEstado { get; set; }

    public string NombreEstado { get; set; } = null!;

    public string Siglas { get; set; } = null!;

    public virtual ICollection<GeoReferencia> GeoReferencia { get; set; } = new List<GeoReferencia>();

    public virtual ICollection<Usuario> IdUsuarios { get; set; } = new List<Usuario>();
}
