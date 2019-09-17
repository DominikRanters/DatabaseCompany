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

        public DepartmentRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Departmnet> Read()
        {
            using (var sqlcon = _dbContext.GetConnection())
            {
                return sqlcon.Query<Departmnet>(selectCmd).AsList();
            }
        }

        public Departmnet Read(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var sqlcon = _dbContext.GetConnection())
            {
                return sqlcon.QueryFirstOrDefault<Departmnet>($"{selectCmd} WHERE @id = Id", parameters);
            }
        }

        public bool Create(DepartmentDto data)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, DepartmentDto data)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var sqlcon = _dbContext.GetConnection())
            {
                return 1 == (sqlcon.Execute(deleteCmd,parameters));
            }
        }

    }
}
