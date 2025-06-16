using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System.Windows;
using TasteAndRate.DTOs;
using TasteAndRate.Interfaces;
using TasteAndRate.ViewModel;
using System.Collections.ObjectModel;
using TasteAndRate.Models;
using TasteAndRate.Utils;
using TasteAndRate.View;

namespace TasteAndRate.ViewModel
{
    public partial class DataGridViewModel : ViewModelBase
    {
        [ObservableProperty]
        private DateTime? _releaseDate;
        private IEnumerable<ValoracionCriterioDTO> _relaciones;
        private readonly IFileService<CriterioDTO> _fileService;

        private readonly ICriterioProvider _criterioProvider;
        private readonly IValoracionCriterioProvider _relacionProvider;
        public DataGridViewModel(ICriterioProvider criterioProvider, IFileService<CriterioDTO> fileService,IValoracionCriterioProvider relacionProvider)
        {
            _criterioProvider = criterioProvider;
            _relacionProvider= relacionProvider;
            _fileService = fileService;
            Criterios = new ObservableCollection<CriterioModel>();

        }
        [ObservableProperty]
        private ObservableCollection<CriterioModel> criterios;

    


        private void MyDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "CreatedDate")
            {
                var column = e.Column as DataGridTextColumn;
                if (column != null)
                {
                    column.Binding = new Binding("Fecha creacion")
                    {
                        StringFormat = "dd/MM/yyyy"
                    };
                }
            }
        }
        [RelayCommand]
        private void Add_Click()
        {
            var viewModel = App.Current.Services.GetService<AddCriterioViewModel>();

            var view = new AddCriterioView { DataContext = viewModel };
            view.ShowDialog();
            LoadAsync();


        }
        
        [RelayCommand]
        public void Export()
        {
            List<CriterioDTO> lista=new List<CriterioDTO>();
            var saveFileDialog = new SaveFileDialog
            {
                Filter = Constantes.JSON_FILTER
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                foreach (var obj in Criterios)
                {
                    lista.Add(CriterioDTO.CreateDTOFromModel(obj));

                }
                _fileService.Save(saveFileDialog.FileName, lista);
            }
        }
        [RelayCommand]
        public async void Import()
        {   
             
            var openFileDialog = new OpenFileDialog
            {
                Filter = Constantes.JSON_FILTER
            };

            if (openFileDialog.ShowDialog() == true)
            {

                var loadedCriterios = _fileService.Load(openFileDialog.FileName);
                if (loadedCriterios == null || !loadedCriterios.Any())
                {
                    MessageBox.Show("El archivo seleccionado está vacío o no es válido.");
                    
                }
                await _criterioProvider.DeleteAllCriterios();
                await _criterioProvider.PostCriterios(loadedCriterios);
                
                Criterios.Clear();
               
                foreach (var obj in loadedCriterios)
                {   
                    Criterios.Add(CriterioModel.CreateModelFromDTO(obj));
                    
                }
            }
            

        }
        public async Task CargarTabla()
        {

            IEnumerable<CriterioDTO> requestData = await _criterioProvider.GetCriterios();

            foreach (var element in requestData)
            {
                Criterios.Add(CriterioModel.CreateModelFromDTO(element));
            }

        }
        public override async Task LoadAsync()
        {
            _relaciones = await _relacionProvider.GetValoracionCriterios();
            Criterios.Clear();
            await CargarTabla();

        }
        private bool ValidarSumaMaximaPesosRelativos()
        {
            int suma = Criterios.Where(c => c.Activo).Sum(c => c.PesoRelativo);

            if (suma > 100)
            {
                MessageBox.Show("La suma de los pesos relativos activos no puede superar 100. Desactiva criterios y/o modifica sus pesos. Actualmente es: " + suma,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        public async Task ActualizarCriterioEnBD(CriterioModel criterio)
        {
            if (criterio == null) return;
            if (!ValidarSumaMaximaPesosRelativos())
            {
                await LoadAsync();
                return;
            }
                CriterioDTO criterioDTO = CriterioDTO.CreateDTOFromModel(criterio);
            await _criterioProvider.PatchCriterio(criterioDTO); 
        }
        [RelayCommand]
        public async Task ConfirmarCambiosAsync()
        {
            int sumaActivos = Criterios
                .Where(c => c.Activo)
                .Sum(c => c.PesoRelativo);

            if (sumaActivos != 100)
            {
                MessageBox.Show($"La suma de los pesos relativos de los criterios activos debe ser 100. Actualmente es {sumaActivos}.",
                    "Error de validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var criterio in Criterios)
            {
                var dto = CriterioDTO.CreateDTOFromModel(criterio);
                await _criterioProvider.PatchCriterio(dto);
            }

            MessageBox.Show("Cambios guardados correctamente.", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
            await LoadAsync();
        }

    }
}
