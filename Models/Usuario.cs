using System.ComponentModel.DataAnnotations;

namespace examen.Models
{
    public class Usuario
    {
        // Propiedad clave primaria
        public int UsuarioID { get; set; }

        // Propiedad para el nombre del usuario
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public string Nombre { get; set; } = string.Empty; // Inicialización predeterminada para evitar valores nulos

        // Propiedad para el correo electrónico
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string CorreoElectronico { get; set; } = string.Empty; // Inicialización predeterminada

        // Propiedad para el teléfono
        [Phone(ErrorMessage = "Formato de teléfono inválido")]
        public string? Telefono { get; set; } // Este campo es opcional, así que se deja como anulable

        // Relación con otras entidades si aplica (si necesitas agregar relaciones con otros modelos)
        // Ejemplo: public ICollection<Equipo>? Equipos { get; set; }
    }
}
