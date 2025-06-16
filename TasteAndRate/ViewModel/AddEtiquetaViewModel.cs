using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TasteAndRate.View;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.ComponentModel;
using TasteAndRate.Interfaces;
using TasteAndRate.DTOs;
using TasteAndRate.Models;
using TasteAndRate.Utils;
using System.Collections.ObjectModel;
using TasteAndRate.DTO;
using TasteAndRate.Services;

namespace TasteAndRate.ViewModel
{
   public partial class AddEtiquetaViewModel : ViewModelBase
    {
        
        private LoginDTO _user;
        private ICollection<EtiquetaDTO> _allEtiquetas;
        private List<string> _allNombresEtiquetas;
        
        [ObservableProperty]
        public string _Nombre;
        
        private readonly IEtiquetaProvider _etiquetaProvider;

       

        public AddEtiquetaViewModel( IEtiquetaProvider etiquetaProvider, LoginDTO user)
        {
            _etiquetaProvider=etiquetaProvider;
            _user = user;
            _allNombresEtiquetas = new List<string>();
            
        }

        

        [RelayCommand]
        private void CancelarVentana(object? parameter)
        {
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is AddEtiquetaView)?.Close();
        }

       
        [RelayCommand]
        private async Task Save()
        {
            await LoadAsync();
            EtiquetaDTO objeto = await PostEtiquetaAsync();
            if (objeto!=null)
            {
                MessageBox.Show("Post exitoso");
                
            }
            else
            {
                MessageBox.Show("Post fallido");
                
            }
            


        }
        public override async Task LoadAsync()
        {
          


            IEnumerable<EtiquetaDTO> etiquetas = await _etiquetaProvider.GetEtiquetas();
            _allEtiquetas = new ObservableCollection<EtiquetaDTO>();
            foreach (var etiqueta in etiquetas)
            {
                _allEtiquetas.Add(etiqueta);
                
            }
           


            
        }


        private async Task<EtiquetaDTO> PostEtiquetaAsync()
        {
            int etiqueta=1;//A modificar cuadno pueda mostrar desplegable
            bool compGastroEtiqueta = EsEtiquetaRepetida(_Nombre);

            try
            {
                if (!compGastroEtiqueta)
                {
                    EtiquetaDTO nuevoObjeto = new EtiquetaDTO
                    {

                        Nombre = _Nombre,
                        UsuarioId =_user.UserId
                        
 
                    };
                   
                    return await _etiquetaProvider.PostEtiqueta(nuevoObjeto);
                    MessageBox.Show("Objeto creado exitosamente");

                }

                return default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return default;
            }
            


        }

     
        private bool EsEtiquetaRepetida(string nombreEtiqueta)
        {
            return _allEtiquetas.Any(v =>
         string.Equals(v.Nombre, nombreEtiqueta));

        }
    }
}
