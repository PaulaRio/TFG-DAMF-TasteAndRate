using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasteAndRate.DTOs
{
    public class ValoracionCompletaDTO
    {
            public List<ValoracionDTO> Valoraciones { get; set; } = new();
            public List<ValoracionCriterioDTO> ValoracionCriterios { get; set; } = new();
            public List<CriterioDTO> Criterios { get; set; } = new();
     

    }
}
