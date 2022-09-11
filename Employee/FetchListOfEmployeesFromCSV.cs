using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public static class FetchListOfEmployeesFromCSV
    {
        static public Employee GetEmployeeDetailsFromCsvLine(this string Line)
        {
            string[] Csvlinesecstions = Line.Split('\u002C');
            if (Csvlinesecstions.Length == 3)
            {
                var Id = Csvlinesecstions[0];
                var EmployeeManagerId = Csvlinesecstions[1];
                var EmployeeSalary = Csvlinesecstions[2];
                int.TryParse(EmployeeSalary, out int salary);

                return Employee.AddNewEmployee(Id, EmployeeManagerId, salary);
            }
            //returns null 

            return null;
        }
    }
}
