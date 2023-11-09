using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Password { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string Rfc { get; set; } = null!;
}
