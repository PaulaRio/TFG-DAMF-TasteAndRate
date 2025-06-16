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
   public partial class AddValoracionViewModel : ViewModelBase
    {
        
        private LoginDTO _user;
        private ICollection<GastroDTO> _allGastros;
        private ICollection<ValoracionDTO> _allValoraciones;

        private IEnumerable<CriterioDTO> _allCriterios;
        private List<int> _allIdGastros;
        private List<int> _allIdEtiquetas;
        
        [ObservableProperty]
        public string _NombreGastro;
        [ObservableProperty]
        public string _Descripcion;
        [ObservableProperty]
        public string _DireccionGastro;
        [ObservableProperty]
        public string _IdEtiqueta;
        [ObservableProperty]
        public string _IdsGastro;

        [ObservableProperty]
        private ICollection<EtiquetaDTO> _allEtiquetas;

        [ObservableProperty]
        private EtiquetaDTO _selectedEtiqueta;

        [ObservableProperty]
        private double _notaFinal;

        private ICollection<CriterioDTO> _allCriteriosActivos;

        private ObservableCollection<CriterioNota> _criteriosNotas;
        public ObservableCollection<CriterioNota> CriteriosNotas
        {
            get => _criteriosNotas;
            set => SetProperty(ref _criteriosNotas, value);
        }

        
       

        private readonly IValoracionProvider _valoracionProvider;
        private readonly IGastroProvider _gastroProvider;
        private readonly IEtiquetaProvider _etiquetaProvider;
        private readonly ICriterioProvider _criterioProvider;
        private readonly IValoracionCriterioProvider _valoracionCriterioProvider;
       

        public AddValoracionViewModel( IValoracionProvider valoracionProvider, IGastroProvider gastroProvider, ICriterioProvider criterioProvider, IEtiquetaProvider etiquetaProvider, IValoracionCriterioProvider valoracionCriterioProvider, LoginDTO user)
        {
            _valoracionProvider = valoracionProvider;
            _gastroProvider = gastroProvider;
            _etiquetaProvider = etiquetaProvider;
            _valoracionCriterioProvider = valoracionCriterioProvider;
            _criterioProvider = criterioProvider;
            _user = user;
            _notaFinal = 0;
            _allIdGastros = new List<int>();
            _allIdEtiquetas = new List<int>();
            _allEtiquetas=new ObservableCollection<EtiquetaDTO>();

        }

       
        public AddValoracionViewModel(IGastroProvider gastroProvider, IValoracionProvider valoracionProvider, ICriterioProvider criterioProvider, IValoracionCriterioProvider valoracionCriterioProvider, IEtiquetaProvider etiquetaProvider, LoginDTO user)
        {
            _gastroProvider = gastroProvider;
            _valoracionProvider = valoracionProvider;
            _criterioProvider = criterioProvider;
            _valoracionCriterioProvider = valoracionCriterioProvider;
            _etiquetaProvider = etiquetaProvider;
            _user = user;
        }

        [RelayCommand]
        private void CancelarVentana(object? parameter)
        {
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is AddValoracionView)?.Close();
        }

       
        [RelayCommand]
        private async Task Save()
        {
            //await LoadAsync();
            ValoracionDTO objeto = await PostValoracionAsync();
            if (objeto != null)
            {
                var valoracionesCriterio = GenerarValoracionesCriterio(objeto.Id);

                foreach (var v in valoracionesCriterio)
                {
                    await _valoracionCriterioProvider.PostValoracionCriterio(v);
                }

                _notaFinal = CalcularNotaFinal();
                objeto.NotaFinal = _notaFinal;
                await _valoracionProvider.PatchValoracion(objeto);
                MessageBox.Show($"Valoración guardada con nota final: {_notaFinal}");

                MessageBox.Show("Post exitoso");
            }else
            {
                MessageBox.Show("Post fallido");
                
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
        

        public override async Task LoadAsync()
        {
            IEnumerable<ValoracionDTO> valoraciones = await _valoracionProvider.GetValoraciones();
            _allValoraciones = new ObservableCollection<ValoracionDTO>();
            foreach (var valoracion in valoraciones)
            {
                _allValoraciones.Add(valoracion);
                
            }
            IEnumerable<CriterioDTO> criterios = await _criterioProvider.GetCriterios();
            _allCriterios = await _criterioProvider.GetCriterios();

            List<CriterioDTO> criteriosActivos = _allCriterios.Where(c => c.Activo).ToList();
            List<CriterioNota> criteriosNotaActivos = criterios.Where(c => c.Activo).Select(c => new CriterioNota
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Nota = 0
            }).ToList();

            CriteriosNotas = new ObservableCollection<CriterioNota>(criteriosNotaActivos);
            _allCriteriosActivos = new ObservableCollection<CriterioDTO>(criteriosActivos);


            AllEtiquetas = new ObservableCollection<EtiquetaDTO>(await _etiquetaProvider.GetEtiquetas());
            
            
        }
       

        private async Task<ValoracionDTO> PostValoracionAsync()
        {
            if (SelectedEtiqueta == null)
            {
                MessageBox.Show("Debes seleccionar una etiqueta.");
                return null;
            }
            int etiqueta= SelectedEtiqueta?.Id ?? 0;
            bool compGastroEtiqueta = EsValoracionRepetida(_NombreGastro, _DireccionGastro, etiqueta);

            try
            {
                if (!compGastroEtiqueta)
                {
                    ValoracionDTO nuevoObjeto = new ValoracionDTO
                    {
                        Descripcion = _Descripcion,
                        UsuarioId=_user.UserId,
                        EtiquetaId=etiqueta,
                        NotaFinal = _notaFinal,
                        Gastro = new GastroDTO
                        {
                            Nombre = _NombreGastro,
                            Direccion = _DireccionGastro,
                            
                            
                        }
              
                    };
                   
                    ValoracionDTO resultado= await _valoracionProvider.PostValoracion(nuevoObjeto);
                    if (resultado != null)
                        MessageBox.Show("Objeto creado exitosamente");

                    return resultado;

                }

                MessageBox.Show("Ya existe una valoración con esos datos.");
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return default;
            }
            


        }

        private bool EsValoracionRepetida(string nombreGastro, string direccionGastro, int etiqueta)
        {
            return _allValoraciones.Any(v =>
         string.Equals(v.Gastro?.Nombre, nombreGastro, StringComparison.OrdinalIgnoreCase) &&
        string.Equals(v.Gastro?.Direccion, direccionGastro, StringComparison.OrdinalIgnoreCase) &&
        v.EtiquetaId == etiqueta);
        }
    }
}
