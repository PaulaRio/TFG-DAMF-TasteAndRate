namespace TasteAndRateAPI.Models.DTOs.ValoracionCriterio
{
    public class CreateValoracionCriterioDTO
    {
        public int ValoracionId { get; set; }
        public int CriterioId { get; set; }
        public double Nota { get; set; }
        public double Peso { get; set; }

    }
}
