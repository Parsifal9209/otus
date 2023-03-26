using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocodesController
        : ControllerBase
    {
        private readonly IRepository<PromoCode> _promocodeRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public PromocodesController(IRepository<PromoCode> promocodeRepository, IRepository<Customer> customerRepository, IMapper mapper)
        {
            _promocodeRepository = promocodeRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PromoCodeShortResponse>), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetPromocodesAsync()
        {
            var promocodes = await _promocodeRepository.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<PromoCodeShortResponse>>(promocodes));
        }
        
        /// <summary>
        /// Создать промокод и выдать его клиентам с указанным предпочтением
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
        {
            //TODO: Создать промокод и выдать его клиентам с указанным предпочтением
            var promocode = request.Adapt<PromoCode>();
            promocode.PartnerManagerId = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a31f");
            promocode.PreferenceId = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c");
            promocode.CustomerId = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0");

            await _promocodeRepository.AddAsync(promocode);

            var customers = _customerRepository.GetAll()
                .Include(x => x.CustomerPreferences)
                .ThenInclude(x => x.Preference)
                .AsSplitQuery()
                .ToList();

            foreach (var customer in customers)
            {
                if (customer.CustomerPreferences.Any(x => x.Preference.Name == request.Preference))
                {
                    customer.PromoCodes.Add(promocode);
                    await _customerRepository.UpdateAsync(customer);
                }
            }

            return Ok();
        }
    }
}