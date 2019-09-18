using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using Dapper;

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
            using (var sqlcon = await _dbContext.GetConnection())
            {
                return sqlcon.Query<Departmnet>(selectCmd).AsList();
            }
        }

        public async Task<Departmnet> Read(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var sqlcon = await _dbContext.GetConnection())
            {
                return sqlcon.QueryFirstOrDefault<Departmnet>($"{selectCmd} WHERE @id = Id", parameters);
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

            using (var sqlcon = await _dbContext.GetConnection())
            {
                try
                {
                    return 1 == (sqlcon.Execute(spCreateOrUpdate, parameters, commandType: CommandType.StoredProcedure));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
                
            }
        }

        public async Task<bool> Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var sqlcon = await _dbContext.GetConnection())
            {
                return 1 == (sqlcon.Execute(deleteCmd,parameters));
            }
        }

    }
}
