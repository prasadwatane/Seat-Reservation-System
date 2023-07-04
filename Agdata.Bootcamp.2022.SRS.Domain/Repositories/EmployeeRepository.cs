using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agdata.Bootcamp._2022.SRS.Domain.Model;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using AutoMapper;
using Agdata.Bootcamp._2022.SRS.Domain.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Agdata.Bootcamp._2022.SRS.Domain.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ISRSDbContext _srsDbContext;
        private readonly IMapper _mapper;

        public EmployeeRepository(ISRSDbContext srsContext, IMapper mapper)
        {
            _srsDbContext = srsContext;
            _mapper = mapper;
        }

        public async Task<int> Add(EmployeeViewModel employeeVm)
        {
            var employee = _mapper.Map<Employee>(employeeVm);
            _srsDbContext.Employees.Add(employee);
            var test =  await _srsDbContext.SaveChangesAsync();
            return test;
        }

        public async Task<int> AddRange(List<EmployeeViewModel> employees)
        {

            await _srsDbContext.Employees.AddRangeAsync(_mapper.Map<List<Employee>>(employees));
            return await _srsDbContext.SaveChangesAsync();

        }

        public async Task<List<EmployeeViewModel>> GetAll()
        {
            var EmployeeVMList =  _mapper.Map<List<Employee>, List<EmployeeViewModel>>(await _srsDbContext.Employees.ToListAsync());
            return EmployeeVMList;                       
        }

        public async Task<EmployeeViewModel> GetByEmployeeId(int employeeId)
        {
            var employee= _mapper.Map<EmployeeViewModel>(await _srsDbContext.Employees.FindAsync(employeeId));
            return employee;
                      
        }

        public async Task<EmployeeViewModel?> GetByEmployeeName(string employeeName)
        {
            var employeeList = await _srsDbContext.Employees.ToListAsync();
            foreach(var employee in employeeList)
            {
                if (employeeName == employee.EmployeeName)
                {
                    var employeeVm = _mapper.Map<EmployeeViewModel>(employee);
                    return employeeVm;
                }                    
            }
            return null;
        }


        
    }
}
