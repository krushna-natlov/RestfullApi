using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestfullApi.Models.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public string? Phone { get; set; }

        [Required]
        public Guid DepartmentId { get; set; } // Foreign Key

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; } // Navigation Property
    }
}
