using System.ComponentModel.DataAnnotations;

namespace TasteAndRateAPI.Models.DTOs.Gastro
{
    public class CreateValoracionDTO
    {


        public CreateGastroDTO Gastro { get; set; }

        [Required(ErrorMessage = "UsuarioId  is required")]
        public string UsuarioId { get; set; }

        public string Descripcion { get; set; }

        [Required(ErrorMessage = "EtiquetaId  is required")]
        public int EtiquetaId { get; set; }
        public double NotaFinal  { get; set; }
 

    }
}
