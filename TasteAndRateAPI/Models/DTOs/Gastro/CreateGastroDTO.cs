using System.ComponentModel.DataAnnotations;

namespace TasteAndRateAPI.Models.DTOs.Gastro
{
    public class CreateGastroDTO
    {
        [Required(ErrorMessage = "Nombre is required")]
        [MaxLength(200, ErrorMessage = "Max char is 200")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Direccion is required")]
        [MaxLength(300, ErrorMessage = "Max char is 1000")]
        public string Direccion { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //public string Horario { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //public string Web { get; set; }

        //[Required(AllowEmptyStrings = true)]
        //public string Fotos { get; set; }



       
       // public int IdValoracion { get; set; }



    }
}
