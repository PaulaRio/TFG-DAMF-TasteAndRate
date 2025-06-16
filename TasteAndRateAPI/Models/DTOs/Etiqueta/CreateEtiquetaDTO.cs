using System.ComponentModel.DataAnnotations;

namespace TasteAndRateAPI.Models.DTOs.Gastro
{
    public class CreateEtiquetaDTO 
    {
        [Required(ErrorMessage = "Nombre is required")]
        [MaxLength(200, ErrorMessage = "Max char is 200")]
        public string Nombre { get; set; }

        public string? UsuarioId { get; set; }

    }
}
