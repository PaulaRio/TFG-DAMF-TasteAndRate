using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasteAndRate.DTOs;

namespace TasteAndRate.Interfaces
{
  
    public interface IValoracionCriterioProvider
    {
        Task<IEnumerable<ValoracionCriterioDTO>> GetValoracionCriterios();
        Task<ValoracionCriterioDTO> GetOneValoracionCriterio(string id);
        Task PostValoracionCriterio(ValoracionCriterioDTO ValoracionCriterio);
        Task PostValoracionCriterios(IEnumerable<ValoracionCriterioDTO> lista);
        Task PatchValoracionCriterio(ValoracionCriterioDTO ValoracionCriterio);
        Task<bool> DeleteValoracionCriterio(string id);
        Task<bool> DeleteAllValoracionCriterios();
        Task<IEnumerable<ValoracionCriterioDTO>> GetValoracionesCriterioByValoracionId(int valoracionId);
    }
}
