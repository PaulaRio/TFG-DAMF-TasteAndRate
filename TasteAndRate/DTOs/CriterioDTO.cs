using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using TasteAndRate.Models;

namespace TasteAndRate.DTOs
{

    public class CriterioDTO : ObservableObject
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("pesoRelativo")]
        public int PesoRelativo { get; set; }

        [JsonPropertyName("activo")]
        public bool Activo { get; set; }
        //[JsonPropertyName("predeterminado")]
        //public bool Predeterminado { get; set; }

        [JsonPropertyName("usuarioId")]
        public string? UsuarioId { get; set; }  





        internal static CriterioDTO CreateDTOFromModel(CriterioModel objeto)
        {


            return new CriterioDTO
            {
                Id = objeto.Id,
                Nombre = objeto.Nombre,
                PesoRelativo = objeto.PesoRelativo,
                Activo = objeto.Activo,
                UsuarioId=objeto.UsuarioId

            };
        }


    }
}

