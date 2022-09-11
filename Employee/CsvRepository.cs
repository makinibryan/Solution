using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class CsvRepository
    {
        public async Task<List<Employee>> FindEmployees(string csvFilePath)
        {
            return await Task.Run(() =>
            {
                return File.ReadAllLines(csvFilePath).Skip(1).Where(p => p.Length > 1).Select(o => o.GetEmployeeDetailsFromCsvLine()).ToList();
            })
        }
    }
}
