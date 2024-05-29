using EmployeeDetails.Models;
using EmployeeDetails.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ConnectionStringProvider _context;

        public ContactsController(ConnectionStringProvider context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetEmployeeDetails")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employeeDetailsResponses = await _context.Contacts.ToListAsync();
                return Ok(employeeDetailsResponses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        // Add employee
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeDetailsRequest employeeRequest)
        {
            try
            {
                _context.Contacts.Add(employeeRequest);
                await _context.SaveChangesAsync();
                return Ok(employeeRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        // Update employee
        [HttpPost]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDetailsRequest employeeRequest)
        {
            try
            {
                var existingEmployee = await _context.Contacts.FindAsync(employeeRequest.id);
                if (existingEmployee == null)
                {
                    return NotFound(new { message = "Employee not found." });
                }

                existingEmployee.firstName = employeeRequest.firstName;
                existingEmployee.lastName = employeeRequest.lastName;
                existingEmployee.email = employeeRequest.email;
                existingEmployee.address = employeeRequest.address;
                existingEmployee.city = employeeRequest.city;
                existingEmployee.state = employeeRequest.state;
                existingEmployee.country = employeeRequest.country;
                existingEmployee.postalCode = employeeRequest.postalCode;
                existingEmployee.phoneNumber = employeeRequest.phoneNumber;
                existingEmployee.longitude = employeeRequest.longitude;
                existingEmployee.latitude = employeeRequest.latitude;

                _context.Contacts.Update(existingEmployee);
                await _context.SaveChangesAsync();

                return Ok(existingEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        // Delete employee
        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _context.Contacts.FindAsync(id);
                if (employee == null)
                {
                    return NotFound(new { message = "Employee not found." });
                }

                _context.Contacts.Remove(employee);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Employee deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
