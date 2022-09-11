using Employees;

namespace Tests;

public class FetchEmployeesFromCsvTest
{
    [Fact]
    public async Task FetchingOfEmployeeFromCsvReturnsNewEmployee()
    {
        string csvline = "Employee1,Employee0,100";
        var employee = FetchListOfEmployeesFromCSV.GetEmployeeDetailsFromCsvLine(csvline);
        
        Assert.Equal("Employee0", employee.ManagerId);
    }
    [Fact]
    public void  FetchingEmployeeFromCsvReturnsNullWhenInputIsNotValidated()
    {
        string csvline = "Employee1,Employee0, 3500";
        var employee = FetchListOfEmployeesFromCSV.GetEmployeeDetailsFromCsvLine(csvline);
        Assert.Null(employee);
    }
}