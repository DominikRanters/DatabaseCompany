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
    public class CompanyController
    {
        string dbConStr = "";
        
        readonly IBaseInterface<Company> _companyRepository = new CompanyRepository();

        public CompanyController(string dbConnectingStr)
        {
            dbConStr = dbConnectingStr;
        }

        public List<Company> Read()
        {
            return _companyRepository.Read(dbConStr);
        }

        public Company Read(int id)
        {
            return _companyRepository.Read(dbConStr, id);
        }

        public Company Create(Company company)
        {
            return _companyRepository.Create(dbConStr, company);
        }

        public Company Update(Company company)
        {
            return _companyRepository.Update(dbConStr, company);
        }

        public bool Delete(int id = 0)
        {
            return _companyRepository.Delete(dbConStr, id);
        }

    }
}
