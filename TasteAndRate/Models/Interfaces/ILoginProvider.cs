using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasteAndRate.DTO;
using TasteAndRate.DTOs;

namespace TasteAndRate.Interfaces
{
    public interface ILoginProvider
    {
        Task<IEnumerable<LoginDTO>> GetLogins();
        Task<LoginDTO> GetOneLogin(int id);
        Task<LoginDTO> PostLogin(LoginDTO valoracion);
        Task PostLogins(IEnumerable<LoginDTO> lista);
        Task PatchLogin(LoginDTO valoracion);
        

    }
}
