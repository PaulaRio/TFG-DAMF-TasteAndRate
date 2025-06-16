using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TasteAndRate.DTOs
{
    public class ValoracionCriterioDTO
    {
        [JsonPropertyName("valoracionId")]
        public int ValoracionId { get; set; }

        [JsonPropertyName("criterioId")]
        public int CriterioId { get; set; }

        [JsonPropertyName("nota")]
        public double Nota { get; set; }

        [JsonPropertyName("peso")]
        public double Peso { get; set; }

        //[JsonPropertyName("nombreCriterio")]
        //public string? NombreCriterio { get; set; }
    }
}
