using EmployeeManagment.Models;
using EmployeeManagment.Reposetories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagment.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmplyeeRepositories _employeeRepositories;

        public EmployeeController(IEmplyeeRepositories employeeRepositories)
        {
            _employeeRepositories = employeeRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployeesAsync()
        {
            var allEmployees = await _employeeRepositories.GetAllAsync();
            return Ok(allEmployees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _employeeRepositories.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }



        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            await _employeeRepositories.AddEmployeAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }
        // The error is that 'IActionResult' is not a generic type and cannot be used with type arguments.
        // Change 'IActionResult<Employee>' to just 'ActionResult<Employee>' or 'IActionResult'.



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeId(int id)
        {
            await _employeeRepositories.DeleteEmployeeAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployeeAsync(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            await _employeeRepositories.UpdateEmployeeAsync(employee);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }
    }
}
