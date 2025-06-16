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
    public class ValoracionCriterioService : IValoracionCriterioProvider
    {
        private readonly IHttpsJsonClientProvider<ValoracionCriterioDTO> _httpsJsonClientProvider;
        public ValoracionCriterioService(IHttpsJsonClientProvider<ValoracionCriterioDTO> httpsJsonClientProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;

        }


        public async Task<IEnumerable<ValoracionCriterioDTO>> GetValoracionCriterios()
        {
            return await _httpsJsonClientProvider.GetAsync(Constantes.VALORACIONCRITERIO_URL);
        }

        public async Task<ValoracionCriterioDTO> GetOneValoracionCriterio(string id)
        {
            return await _httpsJsonClientProvider.GetByIdAsync(Constantes.VALORACIONCRITERIO_URL, id);
        }

        public async Task PatchValoracionCriterio(ValoracionCriterioDTO ValoracionCriterio)
        {
            if (ValoracionCriterio != null)
            {
                await _httpsJsonClientProvider.PatchAsync(Constantes.VALORACIONCRITERIO_URL, ValoracionCriterio);
            }
            else
            {
                MessageBox.Show("No se ha podido cargar el objeto, no se ha realizado el cambio");
            }
        }

        public async Task PostValoracionCriterio(ValoracionCriterioDTO ValoracionCriterio)
        {
            if (ValoracionCriterio != null)
            {
                await _httpsJsonClientProvider.PostAsync(Constantes.VALORACIONCRITERIO_URL, ValoracionCriterio);
            }
            else
            {
                MessageBox.Show("No se ha podido cargar el objeto, no se ha realizado el cambio");
            }
        }
        public async Task<bool> DeleteValoracionCriterio(string id)//await _httpsJsonClientProvider.PatchAsync($"{Constantes.VALORACIONCRITERIO_URL}{_obj.Id}", _obj) != null
        {
            if (await _httpsJsonClientProvider.DeleteAsync($"{Constantes.VALORACIONCRITERIO_URL}", id))
            {
                return true;
            }
            else
            {
                MessageBox.Show("No se ha podido eliminar el objeto");
                return false;
            }
        }

        public async Task<bool> DeleteAllValoracionCriterios()
        {
            bool exito = true;
            IEnumerable<ValoracionCriterioDTO> lista = await GetValoracionCriterios();
            foreach (ValoracionCriterioDTO obj in lista)
            {
                if (!await DeleteValoracionCriterio())
                {
                    MessageBox.Show("No se ha podido eliminar el objeto");
                    exito = false;
                }

            }
            return exito;
        }

        private async Task<bool> DeleteValoracionCriterio()
        {
            throw new NotImplementedException();
        }

        public async Task PostValoracionCriterios(IEnumerable<ValoracionCriterioDTO> lista)
        {
            if (lista != null)
            {
                foreach (ValoracionCriterioDTO obj in lista)
                {

                    await PostValoracionCriterio(obj);

                }
            }
            else
            {
                MessageBox.Show("No se ha podido cargar la lista, no se ha realizado el cambio");
            }

        }
        

        public Task<ValoracionCriterioDTO> GetOneValoracionCriterio(int id)
        {
            throw new NotImplementedException();
        }

        

        public Task<bool> DeleteValoracionCriterio(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<ValoracionCriterioDTO>> GetValoracionesCriterioByValoracionId(int valoracionId)
        {
            // Aquí se asume que tu API permite filtrar con query string `?valoracionId=`
            string path = $"{Constantes.VALORACIONCRITERIO_URL}?valoracionId={valoracionId}";

            var result = await _httpsJsonClientProvider.GetAsync(path);
            return result ?? Enumerable.Empty<ValoracionCriterioDTO>();
        }
    }
}
