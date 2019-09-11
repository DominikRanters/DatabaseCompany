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
<<<<<<< HEAD
        string dbConStr = "";
        
        readonly IBaseInterface<Company> _companyRepository = new CompanyRepository();
=======
        string dbSConStr = "";

        readonly IBaseInterface<Company> _companyRepository;
>>>>>>> develop

        public CompanyController(string dbConnectingStr)
        {
<<<<<<< HEAD
            dbConStr = dbConnectingStr;
=======
            _companyRepository = new CompanyRepository(DbSConStr);
>>>>>>> develop
        }

        public List<Company> Read()
        {
<<<<<<< HEAD
            return _companyRepository.Read(dbConStr);
=======
            return _companyRepository.Read();
>>>>>>> develop
        }

        public Company Read(int id)
        {
<<<<<<< HEAD
            return _companyRepository.Read(dbConStr, id);
=======
            return _companyRepository.Read(id);
>>>>>>> develop
        }

        public Company Create(Company company)
        {
<<<<<<< HEAD
            return _companyRepository.Create(dbConStr, company);
=======
            return _companyRepository.Create(company);
>>>>>>> develop
        }

        public Company Update(Company company)
        {
<<<<<<< HEAD
            return _companyRepository.Update(dbConStr, company);
=======
            return _companyRepository.Update(company);
>>>>>>> develop
        }

        public bool Delete(int id = 0)
        {
<<<<<<< HEAD
            return _companyRepository.Delete(dbConStr, id);
=======
            return _companyRepository.Delete(id);
>>>>>>> develop
        }

    }
}
