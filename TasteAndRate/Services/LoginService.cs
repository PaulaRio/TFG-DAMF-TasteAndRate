using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using TasteAndRate.DTOs;

using TasteAndRate.Utils;
using System.Windows;
using TasteAndRate.Interfaces;
using TasteAndRate.DTO;

namespace TasteAndRate.Services
{
    public class LoginService : ILoginProvider
    {
        private readonly IHttpsJsonClientProvider<LoginDTO> _httpsJsonClientProvider;
        public LoginService(IHttpsJsonClientProvider<LoginDTO> httpsJsonClientProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;

        }


        public async Task<IEnumerable<LoginDTO>> GetLogins()
        {
            return await _httpsJsonClientProvider.GetAsync(Constantes.VALORACION_URL);
        }

        public async Task<LoginDTO> GetOneLogin(string id)
        {
            return await _httpsJsonClientProvider.GetByIdAsync(Constantes.VALORACION_URL, id);
        }

        public async Task PatchLogin(LoginDTO Login)
        {
            if (Login != null)
            {
                await _httpsJsonClientProvider.PatchAsync(Constantes.VALORACION_URL, Login);
            }
            else
            {
                MessageBox.Show("No se ha podido cargar el objeto, no se ha realizado el cambio");
            }
        }

        public async Task PostLogin(LoginDTO Login)
        {
            if (Login != null)
            {
                await _httpsJsonClientProvider.PostAsync(Constantes.VALORACION_URL, Login);
            }
            else
            {
                MessageBox.Show("No se ha podido cargar el objeto, no se ha realizado el cambio");
            }
        }
        public async Task<bool> DeleteLogin(string id)//await _httpsJsonClientProvider.PatchAsync($"{Constantes.VALORACION_URL}{_obj.Id}", _obj) != null
        {
            if (await _httpsJsonClientProvider.DeleteAsync($"{Constantes.VALORACION_URL}", id))
            {
                return true;
            }
            else
            {
                MessageBox.Show("No se ha podido eliminar el objeto");
                return false;
            }
        }

       

        public async Task PostLogins(IEnumerable<LoginDTO> lista)
        {
            if (lista != null)
            {
                foreach (LoginDTO obj in lista)
                {

                    await PostLogin(obj);

                }
            }
            else
            {
                MessageBox.Show("No se ha podido cargar la lista, no se ha realizado el cambio");
            }

        }
        

        public Task<LoginDTO> GetOneLogin(int id)
        {
            throw new NotImplementedException();
        }

        Task<LoginDTO> ILoginProvider.PostLogin(LoginDTO valoracion)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLogin(int id)
        {
            throw new NotImplementedException();
        }
    }
}
