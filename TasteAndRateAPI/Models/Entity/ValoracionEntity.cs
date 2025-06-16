using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TasteAndRateAPI.Models.Entity
{
    public class ValoracionEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UsuarioId { get; set; } 

        public int GastroId { get; set; }

        public GastroEntity Gastro { get; set; }

        public int EtiquetaId { get; set; } 

        [Required(ErrorMessage = "Descripcion is required")]
        public string Descripcion   { get; set; }

        public double NotaFinal { get; set; } 
        //public List<string> Fotos { get; set; } 

        public List<ValoracionCriterioEntity> NotasPorCriterio { get; set; } 

        public ValoracionEntity()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
