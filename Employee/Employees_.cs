using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class Employees_
    {
        private readonly ICsvRepository _csvRepository;
        private string csvFilePath;

        public Employees_(string csvPath, ICsvRepository csvRepository)
        {
            _csvRepository = csvRepository;
            csvFilePath = string.IsNullOrEmpty(csvPath) ? throw new ArgumentException(nameof(csvPath)): csvPath;
        }
        public Employees_()
        {

        }
        //An instance method that returns the salary budget from the specified manager.
        public async Task<long> GetManagerBudget(string managerId)
        {
            var employeeDetails = await _csvRepository.FindEmployees(csvFilePath);
            if (employeeDetails != null)
            {
                EmployeeService services = new EmployeeService(employeeDetails);
                return services.GetMangerBudeget(managerId);
            }
            return 0;
        }


        public async Task<List<Employee>> GetEmployeeDetails()
        {
            var employeeDetails = await _csvRepository.FindEmployees(csvFilePath);
            if (employeeDetails != null)
            {
                EmployeeService service = new EmployeeService(employeeDetails);
                //write logic for the method in service
                service.AuthenticateAllEmployees();
                //write logic for the property in service
                if (service.IsAuthentic)
                {
                    return employeeDetails;
                }
                else
                {
                    //write logic for the property in service
                    throw new AggregateException(service.ExceptionLogger);
                }
                
                


            }
            return null;
        }
    }
}
