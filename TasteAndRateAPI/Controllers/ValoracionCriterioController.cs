using AutoMapper;
using BasicAPI.Controllers.TasteAndRateAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using TasteAndRateAPI.Models.DTOs.ValoracionCriterio;
using TasteAndRateAPI.Models.Entity;
using TasteAndRateAPI.Repository;
using TasteAndRateAPI.Repository.IRepository;

namespace TasteAndRateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValoracionCriterioController : BaseController<ValoracionCriterioEntity, ValoracionCriterioDTO, CreateValoracionCriterioDTO>
    {
        public ValoracionCriterioController(IValoracionCriterioRepository valoracioncriterioRepository, IMapper mapper, ILogger<ValoracionCriterioController> logger) 
            : base(valoracioncriterioRepository, mapper, logger)
        {
        }
    }
}
