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
    public class ValoracionService : IValoracionProvider
    {
        private readonly IHttpsJsonClientProvider<ValoracionDTO> _httpsJsonClientProvider;
        public ValoracionService(IHttpsJsonClientProvider<ValoracionDTO> httpsJsonClientProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;

        }


        public async Task<IEnumerable<ValoracionDTO>> GetValoraciones()
        {
            return await _httpsJsonClientProvider.GetAsync(Constantes.VALORACION_URL);
        }

        public async Task<ValoracionDTO> GetOneValoracion(string id)
        {
            return await _httpsJsonClientProvider.GetByIdAsync(Constantes.VALORACION_URL, id);
        }

        public async Task PatchValoracion(ValoracionDTO Valoracion)
        {
            if (Valoracion != null)
            {
                await _httpsJsonClientProvider.PatchAsync($"{Constantes.VALORACION_URL}{Valoracion.Id}", Valoracion);
            }
            else
            {
                MessageBox.Show("No se ha podido cargar el objeto, no se ha realizado el cambio");
            }
        }

        public async Task<ValoracionDTO> PostValoracion(ValoracionDTO valoracion)
        {
            if (valoracion != null)
            {
              return  await _httpsJsonClientProvider.PostAsync(Constantes.VALORACION_URL, valoracion);
            }
            else
            {
                
                MessageBox.Show("No se ha podido cargar el objeto, no se ha realizado el cambio");
                return default;
            }
        }
        public async Task<bool> DeleteValoracion(int id)
        {
            if (await _httpsJsonClientProvider.DeleteAsync($"{Constantes.VALORACION_URL}", id.ToString()))
            {
                return true;
            }
            else
            {
                MessageBox.Show("No se ha podido eliminar el objeto");
                return false;
            }
        }

        public async Task<bool> DeleteAllValoraciones()
        {
            bool exito = true;
            IEnumerable<ValoracionDTO> lista = await GetValoraciones();
            foreach (ValoracionDTO obj in lista)
            {
                if (!await DeleteValoracion(obj.Id))
                {
                    MessageBox.Show("No se ha podido eliminar el objeto");
                    exito = false;
                }

            }
            return exito;
        }

        public async Task PostValoraciones(IEnumerable<ValoracionDTO> lista)
        {
            if (lista != null)
            {
                foreach (ValoracionDTO obj in lista)
                {

                    await PostValoracion(obj);

                }
            }
            else
            {
                MessageBox.Show("No se ha podido cargar la lista, no se ha realizado el cambio");
            }

        }
        public async Task<bool> ExistenValoraciones(List<int> autoresIds)
        {
            try
            {

                var autores = await _httpsJsonClientProvider.GetAsync(Constantes.VALORACION_URL);


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

        public Task<ValoracionDTO> GetOneValoracion(int id)
        {
            throw new NotImplementedException();
        }

       

      
    }
}
