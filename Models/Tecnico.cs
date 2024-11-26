﻿namespace examen.Models
{
    public class Tecnico
    {
        public int TecnicoID { get; set; }
        public string Nombre { get; set; }
        public string Especialidad { get; set; }

        public ICollection<Asignacion> Asignaciones { get; set; }
    }
}
