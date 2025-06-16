using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TasteAndRateAPI.Models.Entity
{
    public class GastroEntity
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre is required")]
        [MaxLength(200, ErrorMessage = "Max char is 200")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Direccion is required")]
        [MaxLength(300, ErrorMessage = "Max char is 1000")]
        public string Direccion  { get; set; }

        public ICollection<ValoracionEntity> Valoraciones { get; set; }

        

        


    }
}
