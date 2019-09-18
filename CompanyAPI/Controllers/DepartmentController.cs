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
using Microsoft.Extensions.Logging;

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
            try
            {
                var retval = await _departmentRepository.Read();

                if (retval.Count == 0)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeparment(int id)
        {
            try
            {
                var retval = await _departmentRepository.Read(id);

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

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            try
            {
                if (departmentDto.Name == null || departmentDto.Name == "" || departmentDto.CompanyId <= 0)
                    return BadRequest();

                if (await _departmentRepository.Create(departmentDto))
                    return StatusCode(StatusCodes.Status201Created);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
        {
            try
            {
                if (departmentDto.Name == null || departmentDto.Name == "" || departmentDto.CompanyId <= 0)
                    return BadRequest();

                if (await _departmentRepository.Update(id, departmentDto))
                    return NoContent();
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                if (await _departmentRepository.Delete(id))
                    return NoContent();
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

    }
}
