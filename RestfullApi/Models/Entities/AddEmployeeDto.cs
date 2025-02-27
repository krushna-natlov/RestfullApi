namespace RestfullApi.Models.DTOs
{
    public class AddEmployeeDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public Guid DepartmentId { get; set; }
        public string? DepartmentName { get; set; } // To display department info
    }
}
