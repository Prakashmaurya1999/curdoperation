using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using curdoperation.Data;
using curdoperation.model;

namespace curdoperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 GET ALL
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var data = await _context.Employees.ToListAsync();
            return Ok(data);
        }

        // 🔹 GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var data = await _context.Employees.FindAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        // 🔹 CREATE
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee emp)
        {
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();
            return Ok(emp);
        }

        // 🔹 UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee emp)
        {
            if (id != emp.Id) return BadRequest();

            _context.Entry(emp).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(emp);
        }

        // 🔹 DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
