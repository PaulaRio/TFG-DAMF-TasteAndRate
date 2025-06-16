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
    public class GastroController : BaseController<GastroEntity, GastroDTO, CreateGastroDTO>
    {
        public GastroController(IGastroRepository gastroRepository, IMapper mapper, ILogger<GastroController> logger) 
            : base(gastroRepository, mapper, logger)
        {
        }
    }
}
