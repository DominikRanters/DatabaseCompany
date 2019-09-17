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
                return NoContent();

            return Ok(retval);
        }

        [HttpGet("{id}")]
        public IActionResult GetDeparment(int id)
        {
            var retval = _departmentRepository.Read(id);

            if (retval == null)
                return NoContent();

            return Ok(retval);
        }

        [HttpPost]
        public IActionResult CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            if (departmentDto.Name == null || departmentDto.Name == "" || departmentDto.CompanyId <= 0)
                return BadRequest();

            if (_departmentRepository.Create(departmentDto))
                return StatusCode(StatusCodes.Status201Created);

            return StatusCode(StatusCodes.Status409Conflict);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(int id,[FromBody] DepartmentDto departmentDto)
        {
            if (departmentDto.Name == null || departmentDto.Name == "" || departmentDto.CompanyId <= 0)
                return BadRequest();

            if (_departmentRepository.Update(id, departmentDto))
                return NoContent();

            return StatusCode(StatusCodes.Status409Conflict);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            if (_departmentRepository.Delete(id))
                return NoContent();

            return BadRequest();
        }

    }
}
