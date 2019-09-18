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

            if (retval.Count == 0)
                return NoContent();

            return Ok(retval);
        }

        // GET company/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            _logger.LogInformation($"hello from {Request.Headers["User-Agent"]}");
            var retval = await _companyRepository.Read(id);

            try
            {
                if (retval == null)
                    return NoContent();

                return Ok(retval);
            }
            catch (Helper.RepoException repoEx)
            {
                switch (repoEx.ExType)
                {
                    case Helper.RepoResultType.SQL_ERROR:
                        _logger.LogError(repoEx.InnerException, repoEx.Message);
                        return StatusCode(StatusCodes.Status503ServiceUnavailable);

                    case Helper.RepoResultType.WORNGPARAMETER:
                        _logger.LogError(repoEx.InnerException, repoEx.Message);
                        return BadRequest();
                }
            }

            return BadRequest();
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostCompany([FromBody] CompanyDto companyDto)
        {
            if (companyDto.Name == null || companyDto.Name == "")
                return BadRequest();

            var retval = await _companyRepository.Create(companyDto);
            if (retval)
                return StatusCode(StatusCodes.Status201Created);

            return StatusCode(StatusCodes.Status409Conflict);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, [FromBody] CompanyDto companyDto)
        {
            if (companyDto.Name == null || companyDto.Name == "")
                return BadRequest();

            var retval = await _companyRepository.Update(id, companyDto);
            if (retval)
                return NoContent();

            return StatusCode(StatusCodes.Status409Conflict);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if (await _companyRepository.Delete(id))
                return NoContent();

            return BadRequest();
        }
    }
}
