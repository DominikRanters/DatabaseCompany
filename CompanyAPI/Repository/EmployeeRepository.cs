using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using Dapper;

namespace CompanyAPI.Repository
{
    public class EmployeeRepository : IBaseInterface<Employee, EmployeeDto>
    {
        private readonly IDbContext _dbContext;

        string selectCmd = "SELECT Id, FirstName, LastName, Birthday, DepartmentId, AddressId FROM viEmployee";
        string deleteCmd = "UPDATE Employee SET DeleteTime =  GetDate() WHERE Id = @id";
        string spCreateOrUpdate = "spCreateOrUpdateEmployee";

        public EmployeeRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> Read()
        {
            List<Employee> retval;

            try
            {
                using (var sqlcon = await _dbContext.GetConnection())
                {
                    retval = (await sqlcon.QueryAsync<Employee>(selectCmd)).AsList();

                    if (retval == null || retval.Count == 0)
                        throw new Helper.RepoException(Helper.RepoResultType.NOTFOUND);
                }
            }
            catch (SqlException)
            {
                throw new Helper.RepoException(Helper.RepoResultType.SQL_ERROR);
            }

            return retval;
        }

        public async Task<Employee> Read(int id)
        {
            Employee retval;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            try
            {
                using (var sqlcon = await _dbContext.GetConnection())
                {
                    retval = await sqlcon.QueryFirstOrDefaultAsync<Employee>($"{selectCmd} WHERE id == @id", parameters);

                    if (retval == null)
                        throw new Helper.RepoException(Helper.RepoResultType.NOTFOUND);
            }
            }
            catch (SqlException)
            {
                throw new Helper.RepoException(Helper.RepoResultType.SQL_ERROR);
            }

            return retval;
        }

        public async Task<bool> Create(EmployeeDto employeeDto)
        {
            Employee employee = new Employee()
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Birthday = employeeDto.Birthday,
                DepartmnetId = employeeDto.DepartmnetId,
                AddressId = employeeDto.AddressId
            };

            return await CreateOrUpdate(employee);
        }

        public async Task<bool> Update(int id, EmployeeDto employeeDto)
        {
            Employee employee = new Employee()
            {
                Id = id,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Birthday = employeeDto.Birthday,
                DepartmnetId = employeeDto.DepartmnetId,
                AddressId = employeeDto.AddressId
            };

            return await CreateOrUpdate(employee);
        }

        private async Task<bool> CreateOrUpdate(Employee employee)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employee.Id);
            parameters.Add("@FirstName", employee.FirstName);
            parameters.Add("@LastName", employee.LastName);
            parameters.Add("@Birthday", employee.Birthday);
            parameters.Add("@DepartmentId", employee.DepartmnetId);
            parameters.Add("@AddressId", employee.AddressId);

            try
            {
                using (var sqlcon = await _dbContext.GetConnection())
                {
                    return 1 == await sqlcon.ExecuteAsync(spCreateOrUpdate, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException)
            {
                throw new Helper.RepoException(Helper.RepoResultType.SQL_ERROR);
            }
        }

        public async Task<bool> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            try
            {
                using (var sqlcon = await _dbContext.GetConnection())
                {
                    return 1 == await sqlcon.ExecuteAsync(deleteCmd, parameters);
                }
            }
            catch (SqlException)
            {
                throw new Helper.RepoException(Helper.RepoResultType.SQL_ERROR);
            }
        }

    }
}
