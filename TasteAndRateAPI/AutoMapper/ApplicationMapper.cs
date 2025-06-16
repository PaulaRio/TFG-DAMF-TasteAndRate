using TasteAndRateAPI.Models.DTOs.UserDto;
using AutoMapper;
using TasteAndRateAPI.Models.DTOs;
using TasteAndRateAPI.Models.Entity;
using TasteAndRateAPI.Models.DTOs.Gastro;
using TasteAndRateAPI.Models.DTOs.ValoracionCriterio;


namespace TasteAndRateAPI.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
           
            CreateMap<GastroEntity, GastroDTO>().ReverseMap();
            CreateMap<GastroEntity, CreateGastroDTO>().ReverseMap();

            CreateMap<ValoracionEntity, ValoracionDTO>().ReverseMap();
            CreateMap<ValoracionEntity, CreateValoracionDTO>().ReverseMap();

            CreateMap<CriterioEntity, CriterioDTO>().ReverseMap();
            CreateMap<CriterioEntity, CreateCriterioDTO>().ReverseMap();

            CreateMap<ValoracionCriterioEntity, ValoracionCriterioDTO>().ReverseMap();
            CreateMap<ValoracionCriterioEntity, CreateValoracionCriterioDTO>().ReverseMap();

            CreateMap<EtiquetaEntity, EtiquetaDTO>().ReverseMap();
            CreateMap<EtiquetaEntity, CreateEtiquetaDTO>().ReverseMap();

            CreateMap<AppUser, UserDto>().ReverseMap();
        }
    }
}
