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
                    Company companyModel = new Company();
                    Company companyData;


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
                                    foreach (var company in companies)
                                    {
                                        Console.WriteLine($"id={company.Id}, name={company.Name}, foudedDate={company.FoundedDate}");
                                    }
                                    break;

                                default:
                                    companyData = companyController.Read(Convert.ToInt32(action));
                                    Console.WriteLine($"id={companyData.Id}, name={companyData.Name}, foudedDate={companyData.FoundedDate}");
                                    break;

                            }
                            break;

                        case "create":
                            Console.WriteLine("What is the company name?");
                            companyModel.Name = Console.ReadLine();

                            Console.WriteLine("What is the founding date(yyyy-mm-dd)? (can be empty)");
                            input = Console.ReadLine();
                            companyModel.FoundedDate = input == "" ? null : (DateTime?)Convert.ToDateTime(input);

                            companyData = companyController.Create(companyModel);
                            Console.WriteLine($"id={companyData.Id}, name={companyData.Name}, foudedDate={companyData.FoundedDate}");

                            break;

                        case "update":
                            Console.WriteLine("Enter the ID of the company you want to change");
                            companyModel.Id = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("What is the new company name? (no change then press enter)");
                            companyModel.Name = Console.ReadLine();
                            companyModel.Name = companyModel.Name == "" ? null : companyModel.Name;

                            Console.WriteLine("What is the new founding date? (no change then press enter)");
                            input = Console.ReadLine();
                            companyModel.FoundedDate = input == "" ? null : (DateTime?)Convert.ToDateTime(input);


                            companyData = companyController.Update(companyModel);
                            Console.WriteLine($"id={companyData.Id}, name={companyData.Name}, foudedDate={companyData.FoundedDate}");

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
                    Employee employeeModel = new Employee();
                    Employee employeeData;

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
                                    var employees = employeeController.Read();
                                    foreach (var employee in employees)
                                    {
                                        Console.WriteLine($"\nId={employee.Id}, FirstName={employee.FirstName}, LastName={employee.LastName}" +
                                                            $", Birthday={employee.Birthday}, DepartmentId={employee.DepartmentId}, AdressId={employee.AddressId}");
                                    }
                                    break;

                                default:
                                    employeeData = employeeController.Read(Convert.ToInt32(action));
                                    Console.WriteLine($"\nId={employeeData.Id}, FirstName={employeeData.FirstName}, LastName={employeeData.LastName}" +
                                                        $", Birthday={employeeData.Birthday}, DepartmentId={employeeData.DepartmentId}, AdressId={employeeData.AddressId}");
                                    break;

                            }
                            break;

                        case "create":
                            Console.WriteLine("What is the company name?");
                            companyModel.Name = Console.ReadLine();

                            Console.WriteLine("What is the founding date(yyyy-mm-dd)? (can be empty)");
                            input = Console.ReadLine();
                            companyModel.FoundedDate = input == "" ? null : (DateTime?)Convert.ToDateTime(input);

                            companyData = companyController.Create(companyModel);
                            Console.WriteLine($"id={companyData.Id}, name={companyData.Name}, foudedDate={companyData.FoundedDate}");

                            break;

                        case "update":
                            Console.WriteLine("Enter the ID of the company you want to change");
                            companyModel.Id = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("What is the new company name? (no change then press enter)");
                            companyModel.Name = Console.ReadLine();
                            companyModel.Name = companyModel.Name == "" ? null : companyModel.Name;

                            Console.WriteLine("What is the new founding date? (no change then press enter)");
                            input = Console.ReadLine();
                            companyModel.FoundedDate = input == "" ? null : (DateTime?)Convert.ToDateTime(input);


                            companyData = companyController.Update(companyModel);
                            Console.WriteLine($"id={companyData.Id}, name={companyData.Name}, foudedDate={companyData.FoundedDate}");

                            break;

                        case "delete":
                            Console.WriteLine("Geben Sie bitte die id von dem Datensatz an, den sie löschen möchte");
                            id = Convert.ToInt32(Console.ReadLine());
                            companyController.Delete(id);
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
