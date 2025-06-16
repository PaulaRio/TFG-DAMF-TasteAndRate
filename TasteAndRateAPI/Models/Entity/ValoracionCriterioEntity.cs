using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TasteAndRateAPI.Models.Entity
{
    public class ValoracionCriterioEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ValoracionId { get; set; }
        [Required]
        public int CriterioId { get; set; }

        public int Nota { get; set; } 

        public double Peso { get; set; }

        public ValoracionEntity Valoracion { get; set; }
        public CriterioEntity Criterio { get; set; }
    }
}
