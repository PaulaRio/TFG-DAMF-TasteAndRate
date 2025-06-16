using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TasteAndRate.Models;

namespace TasteAndRate.DTOs
{
    public class GastroDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }
        [JsonPropertyName("direccion")]
        public string Direccion { get; set; }
        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }
       
        [JsonPropertyName("valoraciones")]
        public List<ValoracionDTO> Valoraciones { get; set; }


        
    }
}
