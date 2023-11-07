namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string? Password { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? RFC { get; set; }
        public List<object> Usuarios { get; set; }
    }
}