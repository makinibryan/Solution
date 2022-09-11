using Employees;
using Moq;

namespace Tests;

public class Employees_Test
{
    private readonly Employees_ _employee;
     private string csvFilePath = @"EmployeeData.csv";
   // private string csvFilePath = "C:\\Users\\bmaki\\Desktop\\technobrain\\Employees\\Solution\\Employee\\EmployeeData.csv";
    private readonly Mock<ICsvRepository> csvRepoMock = new Mock<ICsvRepository>();

    public Employees_Test()
    {
        _employee = new Employees_(csvFilePath, csvRepoMock.Object);
    }
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Employees_ThrowsArgumentNullExceptionWhenCSVPathIsNotValidated(string path)
    {
        Assert.Throws<ArgumentException>(() => new Employees_(path, csvRepoMock.Object));
    }

    [Fact]
    public async Task Employees_ThrowsAggregateExceptionWhenEmployeesAreNotValidated()
    {
        List<Employee> employees = new List<Employee>()
        {
            Employee.AddNewEmployee("Employee0", "", 100),
            Employee.AddNewEmployee("Employee2", "Employee1", 100),
            Employee.AddNewEmployee("Employee1", "Employee2", 100),
            Employee.AddNewEmployee("Employee1", "", 1100)
        };
        csvRepoMock.Setup(j => j.FindEmployees(csvFilePath)).ReturnsAsync(employees);
        await Assert.ThrowsAsync<AggregateException>(async () => await _employee.GetEmployeeDetails());

    }


    [Fact]
    public async Task FindManagerSalaryBudgetReturnsZeroWhenEmployeeListIsNull()
    {
        csvRepoMock.Setup(k => k.FindEmployees(csvFilePath)).ReturnsAsync((List<Employee>)null);
        var actual = await _employee.GetManagerBudget("Employee1");
        Assert.Equal(0, actual);
    }


    [Theory]
    [InlineData("Employee2", 1800)]
    [InlineData("Employee3", 500)]
    [InlineData("Employee1", 3800)]

    public async Task FindManagerBudgetReturnsManagerBudget(string employeeId, long budget)
    {
        List<Employee> employees = new List<Employee>()
        {
            Employee.AddNewEmployee("Employee1", "", 1000),
            Employee.AddNewEmployee("Employee2", "Employee1", 1000),
            Employee.AddNewEmployee("Employee3", "Employee1", 1000),
            Employee.AddNewEmployee("Employee4", "Employee2", 1000),
            Employee.AddNewEmployee("Employee5", "Employee1", 1000),
            Employee.AddNewEmployee("Employee6", "Employee2", 1000)
        };
        csvRepoMock.Setup(k => k.FindEmployees(csvFilePath)).ReturnsAsync(employees);
        var actual = await _employee.GetManagerBudget(employeeId);  
        Assert.Equal(budget, actual);
    }
    
}