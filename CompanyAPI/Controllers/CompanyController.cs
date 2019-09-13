using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using CompanyAPI.Repository;


namespace CompanyAPI.Controller
{
    [Route("company")]
    public class CompanyController : ControllerBase
    {
        readonly IBaseInterface<Company> _companyRepository;

        public CompanyController()
        {
            _companyRepository = new CompanyRepository("Data Source=tappqa;Initial Catalog=Training-DS-Company;Integrated Security=True");
        }

        // GET api/values
        [HttpGet]
        public IActionResult GetCompanies()
        {
            return Ok(_companyRepository.Read());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetCompany(int id)
        {
            return Ok(_companyRepository.Read(id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult PostCompany([FromBody] CompanyDto company)
        {
            return NoContent;
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public IActionResult PutCompany(int id, [FromBody] string value)
        //{
        //    return
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public IActionResult DeleteCompany(int id)
        //{
        //    return
        //}
    }
}
