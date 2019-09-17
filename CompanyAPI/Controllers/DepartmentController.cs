using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CompanyAPI.Repository;
using CompanyAPI.Helper;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;


namespace CompanyAPI.Controllers
{
    [Route("department")]
    public class DepartmentController : ControllerBase
    {
        private readonly IBaseInterface<Departmnet, DepartmentDto> _departmentRepository;

        public DepartmentController(IBaseInterface<Departmnet, DepartmentDto> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult GetDepartments()
        {
            var retval = _departmentRepository.Read();

            if (retval.Count == 0)
                NoContent();

            return Ok(retval);
        }

        [HttpGet("{id}")]
        public IActionResult GetDeparment(int id)
        {
            var retval = _departmentRepository.Read(id);

            if (retval == null)
                NoContent();

            return Ok(retval);
        }
    }
}
