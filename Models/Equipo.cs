namespace examen.Models
{
    public class Equipo
    {
        public int EquipoID { get; set; }
        public string TipoEquipo { get; set; }
        public string Modelo { get; set; }

        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
    }
}
