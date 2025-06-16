using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TasteAndRate.DTOs;
using TasteAndRate.Interfaces;
using TasteAndRate.Models;
using TasteAndRate.Utils;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TasteAndRate.ViewModel
{
    public partial class StackPanelViewModel : ViewModelBase
    {
        
        [ObservableProperty]
        private ObservableCollection<ValoracionModel> _items;
        private int _objetoId;
        private int _etiquetaId;
        private ValoracionDTO _obj;
        [ObservableProperty]
        private double _notaFinal;
        private IEnumerable<CriterioDTO> _allCriterios;
        private  OverviewViewModel _overviewViewModel;
        private readonly IHttpsJsonClientProvider<ValoracionDTO> _httpsJsonClientProvider;
        [ObservableProperty]
        private StackPanelItemModel _Item;
        private readonly IValoracionProvider _valoracionProvider;
        private readonly IEtiquetaProvider _etiquetaProvider;
        private readonly ICriterioProvider _criterioProvider;
        private readonly IValoracionCriterioProvider _valoracionCriterioProvider;
        private ICollection<EtiquetaDTO> _allEtiquetas;
        private ObservableCollection<CriterioNota> _criteriosNotas;
        public ObservableCollection<CriterioNota> CriteriosNotas
        {
            get => _criteriosNotas;
            set => SetProperty(ref _criteriosNotas, value);
        }

        public StackPanelViewModel(IHttpsJsonClientProvider<ValoracionDTO> httpsJsonClientProvider, IValoracionProvider valoracionProvider, IEtiquetaProvider etiquetaProvider, IValoracionCriterioProvider valoracionCriterioProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider ?? throw new ArgumentNullException(nameof(httpsJsonClientProvider));
            _valoracionProvider = valoracionProvider;
            _items = new ObservableCollection<ValoracionModel>();
            _etiquetaProvider= etiquetaProvider;
            _valoracionCriterioProvider = valoracionCriterioProvider;

        }
        public void SetIdGastro(int id)
        {
            _objetoId= id;
        }

        public override async Task LoadAsync()
        {
            _allEtiquetas = (await _etiquetaProvider.GetEtiquetas()).ToList();
            //IEnumerable<ValoracionDTO> valoraciones = await _httpsJsonClientProvider.GetAsync(Constantes.VALORACION_URL);
            IEnumerable<ValoracionDTO> valoraciones = await _valoracionProvider.GetValoraciones();
            Items = new ObservableCollection<ValoracionModel>();
            foreach (var objeto in valoraciones)
            {
                foreach (var etiqueta in _allEtiquetas)
                {
                    if (etiqueta.Id.Equals(objeto.EtiquetaId))
                    {
                        Items.Add(ValoracionModel.CreateModelFromDTO(objeto, etiqueta.Nombre));
                    }
                }
               
                
            }
            _obj = valoraciones.FirstOrDefault(x => x.Id == _objetoId);
            var nombreEtiqueta = _allEtiquetas.FirstOrDefault(e => e.Id == _obj.EtiquetaId)?.Nombre ?? string.Empty;
            Item = StackPanelItemModel.CreateModelFromDTO(_obj, nombreEtiqueta) ;
           
            // Cargar criterios y notas
            // var valoracionesCriterio = await _valoracionCriterioProvider.GetValoracionesCriterioByValoracionId(_obj.Id);
            //_allCriterios = await _criterioProvider.GetCriterios();
            //CriteriosNotas = new ObservableCollection<CriterioNota>(
            //    (from criterio in _allCriterios
            //     join vc in valoracionesCriterio on criterio.Id equals vc.CriterioId
            //     select new CriterioNota
            //     {
            //         Id = criterio.Id,
            //         Nombre = criterio.Nombre,
            //         Nota = vc.Nota
            //     }).ToList()
            //);
        }
        internal void SetParentViewModel(ViewModelBase overviewViewModel)
        {
            if (overviewViewModel is OverviewViewModel overview)
            {
                _overviewViewModel = overview;
            }

        }

        [RelayCommand]
        private async Task Save()
        {
            _obj.Gastro.Nombre=Item.Nombre;
            _obj.Descripcion = Item.Descripcion;
            _obj.Gastro.Direccion = Item.Direccion;
            var valoracionesCriterio = GenerarValoracionesCriterio(_obj.Id);

            foreach (var v in valoracionesCriterio)
            {
                await _valoracionCriterioProvider.PostValoracionCriterio(v);
            }
            _notaFinal = CalcularNotaFinal();
            _obj.NotaFinal = _notaFinal;
            await _valoracionProvider.PatchValoracion(_obj);
            if ( await _httpsJsonClientProvider.PatchAsync($"{Constantes.VALORACION_URL}{_obj.Id}", _obj) != null)
            {
                _overviewViewModel.LoadAsync();
                MessageBox.Show("Datos modificados");


            }
            else
            {
                MessageBox.Show("Error al actualizar");
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            if (await _valoracionProvider.DeleteValoracion(_obj.Id))
            {
                _overviewViewModel.LoadAsync();
                MessageBox.Show("Objeto eliminado");


            }
            else
            {
                MessageBox.Show("Error al actualizar");
            }
        }


        [RelayCommand]
        private async Task Close()
        {
            if (_overviewViewModel != null)
            {
                _overviewViewModel.SelectedViewModel = null;
            }
        }
        private List<ValoracionCriterioDTO> GenerarValoracionesCriterio(int valoracionId)
        {
            var lista = new List<ValoracionCriterioDTO>();

            foreach (var criterioNota in CriteriosNotas)
            {
                lista.Add(new ValoracionCriterioDTO
                {
                    ValoracionId = valoracionId,
                    CriterioId = criterioNota.Id,
                    Nota = criterioNota.Nota
                });
            }

            return lista;
        }
        private double CalcularNotaFinal()
        {
            double notaFinal = 0.0;

            foreach (var criterioNota in CriteriosNotas)
            {
                var criterio = _allCriterios.FirstOrDefault(c => c.Id == criterioNota.Id);
                if (criterio != null && criterio.PesoRelativo > 0)
                {

                    double peso = criterio.PesoRelativo / 100.0;
                    notaFinal += criterioNota.Nota * peso;
                }
            }

            return Math.Round(notaFinal, 2);
        }



    }
}
