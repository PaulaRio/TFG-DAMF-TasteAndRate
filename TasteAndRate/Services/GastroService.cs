using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using TasteAndRate.DTOs;

using TasteAndRate.Utils;
using System.Windows;
using TasteAndRate.Interfaces;

namespace TasteAndRate.Services
{
    public class GastroService : IGastroProvider
    {
        private readonly IHttpsJsonClientProvider<GastroDTO> _httpsJsonClientProvider;
        public GastroService(IHttpsJsonClientProvider<GastroDTO> httpsJsonClientProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;

        }


        public async Task<IEnumerable<GastroDTO>> GetGastros()
        {
            return await _httpsJsonClientProvider.GetAsync(Constantes.GASTRO_URL);
        }

        public async Task<GastroDTO> GetOneGastro(string id)
        {
            return await _httpsJsonClientProvider.GetByIdAsync(Constantes.GASTRO_URL, id);
        }

        public async Task PatchGastro(GastroDTO Gastro)
        {
            if (Gastro != null)
            {
                await _httpsJsonClientProvider.PatchAsync(Constantes.GASTRO_URL, Gastro);
            }
            else
            {
                MessageBox.Show("No se ha podido cargar el objeto, no se ha realizado el cambio");
            }
        }

        public async Task PostGastro(GastroDTO Gastro)
        {
            if (Gastro != null)
            {
                await _httpsJsonClientProvider.PostAsync(Constantes.GASTRO_URL, Gastro);
            }
            else
            {
                MessageBox.Show("No se ha podido cargar el objeto, no se ha realizado el cambio");
            }
        }
        public async Task<bool> DeleteGastro(string id)
        {
            if (await _httpsJsonClientProvider.DeleteAsync($"{Constantes.GASTRO_URL}", id))
            {
                return true;
            }
            else
            {
                MessageBox.Show("No se ha podido eliminar el objeto");
                return false;
            }
        }

        public async Task<bool> DeleteAllGastroes()
        {
            bool exito = true;
            IEnumerable<GastroDTO> lista = await GetGastros();
            foreach (GastroDTO obj in lista)
            {
                if (!await DeleteGastro(obj.Id.ToString()))
                {
                    MessageBox.Show("No se ha podido eliminar el objeto");
                    exito = false;
                }

            }
            return exito;
        }

        public async Task PostGastroes(IEnumerable<GastroDTO> lista)
        {
            if (lista != null)
            {
                foreach (GastroDTO obj in lista)
                {

                    await PostGastro(obj);

                }
            }
            else
            {
                MessageBox.Show("No se ha podido cargar la lista, no se ha realizado el cambio");
            }

        }
        public async Task<bool> ExistenGastros(List<int> autoresIds)
        {
            try
            {

                var autores = await _httpsJsonClientProvider.GetAsync(Constantes.GASTRO_URL);


                var autoresExistentes = autores.Where(a => autoresIds.Contains(a.Id)).ToList();


                return autoresExistentes.Count == autoresIds.Count;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al verificar los autores: {ex.Message}");
                return false;
            }

        }

        public Task<GastroDTO> GetOneGastro(int id)
        {
            throw new NotImplementedException();
        }

        Task<GastroDTO> IGastroProvider.PostGastro(GastroDTO valoracion)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteGastro(int id)
        {
            throw new NotImplementedException();
        }

       
        public Task PostGastros(IEnumerable<GastroDTO> lista)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllGastros()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExisteGastro(string id)
        {
            throw new NotImplementedException();
        }

        
    }
}
