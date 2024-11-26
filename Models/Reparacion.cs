namespace examen.Models
{
    public class Reparacion
    {
        public int ReparacionID { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }

        public int EquipoID { get; set; }
        public Equipo Equipo { get; set; }

        public string Descripcion { get; set; } // Nueva propiedad

        public ICollection<DetalleReparacion> DetallesReparacion { get; set; }
        public ICollection<Asignacion> Asignaciones { get; set; }
    }
}
