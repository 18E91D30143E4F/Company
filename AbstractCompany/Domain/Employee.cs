using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Employee
    {
        public int Id { get; set; }

        [Display(Name = "Имя сотрудника")]
        public string Name { get; set; }

        [Display(Name = "Фамилия сотрудника")]
        public string Surname { get; set; }
        public int PositionId { get; set; }

        [Display(Name = "Должность")]
        public string PositionName { get; set; }

        [Display(Name = "День рождения")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy.MM.dd}")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Зарплата")]
        public decimal Salary { get; set; }
    }
}