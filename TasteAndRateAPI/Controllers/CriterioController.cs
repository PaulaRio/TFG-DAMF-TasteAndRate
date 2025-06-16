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
    public class CriterioController : BaseController<CriterioEntity, CriterioDTO, CreateCriterioDTO>
    {
        public CriterioController(ICriterioRepository criterioRepository, IMapper mapper, ILogger<CriterioController> logger) 
            : base(criterioRepository, mapper, logger)
        {
        }
    }
}
