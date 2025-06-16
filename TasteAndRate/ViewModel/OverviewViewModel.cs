using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TasteAndRate.DTO;
using TasteAndRate.Service;
using TasteAndRate.DTOs;
using TasteAndRate.Models;
using TasteAndRate.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasteAndRate.Interfaces;
using TasteAndRate.Utils;
using TasteAndRate.Services;
using TasteAndRate.View;
using Microsoft.Extensions.DependencyInjection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.ObjectModel;
using System.Windows;
using System.Collections;
using Microsoft.Win32;

namespace TasteAndRate.ViewModel
{
    public partial class OverviewViewModel : ViewModelBase
    {

        [ObservableProperty]
        private EtiquetaDTO _selectedEtiqueta;

        [ObservableProperty]
        private ObservableCollection<ValoracionDTO> _items;
        private IEnumerable<ValoracionCriterioDTO> _valoracionCriterios;

        private List<ValoracionDTO> _valoraciones;
        private GastroDTO _obj;

        [ObservableProperty]
        private ICollection<EtiquetaDTO> _allEtiquetas;

        private List<int> _gruposIds;
        private readonly IHttpsJsonClientProvider<ValoracionDTO> _httpsJsonClientProvider;
        private readonly StackPanelViewModel _stackPanelViewModel;
        private readonly IStringUtils _stringUtils;
        private readonly IGastroProvider _objectProvider;
        private readonly ICriterioProvider _criterioProvider;
        private readonly IValoracionProvider _valoracionProvider;
        private readonly IValoracionCriterioProvider _valoracionCriterioProvider;
        private readonly ILoginProvider _loginProvider;
        private readonly IEtiquetaProvider _etiquetaProvider;
        private readonly IFileService<ValoracionCompletaDTO> _fileService;

        [ObservableProperty]
        private ViewModelBase? _selectedViewModel;

        public OverviewViewModel(IHttpsJsonClientProvider<ValoracionDTO> httpsJsonClientProvider,
            StackPanelViewModel stackPanelViewModel, IStringUtils stringUtils, IGastroProvider objectProvider, IFileService<ValoracionCompletaDTO> fileService,
            IValoracionProvider valoracionProvider, IValoracionCriterioProvider valoracionCriterioProvider, ILoginProvider loginProvider, IEtiquetaProvider etiquetaProvider, ICriterioProvider criterioProvider)
        {
            _objectProvider = objectProvider;
            _valoracionProvider = valoracionProvider;
            _etiquetaProvider = etiquetaProvider;
            _httpsJsonClientProvider = httpsJsonClientProvider;
            _stackPanelViewModel = stackPanelViewModel;
            _stringUtils = stringUtils;
            _items = new ObservableCollection<ValoracionDTO>();
            _allEtiquetas = new ObservableCollection<EtiquetaDTO>();
            _valoraciones= new List<ValoracionDTO>();
            _fileService = fileService;
            _valoracionCriterioProvider = valoracionCriterioProvider;
            _loginProvider = loginProvider;
            _criterioProvider = criterioProvider;
        }

        public override async Task LoadAsync()
        {
           
            _valoraciones = (await _httpsJsonClientProvider.GetAsync(Constantes.VALORACION_URL))?.ToList() ?? new List<ValoracionDTO>();


            Items = new ObservableCollection<ValoracionDTO>(_valoraciones.OrderByDescending(v => v.NotaFinal));
            AllEtiquetas = new ObservableCollection<EtiquetaDTO>(await _etiquetaProvider.GetEtiquetas());
            _valoracionCriterios = await _valoracionCriterioProvider.GetValoracionCriterios();

        }

     
        public void Filtrar()
        {

            if (_selectedEtiqueta != null)
            {
                var filtradas = _valoraciones
            .Where(v => v.EtiquetaId == _selectedEtiqueta.Id)
            .OrderByDescending(v => v.NotaFinal)
            .ToList();

                Items = new ObservableCollection<ValoracionDTO>(filtradas);
            }
            else
            {
                Items = new ObservableCollection<ValoracionDTO>(_valoraciones.OrderByDescending(v => v.NotaFinal));
            }
        }

        partial void OnSelectedEtiquetaChanged(EtiquetaDTO value)
        {

            Filtrar();


        }



        [RelayCommand]
        private async Task SelectViewModel(object? parameter)
        {
            _stackPanelViewModel.SetIdGastro(_stringUtils.ConvertToInteger(parameter?.ToString() ?? string.Empty) ?? int.MinValue);
            _stackPanelViewModel.SetParentViewModel(this);
            SelectedViewModel = _stackPanelViewModel;
            await _stackPanelViewModel.LoadAsync();
        }
        
        [RelayCommand]
        private  void AddValoracion()
        {
            
            var viewModel = App.Current.Services.GetService<AddValoracionViewModel>();
             viewModel.LoadAsync();
            
            var view = new AddValoracionView { DataContext = viewModel };
            view.ShowDialog();
           


        }
        
        [RelayCommand]
        private void AddEtiqueta()
        {
            var viewModel = App.Current.Services.GetService<AddEtiquetaViewModel>();

            var view = new AddEtiquetaView { DataContext = viewModel };
            view.ShowDialog();
            LoadAsync();


        }
        [RelayCommand]
        public async Task ExportFullData()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                var data = new ValoracionCompletaDTO
                {
                    Valoraciones = _valoraciones, 
                    Criterios = (await _criterioProvider.GetCriterios()).ToList(),
                    ValoracionCriterios = (await _valoracionCriterioProvider.GetValoracionCriterios()).ToList()
                };

                var fileService = new JsonObjectFileService<ValoracionCompletaDTO>();
                fileService.Save(saveFileDialog.FileName, data);

                MessageBox.Show("Exportación completada.");
            }
        }
        [RelayCommand]
        public async Task ImportFullData()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var fileService = new JsonObjectFileService<ValoracionCompletaDTO>();
                var data = fileService.Load(openFileDialog.FileName);

                if (data == null)
                {
                    MessageBox.Show("El archivo está vacío o no es válido.");
                    return;
                }

                
                await _valoracionProvider.DeleteAllValoraciones(); 
                await _criterioProvider.DeleteAllCriterios();      

            
                await _criterioProvider.PostCriterios(data.Criterios);
                await _valoracionProvider.PostValoraciones(data.Valoraciones);
                await _valoracionCriterioProvider.PostValoracionCriterios(data.ValoracionCriterios);

                await LoadAsync();

                MessageBox.Show("Importación completada.");
            }
        }





    }
}
