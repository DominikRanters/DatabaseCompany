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
        string dbConStr = "";
        string selectCmd = "select Id, Name, FoundedDate from viCompany";
        string deleteCmd = "update company set DeleteTime = GetDate() where Id = @id";
        string spCreateOrUpdate = "spCreateOrUpdateCompany";
        public CompanyRepository(string dbConnectionStr)
        {
            dbConStr = dbConnectionStr;
        }

        public List<Company> Read()
        {
            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                return sqlcon.Query<Company>(selectCmd).AsList();
            }
        }

        public Company Read(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                return sqlcon.QueryFirst<Company>($"{selectCmd} WHERE Id = @id", parameters);
            }
        }
        public Company Create(Company company)
        {
            return CreateOrUpdate(company);
        }

        public Company Update(Company company)
        {
            return CreateOrUpdate(company);
        }

        private Company CreateOrUpdate(Company company)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CompanyId", company.Id);
            parameters.Add("@Name", company.Name);
            parameters.Add("@FoundedDate", company.FoundedDate);

            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                int id = sqlcon.ExecuteScalar<int>(spCreateOrUpdate, parameters, commandType: CommandType.StoredProcedure);

                parameters = new DynamicParameters();
                parameters.Add("@id", id);

                return sqlcon.QueryFirst<Company>($"{selectCmd} WHERE Id = @id", parameters);
            }
        }

        public bool Delete(int id = 0)
        {
            using (SqlConnection sqlcon = new SqlConnection(dbConStr))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);

                return 1 == (sqlcon.Execute(deleteCmd, parameters));
            }
        }


        #region Without Dapper
        //public List<Company> Read()
        //{
        //    List<Company> retval = new List<Company>();

        //    using (SqlConnection sqlcon = new SqlConnection(dbConStr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(selectCmd, sqlcon))
        //        {
        //            sqlcon.Open();

        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    Company company = new Company();

        //                    company.Id = (int)reader["Id"];
        //                    company.Name = reader["Name"].ToString();
        //                    company.FoundedDate = reader["FoundedDate"] != DBNull.Value ? Convert.ToDateTime(reader["FoundedDate"]) : DateTime.MinValue;
        //                    retval.Add(company);
        //                }
        //            }
        //        }
        //    }
        //    return retval;
        //}

        //public Company Read(int id)
        //{
        //    Company retval = new Company();

        //    using (SqlConnection sqlcon = new SqlConnection(dbConStr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand($"{selectCmd} WHERE id = @id", sqlcon))
        //        {
        //            cmd.Parameters.AddWithValue("@id", id);
        //            sqlcon.Open();

        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    retval.Id = (int)reader["Id"];
        //                    retval.Name = reader["Name"].ToString();
        //                    retval.FoundedDate = reader["FoundedDate"] != DBNull.Value ? Convert.ToDateTime(reader["FoundedDate"]) : DateTime.MinValue;
        //                }
        //            }
        //        }
        //    }
        //    return retval;
        //}

        //public Company Create(Company company)
        //{
        //    return CreateOrUpdate(company);
        //}

        //public Company Update(Company company)
        //{
        //    return CreateOrUpdate(company);
        //}

        //private Company CreateOrUpdate(Company company)
        //{
        //    Company retval = new Company();

        //    using (SqlConnection sqlcon = new SqlConnection(dbConStr))
        //    {
        //        using (SqlCommand cmdCreate = new SqlCommand(spCreateOrUpdate, sqlcon))
        //        {
        //            cmdCreate.CommandType = CommandType.StoredProcedure;
        //            cmdCreate.Parameters.AddWithValue("@CompanyId", company.Id);
        //            cmdCreate.Parameters.AddWithValue("@Name", company.Name);
        //            cmdCreate.Parameters.AddWithValue("@FoundedDate", company.FoundedDate);

        //            sqlcon.Open();
        //            int id = (int)cmdCreate.ExecuteScalar();

        //            using (SqlCommand cmdRead = new SqlCommand($"{selectCmd} WHERE id = @id", sqlcon))
        //            {
        //                cmdRead.Parameters.AddWithValue("@id", id);

        //                using (SqlDataReader reader = cmdRead.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        retval.Id = (int)reader["Id"];
        //                        retval.Name = reader["Name"].ToString();
        //                        retval.FoundedDate = reader["FoundedDate"] != DBNull.Value ? Convert.ToDateTime(reader["FoundedDate"]) : DateTime.MinValue;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return retval;
        //}

        //public bool Delete(int id = 0)
        //{
        //    bool retval = false;

        //    using (SqlConnection sqlcon = new SqlConnection(dbConStr))
        //    {

        //        using (SqlCommand cmd = new SqlCommand(deleteCmd, sqlcon))
        //        {
        //            cmd.Parameters.AddWithValue("@id", id);
        //            sqlcon.Open();
        //            var result = cmd.ExecuteNonQuery();
        //            retval = (result == 1);
        //        }
        //    }

        //    return retval;
        //} 
        #endregion

    }
}
