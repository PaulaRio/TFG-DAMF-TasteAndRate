using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TasteAndRate.Models;

namespace TasteAndRate.DTOs
{
    public class EtiquetaDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("usuarioId")]
        public string UsuarioId { get; set; }

    }
}
