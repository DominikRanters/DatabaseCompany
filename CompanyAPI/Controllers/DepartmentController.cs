﻿using System;
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
using Microsoft.Extensions.Logging;
using Chayns.Auth.ApiExtensions;
using Chayns.Auth.Shared.Constants;

namespace CompanyAPI.Controllers
{
    [Route("department")]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IBaseInterface<Departmnet, DepartmentDto> _departmentRepository;

        public DepartmentController(IBaseInterface<Departmnet, DepartmentDto> departmentRepository, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DepartmentController>();
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var retval = await _departmentRepository.Read();
            return Ok(retval);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeparment(int id)
        {
            var retval = await _departmentRepository.Read(id);

            if (retval == null)
                return NoContent();

            return Ok(retval);
        }

        [HttpPost]
        [ChaynsAuth(uac: Uac.Manager)]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            if (string.IsNullOrEmpty(departmentDto.Name) || departmentDto.CompanyId <= 0)
                return BadRequest();

            if (await _departmentRepository.Create(departmentDto))
                return StatusCode(StatusCodes.Status201Created);

            return BadRequest();
        }

        [HttpPut("{id}")]
        [ChaynsAuth(uac: Uac.Manager)]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
        {
            if (string.IsNullOrEmpty(departmentDto.Name) || departmentDto.CompanyId <= 0)
                return BadRequest();

            if (await _departmentRepository.Update(id, departmentDto))
                return NoContent();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ChaynsAuth(uac: Uac.Manager)]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (await _departmentRepository.Delete(id))
                return NoContent();

            return BadRequest();
        }

    }
}
