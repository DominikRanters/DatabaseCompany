using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using CompanyAPI.Helper;
using CompanyAPI.Repository;
using Microsoft.Extensions.Logging;


namespace CompanyAPI.Controller
{
    [Route("company")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly IBaseInterface<Company, CompanyDto> _companyRepository;

        public CompanyController(IBaseInterface<Company, CompanyDto> companyRepository, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CompanyController>();
            _companyRepository = companyRepository;
        }

        // GET company
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
                var retval = await _companyRepository.Read();
                return Ok(retval);
        }

        // GET company/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            _logger.LogInformation($"hello from {Request.Headers["User-Agent"]}");
            Company retval = null;

            retval = await _companyRepository.Read(id);

            return Ok(retval);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostCompany([FromBody] CompanyDto companyDto)
        {
            Payload user = Authentifikation.GetUser(HttpContext);
            if (user.PersonId == "131-62105")
            {
                if (companyDto.Name == null || companyDto.Name == "")
                return BadRequest();

            var retval = await _companyRepository.Create(companyDto);
            if (retval)
                return StatusCode(StatusCodes.Status201Created);

            return BadRequest();
        }
            return StatusCode(StatusCodes.Status403Forbidden);
    }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, [FromBody] CompanyDto companyDto)
        {
            Payload user = Authentifikation.GetUser(HttpContext);
            if (user.PersonId == "131-62105")
            {
                if (companyDto.Name == null || companyDto.Name == "")
                return BadRequest();

            var retval = await _companyRepository.Update(id, companyDto);
            if (retval)
                return NoContent();
            return Conflict();

    }
            return StatusCode(StatusCodes.Status403Forbidden);
}

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            Payload user = Authentifikation.GetUser(HttpContext);
            if (user == null)
                return StatusCode(StatusCodes.Status400BadRequest);

            if (user.PersonId == "131-62105")
            {
                if (await _companyRepository.Delete(id))
                return NoContent();

            return BadRequest();
}
            return StatusCode(StatusCodes.Status403Forbidden);
        }
    }
}
