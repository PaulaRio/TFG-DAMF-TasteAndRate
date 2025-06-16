using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasteAndRate.DTOs;

namespace TasteAndRate.Interfaces
{
    public interface IEtiquetaProvider
    {
        Task<IEnumerable<EtiquetaDTO>> GetEtiquetas();
        Task<EtiquetaDTO> GetOneEtiqueta(int id);
        Task<EtiquetaDTO> PostEtiqueta(EtiquetaDTO etiqueta);
        Task PostEtiquetas(IEnumerable<EtiquetaDTO> lista);
        Task PatchEtiqueta(EtiquetaDTO etiqueta);
        Task<bool> DeleteEtiqueta(int id);
        Task<bool> DeleteAllEtiquetas();
        Task<bool> ExisteEtiqueta(string id);
        Task<bool> ExistenEtiquetas(List<int> etiquetasIds);
    }
}
