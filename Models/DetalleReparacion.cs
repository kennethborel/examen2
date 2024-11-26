using System.ComponentModel.DataAnnotations;

namespace examen.Models
{
    public class DetalleReparacion
    {
        [Key] // Declarar explícitamente la clave primaria
        public int DetalleID { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        // Relación con Reparacion
        public int ReparacionID { get; set; } // Clave foránea
        public Reparacion Reparacion { get; set; }
    }
}
