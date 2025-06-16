using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using TasteAndRate.DTOs;

namespace TasteAndRate.Models
{
    public class CriterioModel : ObservableObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _nombre;
        public string Nombre
        {
            get => _nombre;
            set => SetProperty(ref _nombre, value);
        }

        private int _pesoRelativo;
        public int PesoRelativo
        {
            get => _pesoRelativo;
            set => SetProperty(ref _pesoRelativo, value);
        }

        private bool _activo;
        public bool Activo
        {
            get => _activo;
            set => SetProperty(ref _activo, value);
        }

        private string _usuarioId;
        public string UsuarioId
        {
            get => _usuarioId;
            set => SetProperty(ref _usuarioId, value);
        }



        internal static CriterioModel CreateModelFromDTO(CriterioDTO objeto)
        {
            return new CriterioModel
            {
                Id = objeto.Id,
                Nombre = objeto.Nombre,
                PesoRelativo = objeto.PesoRelativo,
                Activo = objeto.Activo,
                UsuarioId = objeto.UsuarioId

            };
        }
    }
}
