using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasteAndRate.DTOs;

namespace TasteAndRate.Interfaces
{
    public interface IGastroProvider
    {
        Task<IEnumerable<GastroDTO>> GetGastros();
        Task<GastroDTO> GetOneGastro(int id);
        Task<GastroDTO> PostGastro(GastroDTO valoracion);
        //Task PostGastro(GastroDTO gastro);
        Task PostGastros(IEnumerable<GastroDTO> lista);
        Task PatchGastro(GastroDTO gastro);
        Task<bool> DeleteGastro(int id);
        Task<bool> DeleteAllGastros();
        Task<bool> ExisteGastro(string id);
        Task<bool> ExistenGastros(List<int> gastrosIds);
        
    }
}
