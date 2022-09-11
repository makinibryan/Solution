namespace Employees
{
    public class EmployeeService
    {
        private List<Employee> _employeeDetails;

        public EmployeeService(List<Employee> _employeeDetails)
        {
            _employeeDetails = _employeeDetails ?? throw new ArgumentNullException(nameof(_employeeDetails));  
        }


        public bool IsAuthentic { get; private set; } = true;
        public List<Exception> ExceptionLogger { get; private set; } = new List<Exception>();

        public long GetMangerBudeget(string managerId)
        {
            if (managerId == String.Empty) throw new ArgumentNullException(nameof(managerId));
            double TotalBudegetSalaryFromAllEmployees = 0;
            TotalBudegetSalaryFromAllEmployees += _employeeDetails.FirstOrDefault(i => i.Id == managerId).Salary;

            foreach (var employee in _employeeDetails.Where( i => i.ManagerId == managerId))
            {
                if (EmployeeIsManager(employee.Id))
                {
                     TotalBudegetSalaryFromAllEmployees += GetMangerBudeget(employee.Id);
                }
                else
                {
                    TotalBudegetSalaryFromAllEmployees += employee.Salary;
                }


                

            }
            return Convert.ToInt32(TotalBudegetSalaryFromAllEmployees);

        }

        private bool EmployeeIsManager(string? id) => _employeeDetails.Where(i => i.ManagerId == id).Count() > 0;
        

        public void AuthenticateAllEmployees()
        {
            Parallel.Invoke(
                () => { AuthenitacateNumberofOrganaizationCeos(); },
                () => { AuthenticateEmployeeWithMoreThanOneManager(); },
                () => { AuthenticateThatAllManagersAreDisplayed(); },
                () => { AuthenticateCircularReferencingWithinTheOrg(); }

                );
        }
        //There is no circular reference, i.e. a first employee reporting to a second employee that is also under the first employee.
        private void AuthenticateCircularReferencingWithinTheOrg()
        {
            foreach (var _ in from item in _employeeDetails.Where(f => f.ManagerId != String.Empty && f.ManagerId != null)
                              let manager = _employeeDetails.Where(f => f.ManagerId != String.Empty && f.ManagerId != null)
                              .FirstOrDefault(f => f.Id == item.ManagerId)
                              where manager != null
                              where manager.ManagerId == item.Id
                              select new {})
            {
                IsAuthentic = false;
                ExceptionLogger.Add(new Exception("A Cyclic Reference has occurred"));
            }
        }
        //There is no manager that is not an employee, i.e. all managers are also listed in the employee column.
        private void AuthenticateThatAllManagersAreDisplayed()
        {
            foreach (var employee in _employeeDetails.Where(u => u.ManagerId != null && u.ManagerId != String.Empty)
                .Select(t => t.ManagerId)
                .Where(m  => _employeeDetails.FirstOrDefault(w => w.Id == m) == null).Select(m => new {}))
            {
                IsAuthentic = false;
                ExceptionLogger.Add(new Exception("Missing managers detected"));
            }
        }
        //One employee does not report to more than one manager.
        private void AuthenticateEmployeeWithMoreThanOneManager()
        {
            foreach (var employeeid in _employeeDetails.Select(r => r.Id).Distinct().Where(id => _employeeDetails.Where(i => i.Id == id)
            .Select(m => m.ManagerId).Distinct().Count() > 1).Select(id => id))
            {
                IsAuthentic = false;
                ExceptionLogger.Add(new Exception($"The Employee {employeeid} has more than one manager"));
            }
        }
        //There is only one CEO, i.e. only one employee with no manager.
        private void AuthenitacateNumberofOrganaizationCeos()
        {
            if (_employeeDetails.Where(i => i.ManagerId == String.Empty || i.ManagerId == null).Count() > 1)
            {
                IsAuthentic = false;
                ExceptionLogger.Add(new Exception("An additional CEO has been detected!"));
            }
        }
    }
}