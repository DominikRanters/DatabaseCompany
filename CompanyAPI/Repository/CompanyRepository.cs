
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using CompanyAPI.Interface;
using CompanyAPI.Model;
using CompanyAPI.Model.Dto;
using Dapper;
using System.Threading.Tasks;

namespace CompanyAPI.Repository
{
    public class CompanyRepository : IBaseInterface<Company, CompanyDto>
    {
        private readonly IDbContext _dbContext; 

        string selectCmd = "select Id, Name, FoundedDate from viCompany";
        string deleteCmd = "update company set DeleteTime = GetDate() where Id = @id";
        string spCreateOrUpdate = "spCreateOrUpdateCompany";

        public CompanyRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Company>> Read()
        {
            using (var sqlcon =  await _dbContext.GetConnection())
            {
                return sqlcon.Query<Company>(selectCmd).AsList();
            }
        }

        public async Task<Company> Read(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var sqlcon = await _dbContext.GetConnection())
            {
                return sqlcon.QueryFirstOrDefault<Company>($"{selectCmd} WHERE Id = @id", parameters);
            }
        }
        public async Task<bool> Create(CompanyDto companyDto)
        {
            var company = new Company()
            {
                Name = companyDto.Name,
                FoundedDate = companyDto.FoundedDate
            };
            return await CreateOrUpdate(company);
        }

        public async Task<bool> Update(int id, CompanyDto companyDto)
        {
            var company = new Company()
            {
                Id = id,
                Name = companyDto.Name,
                FoundedDate = companyDto.FoundedDate
            };
            return await CreateOrUpdate(company);
        }

        private async Task<bool> CreateOrUpdate(Company company)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CompanyId", company.Id);
            parameters.Add("@Name", company.Name);
            parameters.Add("@FoundedDate", company.FoundedDate);

            using (var sqlcon = await _dbContext.GetConnection())
            {
                return 1 == (sqlcon.Execute(spCreateOrUpdate, parameters, commandType: CommandType.StoredProcedure));
            }
        }

        public async Task<bool> Delete(int id = 0)
        {
            if (id < 1)
                throw new Helper.RepoException(Helper.RepoResultType.WORNGPARAMETER);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);

            try
            {
                using (IDbConnection sqlcon = await _dbContext.GetConnection())
                {
                    return 1 == (sqlcon.Execute(deleteCmd, parameters));
                    //if (!retval)
                    //    throw new Helper.RepoException(Helper.RepoResultType.NOTFOUND);
                }
            }
            catch (SqlException ex)
            {
                throw new Helper.RepoException(Helper.RepoResultType.SQL_ERROR);
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
