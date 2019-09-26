using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using CompanyAPI.Helper;
using Dapper;
using System.Data.SqlClient;

namespace CompanyAPI.Repository
{
    public class DepartmentRepository : IBaseInterface<Departmnet, DepartmentDto>
    {
        private readonly IDbContext _dbContext;

        string selectCmd = "SELECT Id, Name, Description, CompanyId FROM viDepartment";
        string deleteCmd = "UPDATE Department SET DeleteTime = GetDate() WHERE @id = Id";
        string spCreateOrUpdate = "spCreateOrUpdateDepartment";

        public DepartmentRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Departmnet>> Read()
        {
            List<Departmnet> retval;

            try
            {
                using (var sqlcon = await _dbContext.GetConnection())
                {
                    retval = (await sqlcon.QueryAsync<Departmnet>(selectCmd)).AsList();
                    if (retval.Count == 0)
                        throw new RepoException(RepoResultType.NOTFOUND);
                }
            }
            catch (SqlException)
            {
                throw new RepoException(RepoResultType.SQL_ERROR);
            }

            return retval;
        }

        public async Task<Departmnet> Read(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            try
            {
                using (var sqlcon = await _dbContext.GetConnection())
                {
                    return await sqlcon.QueryFirstOrDefaultAsync<Departmnet>($"{selectCmd} WHERE @id = Id", parameters);
                }
            }
            catch (SqlException)
            {
                throw new Helper.RepoException(Helper.RepoResultType.SQL_ERROR);
            }
        }

        public async Task<bool> Create(DepartmentDto departmentDto)
        {
            Departmnet departmnet = new Departmnet()
            {
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CompanyId = departmentDto.CompanyId
            };

            return await CreateOrUpdate(departmnet);
        }

        public async Task<bool> Update(int id, DepartmentDto departmentDto)
        {
            Departmnet departmnet = new Departmnet()
            {
                Id = id,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CompanyId = departmentDto.CompanyId
            };

            return await CreateOrUpdate(departmnet);
        }

        private async Task<bool> CreateOrUpdate(Departmnet departmnet)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@DepartmentId", departmnet.Id);
            parameters.Add("@Name", departmnet.Name);
            parameters.Add("@Description", departmnet.Description);
            parameters.Add("@CompanyId", departmnet.CompanyId);

            try
            {
                using (var sqlcon = await _dbContext.GetConnection())
                {
                    return 1 == await (sqlcon.ExecuteAsync(spCreateOrUpdate, parameters, commandType: CommandType.StoredProcedure));
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
            parameters.Add("@id", id);

            try
            {
                using (var sqlcon = await _dbContext.GetConnection())
                {
                    return 1 == await (sqlcon.ExecuteAsync(deleteCmd, parameters));
                }
            }
            catch (SqlException)
            {
                throw new Helper.RepoException(Helper.RepoResultType.SQL_ERROR);
            }
        }

    }
}
