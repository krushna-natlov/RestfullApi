using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfullApi.Data;
using RestfullApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfullApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentsController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        // GET: api/departments
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _dbContext.Departments.ToListAsync();
            return Ok(departments);
        }

        // GET: api/departments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(Guid id)
        {
            var department = await _dbContext.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound(new { message = "Department not found" });
            }
            return Ok(department);
        }

        // POST: api/departments
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            if (department == null || string.IsNullOrEmpty(department.Name))
            {
                return BadRequest(new { message = "Invalid department data" });
            }

            department.Id = Guid.NewGuid(); // Generate new GUID
            await _dbContext.Departments.AddAsync(department);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, department);
        }

        // PUT: api/departments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] Department updatedDepartment)
        {
            var department = await _dbContext.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound(new { message = "Department not found" });
            }

            department.Name = updatedDepartment.Name;

            await _dbContext.SaveChangesAsync();
            return Ok(department);
        }

        // DELETE: api/departments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            var department = await _dbContext.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound(new { message = "Department not found" });
            }

            _dbContext.Departments.Remove(department);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
