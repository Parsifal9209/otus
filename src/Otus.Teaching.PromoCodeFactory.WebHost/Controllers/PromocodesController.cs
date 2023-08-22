using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private readonly IRepository<Preference> _preferenceRepository;
        private readonly IRepository<PromoCode> _promocodeRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public PromocodesController(IRepository<PromoCode> promocodeRepository, IRepository<Customer> customerRepository, IRepository<Preference> preferenceRepository, IMapper mapper)
        {
            _promocodeRepository = promocodeRepository;
            _customerRepository = customerRepository;
            _preferenceRepository = preferenceRepository;
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
            var preferences = (await _preferenceRepository.GetAllAsync()).ToList();
            var preference = preferences.FirstOrDefault(x=>x.Name.ToUpper() == request.Preference.ToUpper());

            if (preference is null)
                return NotFound("Указанное предпочтение не найдено!");

            var customers = _customerRepository.GetAll()
                .Include(x => x.CustomerPreferences)
                .Where(x => x.CustomerPreferences.Any(x=>x.PreferenceId == preference.Id))
                .ToList();

            var rnd = new Random();
            var lukyCustomer = customers[rnd.Next(customers.Count)];

            var promocode = request.Adapt<PromoCode>();
            promocode.Preference = preference;
            promocode.PreferenceId = preference.Id;
            promocode.CustomerId = lukyCustomer.Id;

            await _promocodeRepository.AddAsync(promocode);

            lukyCustomer.PromoCodes.Add(promocode);
            await _customerRepository.UpdateAsync(lukyCustomer);

            return Ok($"Промокод создан и добавлен случайному клиенту " + lukyCustomer.FullName);
        }
    }
}