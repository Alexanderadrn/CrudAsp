using System.ComponentModel.DataAnnotations;

namespace CrudAsp.DTO
{
    public class ContactoDTO
    {
        [Key]
        public int IdContacto { get; set; }
        [Required(ErrorMessage ="El campo es obligarorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo es obligarorio")]

        public string? Telefono { get; set; }
        [Required(ErrorMessage = "El campo es obligarorio")]

        public string? Correo { get; set; }
    }
}
