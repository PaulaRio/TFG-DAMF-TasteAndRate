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
    public class EtiquetaController : BaseController<EtiquetaEntity, EtiquetaDTO, CreateEtiquetaDTO>
    {
        public EtiquetaController(IEtiquetaRepository etiquetaRepository, IMapper mapper, ILogger<EtiquetaController> logger) 
            : base(etiquetaRepository, mapper, logger)
        {
        }
    }
}
