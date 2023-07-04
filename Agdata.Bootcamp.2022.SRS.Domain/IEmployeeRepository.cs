using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agdata.Bootcamp._2022.SRS.Domain.Repositories;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;

namespace Agdata.Bootcamp._2022.SRS.Domain
{
    public interface IEmployeeRepository
    {
        Task<int> Add(EmployeeViewModel employee);

        Task<EmployeeViewModel> GetByEmployeeId(int employeeId);

        Task<List<EmployeeViewModel>> GetAll();

        Task<int> AddRange(List<EmployeeViewModel> employees);

        Task<EmployeeViewModel?> GetByEmployeeName(string employeeName);

    }
}
