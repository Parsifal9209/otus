using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Клиенты
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController
        : ControllerBase
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CustomersController(IRepository<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех клиентов
        /// </summary>
        /// <returns>Коллекция клиентов в сокращенной форме</returns>
        [HttpGet]        
        [ProducesResponseType(typeof(IEnumerable<CustomerShortResponse>), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetAll().ToListAsync();

            return Ok(_mapper.Map<IEnumerable<CustomerShortResponse>>(customers));
        }
        
        /// <summary>
        /// Получение клиента по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        /// <returns>Клиент</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetAll()
                .Include(x=>x.CustomerPreferences)
                .ThenInclude(x=>x.Preference)
                .Include(x=>x.PromoCodes)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x=>x.Id == id);

            if (customer is null)
                return NotFound("Клиент не найден");

            var result = _mapper.Map<CustomerResponse>(customer);
            result.PromoCodes = _mapper.Map<List<PromoCodeShortResponse>>(customer.PromoCodes);
            result.PrefernceResponses = _mapper.Map<List<PrefernceResponse>>(customer.CustomerPreferences.Select(x=>x.Preference));

            return Ok(result);
        }
        
        /// <summary>
        /// Добавить клиента
        /// </summary>
        /// <param name="request">Модель клиента</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateCustomerAsync(CreateOrEditCustomerRequest request)
        {
            var customerId = Guid.NewGuid();
            var customer = new Customer()
            {
                Id = customerId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                CustomerPreferences = request.PreferenceIds.Select(x => new CustomerPreference()
                {
                    CustomerId = customerId,
                    PreferenceId = x
                }).ToList()
            };

            await _customerRepository.AddAsync(customer);

            return Ok();
        }
        
        /// <summary>
        /// Обновить клиента вместе с его предпочтениями
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> EditCustomersAsync(Guid id, CreateOrEditCustomerRequest request)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
                return NotFound("Клиент не найден.");

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Email = request.Email;
            customer.CustomerPreferences = request.PreferenceIds.Select(x => new CustomerPreference()
            {
                CustomerId = customer.Id,
                PreferenceId = x
            }).ToList();

            await _customerRepository.UpdateAsync(customer);

            return Ok();
        }
        
        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
                return NotFound("Клиент не найден.");

            await _customerRepository.RemoveAsync(customer);

            return Ok();
        }
    }
}