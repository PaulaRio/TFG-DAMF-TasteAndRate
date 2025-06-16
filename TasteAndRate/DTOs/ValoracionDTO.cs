using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TasteAndRate.Models;

namespace TasteAndRate.DTOs
{
    public class ValoracionDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("usuarioId")]
        public string UsuarioId { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("etiquetaId")]
        public int EtiquetaId { get; set; }
   
        [JsonPropertyName("notaFinal")]
        public double NotaFinal { get; set; }

        [JsonPropertyName("gastro")]
        public GastroDTO Gastro { get; set; }

        [JsonPropertyName("criteriosValorados")]
        public List<ValoracionCriterioDTO> CriteriosValorados { get; set; } = new();
    }



}