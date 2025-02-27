using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfullApi.Data;
using RestfullApi.Models.DTOs;
using RestfullApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfullApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //private readonly ApplicationDbConext  _dbContext;

        //public EmployeesController(ApplicationDbConext dbContext)
        //{
        //    this._dbContext = dbContext;
        //}

        private readonly ApplicationDbContext _dbContext;

        public EmployeesController(ApplicationDbContext context)
        {
            this._dbContext = context;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var allEmployees = await _dbContext.Employees
                .Include(e => e.Department) // Include department info
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    e.Email,
                    e.Phone,
                    Department = e.Department != null ? new
                    {
                        e.Department.Id,
                        e.Department.Name
                    } : null
                })
                .ToListAsync();

            return Ok(allEmployees);
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _dbContext.Employees
                .Include(e => e.Department)
                .Where(e => e.Id == id)
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    e.Email,
                    e.Phone,
                    Department = e.Department != null ? new
                    {
                        e.Department.Id,
                        e.Department.Name
                    } : null
                })
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound(new { message = "Employee not found" });
            }
            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeDto addEmployeeDto)
        {
            if (addEmployeeDto == null)
            {
                return BadRequest(new { message = "Invalid employee data" });
            }

            var employeeEntity = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                DepartmentId = addEmployeeDto.DepartmentId // Assign department
            };

            await _dbContext.Employees.AddAsync(employeeEntity);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeEntity.Id }, employeeEntity);
        }

        // PUT: api/employees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] AddEmployeeDto updateEmployeeDto)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound(new { message = "Employee not found" });
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.DepartmentId = updateEmployeeDto.DepartmentId; // Update department

            await _dbContext.SaveChangesAsync();

            return Ok(employee);
        }

        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound(new { message = "Employee not found" });
            }

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
