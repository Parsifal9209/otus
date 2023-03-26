using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Предпочтения
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PreferencesController : ControllerBase
    {
        private readonly IRepository<Preference> _preferenceRepository;
        private readonly IMapper _mapper;

        public PreferencesController(IRepository<Preference> preferenceRepository, IMapper mapper)
        {
            _preferenceRepository = preferenceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех предпочтений
        /// </summary>
        /// <returns>Коллекция клиентов в сокращенной форме</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PrefernceResponse>), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var preferences = await _preferenceRepository.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<PrefernceResponse>>(preferences));
        }
    }
}
