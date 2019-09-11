using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CompanyApp.Interface;
using CompanyApp.Repository;
using CompanyApp.Model;


namespace CompanyApp.Controller
{
    public class LocationController
    {
        string dbSConStr = "";
        
        readonly IBaseInterface<Company> _companyRepository = new CompanyRepository();

        public LocationController(string DbSConStr)
        {
            dbSConStr = DbSConStr;
        }

        public List<Company> Read()
        {
            return _companyRepository.Read(dbSConStr);
        }

        public Company Read(int id)
        {
            return _companyRepository.Read(dbSConStr, id);
        }

        public Company Create(Company company)
        {
            return _companyRepository.Create(dbSConStr, company);
        }

        public Company Update(Company company)
        {
            return _companyRepository.Update(dbSConStr, company);
        }

        public bool Delete(int id = 0)
        {
            return _companyRepository.Delete(dbSConStr, id);
        }

    }
}
