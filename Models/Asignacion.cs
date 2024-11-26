namespace examen.Models
{
    public class Asignacion
    {
        public int AsignacionID { get; set; }
        public DateTime FechaAsignacion { get; set; }

        public int ReparacionID { get; set; }
        public Reparacion? Reparacion { get; set; }

        public int TecnicoID { get; set; }
        public Tecnico? Tecnico { get; set; }
    }
}
