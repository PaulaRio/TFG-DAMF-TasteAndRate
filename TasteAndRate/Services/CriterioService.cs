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
    public class CriterioService : ICriterioProvider
    {
        private readonly IHttpsJsonClientProvider<CriterioDTO> _httpsJsonClientProvider;
        public CriterioService(IHttpsJsonClientProvider<CriterioDTO> httpsJsonClientProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;

        }


        public async Task<IEnumerable<CriterioDTO>> GetCriterios()
        {
            return await _httpsJsonClientProvider.GetAsync(Constantes.CRITERIO_URL);
        }

        public async Task<CriterioDTO> GetOneCriterio(string id)
        {
            return await _httpsJsonClientProvider.GetByIdAsync(Constantes.CRITERIO_URL, id);
        }

        public async Task PatchCriterio(CriterioDTO Criterio)
        {
            if (Criterio != null)
            {
                await _httpsJsonClientProvider.PatchAsync($"{Constantes.CRITERIO_URL}{Criterio.Id}", Criterio);
            }
            else
            {
                MessageBox.Show("No se ha podido cargar el objeto, no se ha realizado el cambio");
            }
        }

        public async Task<CriterioDTO> PostCriterio(CriterioDTO Criterio)
        {
            if (Criterio != null)
            {
                return await _httpsJsonClientProvider.PostAsync(Constantes.CRITERIO_URL, Criterio);
            }
            else
            {
                MessageBox.Show("No se ha podido cargar el objeto, no se ha realizado el cambio");
                return default;
            }
        }
        public async Task<bool> DeleteCriterio(string id)
        {
            if (await _httpsJsonClientProvider.DeleteAsync($"{Constantes.CRITERIO_URL}", id))
            {
                return true;
            }
            else
            {
                MessageBox.Show("No se ha podido eliminar el objeto");
                return false;
            }
        }

        public async Task<bool> DeleteAllCriterios()
        {
            bool exito = true;
            IEnumerable<CriterioDTO> lista = await GetCriterios();
            foreach (CriterioDTO obj in lista)
            {
                if (!await DeleteCriterio(obj.Id.ToString()))
                {
                    MessageBox.Show("No se ha podido eliminar el objeto");
                    exito = false;
                }

            }
            return exito;
        }

        public async Task PostCriterios(IEnumerable<CriterioDTO> lista)
        {
            if (lista != null)
            {
                foreach (CriterioDTO obj in lista)
                {

                    await PostCriterio(obj);

                }
            }
            else
            {
                MessageBox.Show("No se ha podido cargar la lista, no se ha realizado el cambio");
            }

        }
        public async Task<bool> ExistenCriterios(List<int> autoresIds)
        {
            try
            {

                var autores = await _httpsJsonClientProvider.GetAsync(Constantes.CRITERIO_URL);


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

        public Task<CriterioDTO> GetOneCriterio(int id)
        {
            throw new NotImplementedException();
        }

      

        public Task<bool> DeleteCriterio(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExisteCriterio(string id)
        {
            throw new NotImplementedException();
        }
    }
}
