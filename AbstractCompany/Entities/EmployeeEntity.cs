using Entities.Abstract.Base;
using System;

namespace Entities
{
    public class EmployeeEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public DateTime Birthday { get; set; }
        public decimal Salary { get; set; }
    }
}