using System;
using CompanyApp.Model;

namespace CompanyApp
{
    class Program
    {
        const string CONSTRING_TAPPQA = "Data Source=tappqa;Initial Catalog=Training-DS-Company;Integrated Security=True";
        static void Main(string[] args)
        {
            // Variables
            string input, action;
            int id = 0;

            Controller.CompanyController companyController = new Controller.CompanyController(CONSTRING_TAPPQA);
            Controller.EmployeeController employeeController = new Controller.EmployeeController(CONSTRING_TAPPQA);

            Console.WriteLine("An welcher Tabelle wollen Sie was ändern? (Company, Employee, Address)");
            action = Console.ReadLine();

            switch (action.ToLower())
            {
                case "company":
                    // Company
                    #region Company
                    Company company = new Company();


                    Console.WriteLine("What would you do? (Read, Create, Update, Delete)");
                    action = Console.ReadLine();

                    switch (action.ToLower())
                    {
                        case "read":
                            Console.WriteLine("All column or search a id? ('All' or the id you search for)");
                            action = Console.ReadLine();
                            Console.WriteLine();

                            switch (action.ToLower())
                            {
                                case "all":
                                    var companies = companyController.Read();
                                    foreach (var companyItem in companies)
                                    {
                                        Console.WriteLine($"id={companyItem.Id}, name={companyItem.Name}, foudedDate={companyItem.FoundedDate}");
                                    }
                                    break;

                                default:
                                    company = companyController.Read(Convert.ToInt32(action));
                                    Console.WriteLine($"id={company.Id}, name={company.Name}, foudedDate={company.FoundedDate}");
                                    break;

                            }
                            break;

                        case "create":
                            Console.WriteLine("What is the company name?");
                            company.Name = Console.ReadLine();

                            Console.WriteLine("What is the founding date(yyyy-mm-dd)? (can be empty)");
                            input = Console.ReadLine();
                            company.FoundedDate = input == "" ? null : (DateTime?)Convert.ToDateTime(input);

                            company = companyController.Create(company);
                            Console.WriteLine($"id={company.Id}, name={company.Name}, foudedDate={company.FoundedDate}");

                            break;

                        case "update":
                            Console.WriteLine("Enter the ID of the company you want to change");
                            company.Id = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("What is the new company name? (no change then press enter)");
                            company.Name = Console.ReadLine();
                            company.Name = company.Name == "" ? null : company.Name;

                            Console.WriteLine("What is the new founding date(yyyy-mm-dd)? (no change then press enter)");
                            input = Console.ReadLine();
                            company.FoundedDate = input == "" ? null : (DateTime?)Convert.ToDateTime(input);


                            company = companyController.Update(company);
                            Console.WriteLine($"id={company.Id}, name={company.Name}, foudedDate={company.FoundedDate}");

                            break;

                        case "delete":
                            Console.WriteLine("Geben Sie bitte die id von dem Datensatz an, den sie löschen möchte");
                            id = Convert.ToInt32(Console.ReadLine());
                            companyController.Delete(id);
                            break;
                    }
                    #endregion
                    break;

                case "employee":
                    // Employee
                    #region Employee
                    Employee employee = new Employee();

                    Console.WriteLine("What would you do? (Read, Create, Update, Delete)");
                    action = Console.ReadLine();

                    switch (action.ToLower())
                    {
                        case "read":

                            Console.WriteLine("All column or search a id? ('All' or the id you search for)");
                            action = Console.ReadLine();

                            switch (action.ToLower())
                            {
                                case "all":
                                    Console.WriteLine();
                                    var employees = employeeController.Read();
                                    foreach (var employeeItem in employees)
                                    {
                                        Console.WriteLine($"Id={employeeItem.Id}, FirstName={employeeItem.FirstName}, LastName={employeeItem.LastName}" +
                                                            $", Birthday={employeeItem.Birthday}, DepartmentId={employeeItem.DepartmentId}, AdressId={employeeItem.AddressId}");
                                    }
                                    break;

                                default:
                                    employee = employeeController.Read(Convert.ToInt32(action));
                                    Console.WriteLine($"\nId={employee.Id}, FirstName={employee.FirstName}, LastName={employee.LastName}" +
                                                        $", Birthday={employee.Birthday}, DepartmentId={employee.DepartmentId}, AdressId={employee.AddressId}");
                                    break;

                            }
                            break;

                        case "create":
                            Console.WriteLine("\nfirst name");
                            employee.FirstName = Console.ReadLine();

                            Console.WriteLine("\nlast name");
                            employee.LastName = Console.ReadLine();

                            Console.WriteLine("\nbirthday (yyyy-mm-dd) (can be empty)");
                            input = Console.ReadLine();
                            employee.Birthday = input == "" ? null : (DateTime?)Convert.ToDateTime(input);

                            Console.WriteLine("\ndepartment id");
                            employee.DepartmentId = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("\naddress id (can be empty)");
                            employee.AddressId = Convert.ToInt32(Console.ReadLine());

                            employee = employeeController.Create(employee);
                            Console.WriteLine($"\nId={employee.Id}, FirstName={employee.FirstName}, LastName={employee.LastName}" +
                                                        $", Birthday={employee.Birthday}, DepartmentId={employee.DepartmentId}, AdressId={employee.AddressId}");

                            break;

                        case "update":
                            Console.WriteLine("Enter the ID of the employee you want to change");
                            employee.Id = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("\nfirst name (no change then press enter)");
                            employee.FirstName = Console.ReadLine();

                            Console.WriteLine("\nlast name (no change then press enter)");
                            employee.LastName = Console.ReadLine();

                            Console.WriteLine("\nbirthday (yyyy-mm-dd)  (no change then press enter)");
                            input = Console.ReadLine();
                            employee.Birthday = input == "" ? null : (DateTime?)Convert.ToDateTime(input);

                            Console.WriteLine("\ndepartment id (no change then press enter)");
                            employee.DepartmentId = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("\naddress id (no change then press enter)");
                            employee.AddressId = Convert.ToInt32(Console.ReadLine());

                            employee = employeeController.Create(employee);
                            Console.WriteLine($"\nId={employee.Id}, FirstName={employee.FirstName}, LastName={employee.LastName}" +
                                                        $", Birthday={employee.Birthday}, DepartmentId={employee.DepartmentId}, AdressId={employee.AddressId}");

                            employee = employeeController.Update(employee);
                            Console.WriteLine($"\nId={employee.Id}, FirstName={employee.FirstName}, LastName={employee.LastName}" +
                                                        $", Birthday={employee.Birthday}, DepartmentId={employee.DepartmentId}, AdressId={employee.AddressId}");

                            break;

                        case "delete":
                            Console.WriteLine("Geben Sie bitte die id von dem Datensatz an, den sie löschen möchte");
                            id = Convert.ToInt32(Console.ReadLine());
                            employeeController.Delete(id);
                            break;
                    }
                    #endregion
                    break;

                case "address":
                    break;
            }


            Console.WriteLine("\nPress Enter to quit");
            Console.ReadKey();

        }
    }
}
