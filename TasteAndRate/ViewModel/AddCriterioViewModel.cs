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
   public partial class AddCriterioViewModel : ViewModelBase
    {
        
        private LoginDTO _user;
     
        private ICollection<CriterioDTO> _allCriterios;
        private List<string> _allNombresCriterios;
        
        [ObservableProperty]
        public string _Nombre;
        [ObservableProperty]
        public int _PesoRelativo;

        private readonly ICriterioProvider _etiquetaProvider;

       

        public AddCriterioViewModel( ICriterioProvider etiquetaProvider, LoginDTO user)
        {
            _etiquetaProvider=etiquetaProvider;
            _user = user;
            _allNombresCriterios = new List<string>();
            
        }

        

        [RelayCommand]
        private void CancelarVentana(object? parameter)
        {
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is AddCriterioView)?.Close();
        }

       
        [RelayCommand]
        private async Task Save()
        {
            await LoadAsync();
            CriterioDTO objeto = await PostCriterioAsync();
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
          


            IEnumerable<CriterioDTO> etiquetas = await _etiquetaProvider.GetCriterios();
            _allCriterios = new ObservableCollection<CriterioDTO>();
            foreach (var etiqueta in etiquetas)
            {
                _allCriterios.Add(etiqueta);
                
            }
           


            
        }
     

        private async Task<CriterioDTO> PostCriterioAsync()
        {
            int etiqueta=1;//A modificar cuadno pueda mostrar desplegable
            bool compGastroCriterio = EsCriterioRepetida(_Nombre);

            try
            {
                if (!compGastroCriterio)
                {
                    CriterioDTO nuevoObjeto = new CriterioDTO
                    {
                        Nombre = _Nombre,
                        PesoRelativo = _PesoRelativo,
                        Activo =true,
                        UsuarioId = _user.UserId


                    };
                   
                    return await _etiquetaProvider.PostCriterio(nuevoObjeto);
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

      
        private bool EsCriterioRepetida(string nombreCriterio)
        {
            return _allCriterios.Any(v =>
         string.Equals(v.Nombre, nombreCriterio));

        }
    }
}
