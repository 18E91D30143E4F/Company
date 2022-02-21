using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace AbstractCompany.Web.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public IEnumerable<Position> Positions { get; set; }
    }
}