using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TasteAndRateAPI.Models.Entity
{
    public class CriterioEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UsuarioId { get; set; } 

        [Required(ErrorMessage = "Nombre is required")]
        [MaxLength(200, ErrorMessage = "Max char is 200")]
        public string Nombre { get; set; }

       
        [Required(ErrorMessage = "PesoRelativo is required")]
        public int PesoRelativo { get; set; } //Porcentaje

        public bool Activo { get; set; } 

       public ICollection<ValoracionCriterioEntity> ValoracionesCriterios { get; set; }
    }
}
