using Domain;
using Entities;

namespace Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeEntity ToEntity(this Employee employee)
        {
            return new EmployeeEntity
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                Birthday = employee.Birthday,
                Salary = employee.Salary,
                PositionId = employee.PositionId,
                PositionName = employee.PositionName
            };
        }

        public static Employee ToDomain(this EmployeeEntity employee)
        {
            return new Employee
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                Birthday = employee.Birthday,
                Salary = employee.Salary,
                PositionName = employee.PositionName,
                PositionId = employee.PositionId
            };
        }
    }
}