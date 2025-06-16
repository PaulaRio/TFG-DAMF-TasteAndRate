using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasteAndRate.DTOs;

namespace TasteAndRate.Interfaces
{
    public interface IValoracionProvider
    {
        Task<IEnumerable<ValoracionDTO>> GetValoraciones();
        Task<ValoracionDTO> GetOneValoracion(int id);
        Task<ValoracionDTO> PostValoracion(ValoracionDTO valoracion);
        Task PostValoraciones(IEnumerable<ValoracionDTO> lista);
        Task PatchValoracion(ValoracionDTO valoracion);
        Task<bool> DeleteValoracion(int id);
        Task<bool> DeleteAllValoraciones();

        Task<bool> ExistenValoraciones(List<int> gruposIds);
    }
}
