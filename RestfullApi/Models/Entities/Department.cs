using System.ComponentModel.DataAnnotations;

namespace RestfullApi.Models.Entities
{
    public class Department
    {
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }

     
    }
}
