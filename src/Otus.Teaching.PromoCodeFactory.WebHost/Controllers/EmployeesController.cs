using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController
        : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeesController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(x => 
                new EmployeeShortResponse()
                    {
                        Id = x.Id,
                        Email = x.Email,
                        FullName = x.FullName,
                    }).ToList();

            return employeesModelList;
        }
        
        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();
            
            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                Roles = employee.Roles.Select(x => new RoleItemResponse()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }

        /// <summary>
        /// Создать сотрудника
        /// </summary>
        /// <param name="employeeRequest">Модель сотрудника</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateEmployeeAsync([FromBody]EmployeeRequest employeeRequest)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeRequest.Id);

            if (employee != null)
                return BadRequest($"Сотрудник с указанным идентификатором {employeeRequest.Id} уже существует в системе. Измените идентификатор");

            if(!employeeRequest.Roles.Any())
                return BadRequest($"Не указана роль у сотрудника.");

            if (employeeRequest.Id == default(Guid))
                employeeRequest.Id = Guid.NewGuid();

            employee = new Employee()
            {
                Id = employeeRequest.Id,
                FirstName = employeeRequest.FirstName,
                LastName = employeeRequest.LastName,
                Email = employeeRequest.Email,
                Roles = employeeRequest.Roles.Select(x => new Role()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),                
                AppliedPromocodesCount = employeeRequest.AppliedPromocodesCount
            };

            await _employeeRepository.CreateAsync(employee);

            return Ok();
        }

        /// <summary>
        /// Удалить сотрудника по id
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound("Сотрудник не найден");

            await _employeeRepository.DeleteByIdAsync(id);

            return Ok();
        }

        /// <summary>
        /// Обновить сотрудника по id
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> UpdateEmployee([FromBody] EmployeeRequest employeeRequest)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeRequest.Id);

            if (employee == null)
                return NotFound("Сотрудник не найден");

            employee = new Employee()
            {
                Id = employeeRequest.Id,
                FirstName = employeeRequest.FirstName,
                LastName = employeeRequest.LastName,
                Email = employeeRequest.Email,
                Roles = employeeRequest.Roles.Select(x => new Role()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                AppliedPromocodesCount = employeeRequest.AppliedPromocodesCount
            };

            await _employeeRepository.UpdateAsync(employee);

            return Ok();
        }
    }
}