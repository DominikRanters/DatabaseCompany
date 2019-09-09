using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace CompanyApp.Controller
{
    class LocationController
    {
        string dbSConStr = "";
        string selectCmd = "select Id, Name, FoundedDate from viCompany";
        string deleteCmd = "update company set DeleteTime = GetDate() where id = @id";
        string spCreateOrUpdate = "spCreateOrUpdateCompany";

        public LocationController(string DbSConStr)
        {
            dbSConStr = DbSConStr;
        }

        public List<Model.Company> Read()
        {
            List<Model.Company> retval = new List<Model.Company>();

            using (SqlConnection sqlcon = new SqlConnection(dbSConStr))
            {
                using (SqlCommand cmd = new SqlCommand(selectCmd, sqlcon))
                {
                    sqlcon.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Model.Company company = new Model.Company();

                            company.Id = (int)reader["Id"];
                            company.Name = reader["Name"].ToString();
                            company.FoundedDate = reader["FoundedDate"] != DBNull.Value ? Convert.ToDateTime(reader["FoundedDate"]) : DateTime.MinValue;
                            retval.Add(company);
                        }
                    }
                }
            }

            return retval;
        }

        public Model.Company Read(int id)
        {
            Model.Company company = new Model.Company();

            using (SqlConnection sqlcon = new SqlConnection(dbSConStr))
            {
                using (SqlCommand cmd = new SqlCommand($"{selectCmd} WHERE id = @id", sqlcon))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    sqlcon.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                    
                            company.Id = (int)reader["Id"];
                            company.Name = reader["Name"].ToString();
                            company.FoundedDate = reader["FoundedDate"] != DBNull.Value ? Convert.ToDateTime(reader["FoundedDate"]) : DateTime.MinValue;
                            
                        }
                    }
                }
            }

            return company;
        }

        public bool Create(Model.Company company)
        {
            bool retval = false;

            retval = CreateOrUpdate(company);

            return retval;
        }

        public bool Update(Model.Company company)
        {
            bool retval = false;

            retval = CreateOrUpdate(company);

            return retval;
        }

        private bool CreateOrUpdate(Model.Company company)
        {
            bool retval = false;

            using (SqlConnection sqlcon = new SqlConnection(dbSConStr))
            {
                using (SqlCommand cmd = new SqlCommand(spCreateOrUpdate, sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CompanyId", company.Id);
                    cmd.Parameters.AddWithValue("@Name", company.Name);
                    cmd.Parameters.AddWithValue("@FoundedDate", company.FoundedDate);

                    sqlcon.Open();
                    var result = cmd.ExecuteNonQuery();
                    retval = (result == 1);
                }
            }

            return retval;
        }


        public bool Delete(int id = 0)
        {
            bool retval = false;

            using (SqlConnection sqlcon = new SqlConnection(dbSConStr))
            {
                using (SqlCommand cmd = new SqlCommand(deleteCmd, sqlcon))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    sqlcon.Open();
                    var result = cmd.ExecuteNonQuery();
                    retval = (result == 1);
                }
            }

            return retval;
        }

    }
}
