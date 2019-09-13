using System;
using System.Collections.Generic;
using System.Text;
using CompanyApp.Interface;
using CompanyApp.Repository;
using CompanyApp.Model;

namespace CompanyApp.Controller
{
    public class EmployeeController
    {
        readonly IBaseInterface<Employee> _employeeRepository;

       public EmployeeController(string dbConnectionStr)
        {
            _employeeRepository = new EmployeeRepository(dbConnectionStr);
        }

        public List<Employee> Read()
        {
            return _employeeRepository.Read();
        }

        public Employee Read(int id)
        {
            return _employeeRepository.Read(id);
        }

        public Employee Create(Employee employee)
        {
            return _employeeRepository.Create(employee);
        }

        public Employee Update(Employee employee)
        {
            return _employeeRepository.Update(employee);
        }

        public bool Delete(int id)
        {
            return _employeeRepository.Delete(id);
        }
    }
}
