using System.Text.Json.Serialization;

namespace TasteAndRateAPI.Models.DTOs.Gastro
{
    public class ValoracionDTO : CreateValoracionDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        


    }
}
