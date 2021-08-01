using System.ComponentModel.DataAnnotations;

namespace MVC_CRUD.Models
{
    public class Employee
    {
        public int EmpId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        [Required]
        public string Mobile { get; set; }
    }
}
