using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace APIConsume
{
    class Program
    {
        private static IList<Employee> employeeInMemoryDb;
        static void Main(string[] args)
        {

            var employees = AllEmployees();

            File.WriteAllText("employees2.json", JsonConvert.SerializeObject(employees));

            employeeInMemoryDb = Greater1000Age25();

            Console.WriteLine(JsonConvert.SerializeObject(employeeInMemoryDb));

            Console.WriteLine("Done!");
            Console.ReadKey();
            }

        /// <summary>
        /// Get all employees from safaricom dummy data api
        /// </summary>
        /// <returns></returns>
        public static List<Employee> AllEmployees()
        {
            var client = new RestClient("http://dummy.restapiexample.com");

            var request = new RestRequest("api/v1/employees", Method.GET);

            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<List<Employee>>(response.Content);

            //write to file

            return result;
        }

        public static List<Employee> Greater1000Age25()
        {
            var employees = AllEmployees();

            return employees.Where(x => x.employee_age > 25 && x.employee_salary > 1000).ToList();
        }

        public static void CreateEmployee(Employee employee)
        {
            var client = new RestClient("http://dummy.restapiexample.com");

            var request = new RestRequest("api/v1/create", Method.POST);
            request.AddJsonBody(employee);

            var response = client.Execute(request);

            Console.WriteLine(response.Content);
        }

        public static void UpdateEmployee(int id,Employee employee)
        {
            var client = new RestClient("http://dummy.restapiexample.com");

            var request = new RestRequest($"api/v1/update/{id}", Method.PUT);
            request.AddJsonBody(employee);

            var response = client.Execute(request);

            Console.WriteLine(response.Content);
        }


    }

    public class Employee
    {
        public long id { get; set; }
        public string employee_name { get; set; }
        public double employee_salary { get; set; }
        public int employee_age { get; set; }
        public string profile_image { get; set; }
    }


}
