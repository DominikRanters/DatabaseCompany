using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using Chayns.Auth.ApiExtensions;
using Chayns.Auth.Shared.Constants;

namespace CompanyAPI.Controllers
{
    [Route("employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IBaseInterface<Employee, EmployeeDto> _employeeRepository;

        public EmployeeController(IBaseInterface<Employee, EmployeeDto> employeeRepository, ILoggerFactory loggerFactory) 
        {
            _employeeRepository = employeeRepository;
            _logger = loggerFactory.CreateLogger<EmployeeController>();
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var retval = await _employeeRepository.Read();
            return Ok(retval);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            if (id > 1)
                throw new Helper.RepoException(Helper.RepoResultType.WRONGPARAMETER);

            var retval = await _employeeRepository.Read();
            return Ok(retval);
        }

        [HttpPost]
        [ChaynsAuth(uac: Uac.Manager)]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (await _employeeRepository.Create(employeeDto))
                return NoContent();

            return BadRequest();
        }

        [HttpPut("{id}")]
        [ChaynsAuth(uac: Uac.Manager)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (id > 1)
                throw new Helper.RepoException(Helper.RepoResultType.WRONGPARAMETER);

            if (await _employeeRepository.Update(id, employeeDto))
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ChaynsAuth(uac: Uac.Manager)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id > 1)
                throw new Helper.RepoException(Helper.RepoResultType.WRONGPARAMETER);

            if(await _employeeRepository.Delete(id))
                return NoContent();

            return BadRequest();
        }
    }
}

