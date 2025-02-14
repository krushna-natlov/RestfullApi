﻿namespace RestfullApi.Models.Entities
{
    public class AddEmployeeDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
    }
}
