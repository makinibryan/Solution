using Employees;

namespace Tests;

public class EmployeeServiceTest
{
    [Fact]
    public void VallidatesAllEmployeesAndThrowsExceptionWhenMoreThanOneCEOExiSts()
    {
       List<Employee> employees = new List<Employee>
       {
           Employee.AddNewEmployee("Employee1", "", 100),
           Employee.AddNewEmployee("Employee1", null, 100)

       };
        EmployeeService employeeService = new EmployeeService(employees);
        employeeService.AuthenticateAllEmployees();
        Assert.False(employeeService.IsAuthentic);
        Assert.Contains(employeeService.ExceptionLogger, m => m.Message == "An additional CEO has been detected!");

    }
    [Fact]
    public void ValidatesIfEmployeeHasMoreThanOneManagerAndThrowsExceptionWhenEmployeeHasMoreThanOneManager()
    {
        List<Employee> employees = new List<Employee>
      {
           Employee.AddNewEmployee("Employee0","",100),
           Employee.AddNewEmployee("Employee2","Employee1",100),
           Employee.AddNewEmployee("Employee1","Employee2",100)
      };
        EmployeeService employeeService = new EmployeeService(employees);
        employeeService.AuthenticateAllEmployees();
        Assert.False(employeeService.IsAuthentic);
        Assert.Contains(employeeService.ExceptionLogger, m => m.Message == "A Cyclic Reference has occurred");


    }
    [Theory]
    [InlineData("")]
    public void GetManagerSalaryBudgetAndThrowsArgumentNullExceptionWhenIdIsNotValidated(string managerId)
    {
        EmployeeService employeeService = new EmployeeService(new List<Employee>());
        Assert.Throws<ArgumentNullException>(nameof(managerId), () => employeeService.GetMangerBudeget(managerId));

    }

    [Theory]
    [InlineData("Employee2", 1000)]
    [InlineData("Employee3", 500)]
    [InlineData("Employee1", 3800)]
    public void GetManagersSalaryBudgetAndReturnsTheBudget( string managerId, long expectedsalary)
    {
        List<Employee> employees = new List<Employee>
        {
            Employee.AddNewEmployee("Employee1", "", 1000),
            Employee.AddNewEmployee("Employee2", "", 800),
            Employee.AddNewEmployee("Employee3", "", 500),
            Employee.AddNewEmployee("Employee4", "", 500),
            Employee.AddNewEmployee("Employee5", "", 500),
            Employee.AddNewEmployee("Employee6", "", 500),
        };
        EmployeeService employeeService = new EmployeeService(employees);
        var actualSalary = employeeService.GetMangerBudeget(managerId);
        Assert.Equal(expectedsalary, actualSalary);
    }
}