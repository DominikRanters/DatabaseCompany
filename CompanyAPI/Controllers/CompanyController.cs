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


namespace CompanyAPI.Controller
{
    [Route("company")]
    public class CompanyController : ControllerBase
    {
        private readonly IBaseInterface<Company, CompanyDto> _companyRepository;

        public CompanyController(IBaseInterface<Company, CompanyDto> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        // GET company
        [HttpGet]
        public IActionResult GetCompanies()
        {
            var retval = _companyRepository.Read();

            if (retval.Count == 0)
                return NoContent();

            return Ok(retval);
        }

        // GET company/1
        [HttpGet("{id}")]
        public IActionResult GetCompany(int id)
        {
            var retval = _companyRepository.Read(id);

            if (retval == null)
                return NoContent();

            return Ok(retval);
        }

        // POST api/values
        [HttpPost]
        public IActionResult PostCompany([FromBody] CompanyDto companyDto)
        {
            if (companyDto.Name == null)
                return BadRequest();

            var retval = _companyRepository.Create(companyDto);
            if (retval)
                return StatusCode(StatusCodes.Status201Created);

            return BadRequest();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult PutCompany(int id, [FromBody] CompanyDto companyDto)
        {
            if (companyDto.Name == null)
                return BadRequest();

            var retval = _companyRepository.Update(id, companyDto);
            if (retval)
                return NoContent();

            return BadRequest();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            if (_companyRepository.Delete(id))
                return NoContent();

            return BadRequest();
        }
    }
}
