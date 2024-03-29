﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using Chayns.Auth.ApiExtensions;
using Chayns.Auth.Shared.Constants;
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

        // GET companies
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
        [ChaynsAuth(uac: Uac.Manager)]
        public async Task<IActionResult> PostCompany([FromBody] CompanyDto companyDto)
        {
            if (string.IsNullOrEmpty(companyDto.Name))
                return BadRequest();

            var retval = await _companyRepository.Create(companyDto);
            if (retval)
                return StatusCode(StatusCodes.Status201Created);

            return BadRequest();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ChaynsAuth(uac: Uac.Manager)]
        public async Task<IActionResult> PutCompany(int id, [FromBody] CompanyDto companyDto)
        {
            if (string.IsNullOrEmpty(companyDto.Name))
                return BadRequest();

            var retval = await _companyRepository.Update(id, companyDto);
            if (retval)
                return NoContent();
            return Conflict();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ChaynsAuth(uac: Uac.Manager)]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if (await _companyRepository.Delete(id))
                return NoContent();

            return BadRequest();
        }
    }
}
