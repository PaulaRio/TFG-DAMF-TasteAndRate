using System.ComponentModel.DataAnnotations;

namespace TasteAndRateAPI.Models.DTOs.Gastro
{
    public class CreateCriterioDTO
    {
        [Required(ErrorMessage = "Nombre is required")]
        [MaxLength(200, ErrorMessage = "Max char is 200")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "PesoRelativo is required")]
        public int PesoRelativo { get; set; }

        public bool Activo { get; set; }

       // public bool Predeterminado { get; set; }


        public string? UsuarioId { get; set; }
        


    }
}
