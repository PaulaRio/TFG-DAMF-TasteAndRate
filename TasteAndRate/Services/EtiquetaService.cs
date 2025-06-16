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
    public class EtiquetaService : IEtiquetaProvider
    {
        private readonly IHttpsJsonClientProvider<EtiquetaDTO> _httpsJsonClientProvider;
        public EtiquetaService(IHttpsJsonClientProvider<EtiquetaDTO> httpsJsonClientProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;

        }


        public async Task<IEnumerable<EtiquetaDTO>> GetEtiquetas()
        {
            return await _httpsJsonClientProvider.GetAsync(Constantes.ETIQUETA_URL);
        }

        public async Task<EtiquetaDTO> GetOneEtiqueta(string id)
        {
            return await _httpsJsonClientProvider.GetByIdAsync(Constantes.ETIQUETA_URL, id);
        }

        public async Task PatchEtiqueta(EtiquetaDTO Etiqueta)
        {
            if (Etiqueta != null)
            {
                await _httpsJsonClientProvider.PatchAsync(Constantes.ETIQUETA_URL, Etiqueta);
            }
            else
            {
                MessageBox.Show("No se ha podido cargar el objeto, no se ha realizado el cambio");
            }
        }

        public async Task<EtiquetaDTO> PostEtiqueta(EtiquetaDTO Etiqueta)
        {
            if (Etiqueta != null)
            {
                return await _httpsJsonClientProvider.PostAsync(Constantes.ETIQUETA_URL, Etiqueta);
            }
            else
            {
               

                MessageBox.Show("No se ha podido cargar el objeto, no se ha realizado el cambio");
                return default;
            }
        }
        public async Task<bool> DeleteEtiqueta(string id)
        {
            if (await _httpsJsonClientProvider.DeleteAsync($"{Constantes.ETIQUETA_URL}", id))
            {
                return true;
            }
            else
            {
                MessageBox.Show("No se ha podido eliminar el objeto");
                return false;
            }
        }

        public async Task<bool> DeleteAllEtiquetas()
        {
            bool exito = true;
            IEnumerable<EtiquetaDTO> lista = await GetEtiquetas();
            foreach (EtiquetaDTO obj in lista)
            {
                if (!await DeleteEtiqueta(obj.Id.ToString()))
                {
                    MessageBox.Show("No se ha podido eliminar el objeto");
                    exito = false;
                }

            }
            return exito;
        }

        public async Task PostEtiquetas(IEnumerable<EtiquetaDTO> lista)
        {
            if (lista != null)
            {
                foreach (EtiquetaDTO obj in lista)
                {

                    await PostEtiqueta(obj);

                }
            }
            else
            {
                MessageBox.Show("No se ha podido cargar la lista, no se ha realizado el cambio");
            }

        }
        public async Task<bool> ExistenEtiquetas(List<int> autoresIds)
        {
            try
            {

                var autores = await _httpsJsonClientProvider.GetAsync(Constantes.ETIQUETA_URL);


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

        public Task<EtiquetaDTO> GetOneEtiqueta(int id)
        {
            throw new NotImplementedException();
        }


        public Task<bool> DeleteEtiqueta(int id)
        {
            throw new NotImplementedException();
        }

        
        public Task<bool> ExisteEtiqueta(string id)
        {
            throw new NotImplementedException();
        }

    }
}
