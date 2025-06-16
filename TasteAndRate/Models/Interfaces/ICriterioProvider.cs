using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasteAndRate.DTOs;

namespace TasteAndRate.Interfaces
{
    public interface ICriterioProvider
    {
        Task<IEnumerable<CriterioDTO>> GetCriterios();
        Task<CriterioDTO> GetOneCriterio(int id);
        Task<CriterioDTO> PostCriterio(CriterioDTO valoracion);
        //Task PostCriterio(CriterioDTO criterio);
        Task PostCriterios(IEnumerable<CriterioDTO> lista);
        Task PatchCriterio(CriterioDTO criterio);
        Task<bool> DeleteCriterio(int id);
        Task<bool> DeleteAllCriterios();
        Task<bool> ExisteCriterio(string id);
        Task<bool> ExistenCriterios(List<int> objetosIds);
      
    }
}
