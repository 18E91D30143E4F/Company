using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Position
    {
        public int Id { get; set; }

        [Display(Name = "Должность")]
        public string Name { get; set; }
    }
}