using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TasteAndRateAPI.Models.Entity
{
    public class EtiquetaEntity
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre is required")]
        [MaxLength(200, ErrorMessage = "Max char is 200")]
        public string Nombre { get; set; } 

      
        public string? UsuarioId { get; set; }
    }
}
