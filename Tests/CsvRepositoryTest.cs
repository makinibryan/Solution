using Employees;

namespace Tests;

public class CsvRepositoryTest
{
    [Fact]
    public async Task FindsAllEmployeesAndReturnsAListOfAllEmployeesWhenCalled()
    {
        CsvRepository csvRepository = new CsvRepository();
        var result = await csvRepository.FindEmployees("EmployeeData.csv");
        Assert.Equal(5, result.Count);
    }
}