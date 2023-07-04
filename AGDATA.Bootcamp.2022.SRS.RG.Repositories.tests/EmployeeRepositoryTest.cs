using Xunit;
using Moq;
using Agdata.Bootcamp._2022.SRS.Domain.Repositories;
using Agdata.Bootcamp._2022.SRS.Domain;
using AutoMapper;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using Agdata.Bootcamp._2022.SRS.Domain.Model;


namespace AGDATA.Bootcamp._2022.SRS.RG.Repositories.tests
{
    public class EmployeeRepositoryTest
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly Mock<ISRSDbContext> _srsDbContextMock = new Mock<ISRSDbContext>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public EmployeeRepositoryTest()
        {
            _employeeRepository = new EmployeeRepository(_srsDbContextMock.Object, _mapperMock.Object);
        }

        //[Fact]
        public async Task AddEmployee_ExpectReturnOne_WhenEmployeeAdded()
        {
            //Arrange
            var employeeName = "Rahul";
            EmployeeViewModel employeeVm = new EmployeeViewModel()
            {
                EmployeeName = employeeName
            };

            Employee employee = new Employee()
            {
                EmployeeName = employeeName
            };

            _mapperMock.Setup(mapper => mapper.Map<Employee>(employeeVm)).Returns(employee);
            _srsDbContextMock.Setup(employeedb => employeedb.Employees.Add(employee));
                     

            //Act
            var status = await _employeeRepository.Add(employeeVm);

            //Assert
            Assert.Equal(1,status);
        }

       [Fact] 
        public async Task GetByEmployeeId_WhenEmployeeIdPAssed_ExpectEmployee()
        {
            //Arrange
            var employeeId = 1;
            var employeeName = "Rahul";
     

            Employee employee1 = new Employee()
            {
                EmployeeId = employeeId,
                EmployeeName = employeeName
            };
            EmployeeViewModel employee2 = new EmployeeViewModel()
            {
                EmployeeId = employee1.EmployeeId,
                EmployeeName = employee1.EmployeeName
            };
            _srsDbContextMock.Setup(employees => employees.Employees.Add(employee1));
            _srsDbContextMock.Setup(employee => employee.Employees.FindAsync(employeeId)).ReturnsAsync(employee1);
            _mapperMock.Setup(mapper => mapper.Map<EmployeeViewModel>(employee1)).Returns(employee2);


            //Act
            var employee3 = await _employeeRepository.GetByEmployeeId(employeeId);

            //Assert
            Assert.Equal(employeeId,employee3.EmployeeId);
            Assert.Equal(employeeName, employee3.EmployeeName);
        }

        [Fact]
        public async Task GetByEmployeeId_WhenEmployeeIdPAssedWhichIsNotPresent_ExpectReturnNull()
        {
            //Arrange
            var employeeId = 1;
            var employeeName = "Rahul";


            Employee employee1 = new Employee()
            {
                EmployeeId = employeeId,
                EmployeeName = employeeName
            };
            EmployeeViewModel employee2 = new EmployeeViewModel()
            {
                EmployeeId = employee1.EmployeeId,
                EmployeeName = employee1.EmployeeName
            };
            _srsDbContextMock.Setup(employees => employees.Employees.Add(employee1));
            _srsDbContextMock.Setup(employee => employee.Employees.FindAsync(5)).ReturnsAsync(() => null);
            _mapperMock.Setup(mapper => mapper.Map<EmployeeViewModel>(employee1)).Returns(() => null);


            //Act
            var employee3 = await _employeeRepository.GetByEmployeeId(employeeId);

            //Assert
            Assert.Null(employee3);
        }

        //[Fact]
        public async Task GetAll_ExpectAllEmployeeList()
        {
            //Arrange
            List<Employee> list = new List<Employee>();
            Employee employee1 = new Employee()
            {
                EmployeeId = 1,
                EmployeeName = "Rahul"
            };
            Employee employee2 = new Employee()
            {
                EmployeeId = 2,
                EmployeeName = "Rohan"
            };
            list.Add(employee1);
            list.Add(employee2);

            EmployeeViewModel employeeVm1 = new EmployeeViewModel()
            {
                EmployeeId = 1,
                EmployeeName = "Rahul"
            };
            EmployeeViewModel employeeVm2 = new EmployeeViewModel()
            {
                EmployeeId = 2,
                EmployeeName = "Rohan"
            };

            List<EmployeeViewModel> employeeVmList = new List<EmployeeViewModel>();
            employeeVmList.Add(employeeVm1);
            employeeVmList.Add(employeeVm2);

            _srsDbContextMock.Setup(employee => employee.Employees.ToList()).Returns(list);
            _mapperMock.Setup(mapper => mapper.Map<List<EmployeeViewModel>>(list)).Returns(employeeVmList);

            //Act
            var employeeList =await _employeeRepository.GetAll();

            //Assert
            Assert.Equal(list.Count, employeeList.Count);
        }
    }
}