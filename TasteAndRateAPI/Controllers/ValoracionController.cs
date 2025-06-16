using AutoMapper;
using BasicAPI.Controllers.TasteAndRateAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using TasteAndRateAPI.Models.DTOs.Gastro;
using TasteAndRateAPI.Models.Entity;
using TasteAndRateAPI.Repository;
using TasteAndRateAPI.Repository.IRepository;

namespace TasteAndRateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValoracionController : BaseController<ValoracionEntity, ValoracionDTO, CreateValoracionDTO>
    {
        public ValoracionController(IValoracionRepository valoracionRepository, IMapper mapper, ILogger<ValoracionController> logger) 
            : base(valoracionRepository, mapper, logger)
        {
        }
    }
}
