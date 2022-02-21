using Data.Repositories;
using Data.Repositories.Abstract;
using Domain;
using Mappers;
using Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _EmployeeRepository;

        #region Constructors

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _EmployeeRepository = employeeRepository;
        }

        public EmployeeService()
        {
            _EmployeeRepository = new EmployeeRepository();
        }

        #endregion

        public bool Add(Employee entity) => _EmployeeRepository.Add(entity.ToEntity());

        public bool Update(Employee entity) => _EmployeeRepository.Update(entity.ToEntity());

        public bool Delete(int id) => _EmployeeRepository.Delete(id);
        public Employee Get(int id) => _EmployeeRepository.Get(id).ToDomain();

        public IEnumerable<Employee> GetAll() =>
            _EmployeeRepository.GetAll()
                .Select(employee => employee.ToDomain());
    }
}