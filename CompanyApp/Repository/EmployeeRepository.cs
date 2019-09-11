using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CompanyApp.Interface;
using CompanyApp.Model;
using Dapper;

namespace CompanyApp.Repository
{
    public class EmployeeRepository : IBaseInterface<Employee>
    {
        string selcetCmd = "SELECT Id, Name, FoundedDate FROM viEmployee";
        string deleteCmd = "UPDATE employee SET DeleteTime = GetDate() WHERE @id = Id";
        string spCreateOrUpdate = "spCreateOrUpateEmployee";

        public List<Employee> Read(string dbSConStr)
        {
            using (SqlConnection sqlcon = new SqlConnection(dbSConStr))
            {
               return sqlcon.Query<Employee>(selcetCmd).AsList();
            }
        }

        public Employee Read(string dbSConStr, int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (SqlConnection sqlcon = new SqlConnection(dbSConStr))
            {
                return sqlcon.QueryFirst<Employee>($"{selcetCmd} WHERE @id = Id", parameters);
            }
        }

        public Employee Create(string dbSConStr, Employee data)
        {
            DynamicParameters parameters = new DynamicParameters();

        }

        public Employee Update(string dbSConStr, Employee data)
        {
            throw new NotImplementedException();
        }

        private Employee createOrUpdate(string dbConStr, Employee data)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string dbSConStr, int id)
        {
            throw new NotImplementedException();
        }
    }
}
