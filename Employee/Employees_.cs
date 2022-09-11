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
        public async Task<long> GetManagerBudget(string managerId)
        {

        }
    }
}
