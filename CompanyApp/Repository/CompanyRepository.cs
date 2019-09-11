using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using CompanyApp.Interface;
using CompanyApp.Model;
using Dapper;

namespace CompanyApp.Repository
{
    public class CompanyRepository : IBaseInterface<Company>
    {
        string selectCmd = "select Id, Name, FoundedDate from viCompany";
        string deleteCmd = "update company set DeleteTime = GetDate() where id = @id";
        string spCreateOrUpdate = "spCreateOrUpdateCompany";

        public List<Company> Read(string dbConStr)
        {
            List<Company> retval = new List<Company>();

            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                retval = sqlcon.Query<Company>(selectCmd).AsList();

                //using (SqlCommand cmd = new SqlCommand(selectCmd, sqlcon))
                //{
                //    sqlcon.Open();

                //    using (SqlDataReader reader = cmd.ExecuteReader())
                //    {
                //        while (reader.Read())
                //        {
                //            Company company = new Company();

                //            company.Id = (int)reader["Id"];
                //            company.Name = reader["Name"].ToString();
                //            company.FoundedDate = reader["FoundedDate"] != DBNull.Value ? Convert.ToDateTime(reader["FoundedDate"]) : DateTime.MinValue;
                //            retval.Add(company);
                //        }
                //    }
                //}
            }
            return retval;
        }

        public Company Read(string dbConStr, int id)
        {
            Company retval = new Company();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                retval = sqlcon.QueryFirst<Company>(selectCmd, parameters);

                //using (SqlCommand cmd = new SqlCommand($"{selectCmd} WHERE id = @id", sqlcon))
                //{
                //    cmd.Parameters.AddWithValue("@id", id);
                //    sqlcon.Open();

                //    using (SqlDataReader reader = cmd.ExecuteReader())
                //    {
                //        if (reader.Read())
                //        {
                //            company.Id = (int)reader["Id"];
                //            company.Name = reader["Name"].ToString();
                //            company.FoundedDate = reader["FoundedDate"] != DBNull.Value ? Convert.ToDateTime(reader["FoundedDate"]) : DateTime.MinValue;
                //        }
                //    }
                //}
            }

            return retval;
        }

        public Company Create(string dbConStr, Company company)
        {
            return CreateOrUpdate(dbConStr, company);
        }

        public Company Update(string dbConStr, Company company)
        {
            return CreateOrUpdate(dbConStr, company);
        }

        private Company CreateOrUpdate(string dbConStr, Company company)
        {
            Company retval = new Company();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CompanyId", company.Id);
            parameters.Add("@Name", company.Name);
            parameters.Add("@FoundedDate", company.FoundedDate);

            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                int id = sqlcon.ExecuteScalar<int>(spCreateOrUpdate, parameters, commandType: CommandType.StoredProcedure);

                parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                retval = sqlcon.QueryFirst<Company>($"{selectCmd} WHERE id = @id", parameters);

                //using (SqlCommand cmdCreate = new SqlCommand(spCreateOrUpdate, sqlcon))
                //{
                //    cmdCreate.CommandType = CommandType.StoredProcedure;
                //    cmdCreate.Parameters.AddWithValue("@CompanyId", company.Id);
                //    cmdCreate.Parameters.AddWithValue("@Name", company.Name);
                //    cmdCreate.Parameters.AddWithValue("@FoundedDate", company.FoundedDate);

                //    sqlcon.Open();
                //    int id = (int)cmdCreate.ExecuteScalar();

                //    using (SqlCommand cmdRead = new SqlCommand($"{selectCmd} WHERE id = @id", sqlcon))
                //    {
                //        cmdRead.Parameters.AddWithValue("@id", id);

                //        using (SqlDataReader reader = cmdRead.ExecuteReader())
                //        {
                //            if (reader.Read())
                //            {
                //                retval.Id = (int)reader["Id"];
                //                retval.Name = reader["Name"].ToString();
                //                retval.FoundedDate = reader["FoundedDate"] != DBNull.Value ? Convert.ToDateTime(reader["FoundedDate"]) : DateTime.MinValue;
                //            }
                //        }
                //    }
                //}
            }
            return retval;
        }

        public bool Delete(string dbConStr, int id = 0)
        {
            bool retval = false;

            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);

                var result = sqlcon.Execute(deleteCmd, parameters);
                retval = (result == 1);


                //using (SqlCommand cmd = new SqlCommand(deleteCmd, sqlcon))
                //{
                //    cmd.Parameters.AddWithValue("@id", id);
                //    sqlcon.Open();
                //    var result = cmd.ExecuteNonQuery();
                //    retval = (result == 1);
                //}
            }

            return retval;
        }

    }
}
