using Employees;

namespace Tests;

public class EmployeeTest
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]

    public  void CreateThrowsArgumentNullException_WhenIdIsNotValidated(string  id)
    {
        var exception = () => Employee.AddNewEmployee(id,null, default);
        Assert.Throws<ArgumentNullException>(nameof( id), exception );

    }

    [Fact]
    public void CreateThrowsArgmentOutOfRangeExceptionWhenSalaryIsNotAuthentic()
    {
        int salary = -1;
        Assert.Throws<ArgumentOutOfRangeException>(nameof(salary), () => Employee.AddNewEmployee("Empl001", null, salary));
    }
    [Fact]
    public void CreateCreatesNewEmployee()
    {
        //parameters
        var employee = Employee.AddNewEmployee("Employee1","", 100);
        Assert.Equal("Employee1", employee.Id);
        Assert.Equal(100, employee.Salary);

    }
}