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
        string dbSConStr = "";

        readonly IBaseInterface<Company> _companyRepository;

        public CompanyController(string DbSConStr)
        {
            _companyRepository = new CompanyRepository(DbSConStr);
        }

        public List<Company> Read()
        {
            return _companyRepository.Read();
        }

        public Company Read(int id)
        {
            return _companyRepository.Read(id);
        }

        public Company Create(Company company)
        {
            return _companyRepository.Create(company);
        }

        public Company Update(Company company)
        {
            return _companyRepository.Update(company);
        }

        public bool Delete(int id = 0)
        {
            return _companyRepository.Delete(id);
        }

    }
}
