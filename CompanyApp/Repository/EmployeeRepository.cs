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
        string dbConStr = "";
        string selcetCmd = "SELECT Id, FirstName, LastName, Birthday, DepartmentId, AddressId FROM Employee";
        string deleteCmd = "UPDATE employee SET DeleteTime = GetDate() WHERE @Id = Id";
        string spCreateOrUpdate = "spCreateOrUpateEmployee";

        public EmployeeRepository(string dbConnectionStr)
        {
            dbConStr = dbConnectionStr;
        }

        public List<Employee> Read()
        {
            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                return sqlcon.Query<Employee>(selcetCmd).AsList();
            }
        }

        public Employee Read(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                return sqlcon.QueryFirst<Employee>($"{selcetCmd} WHERE @Id = Id", parameters);
            }
        }

        public Employee Create(Employee employee)
        {
            return CreateOrUpdate(employee);
        }

        public Employee Update(Employee employee)
        {
            return CreateOrUpdate(employee);
        }

        private Employee CreateOrUpdate(Employee employee)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employee.Id);
            parameters.Add("@FirstName", employee.FirstName);
            parameters.Add("@LastName", employee.LastName);
            parameters.Add("@BirthDay", employee.Birthday);
            parameters.Add("@DepartmentId", employee.DepartmentId);
            parameters.Add("@AdressId", employee.AddressId);

            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                int id = sqlcon.ExecuteScalar<int>(spCreateOrUpdate, parameters, commandType: CommandType.StoredProcedure);

                parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                return sqlcon.QueryFirst<Employee>($"{selcetCmd} WHERE @id = Id", parameters);
            }
        }

        public bool Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            using(SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                return 1 == (sqlcon.Execute(deleteCmd, parameters));

            }
        }
    }
}
