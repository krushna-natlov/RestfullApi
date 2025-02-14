using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfullApi.Data;
using RestfullApi.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RestfullApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployesController : ControllerBase
    {
        private readonly ApplicationDbConext _dbContext;

        public EmployesController(ApplicationDbConext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = _dbContext.Employees.ToList();
            return Ok(allEmployees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] AddEmployeeDto addEmployeeDto)
        {
            if (addEmployeeDto == null)
            {
                return BadRequest("Invalid employee data");
            }

            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone
            };

            _dbContext.Employees.Add(employeeEntity);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeEntity.Id }, employeeEntity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] AddEmployeeDto updateEmployeeDto)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;

            _dbContext.SaveChanges();

            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}