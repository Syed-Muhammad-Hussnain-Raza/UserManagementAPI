using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>();

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving users", detail = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                return user == null ? NotFound(new { message = "User not found" }) : Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving user", detail = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User newUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                newUser.Id = users.Count + 1;
                users.Add(newUser);
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating user", detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = users.FirstOrDefault(u => u.Id == id);
                if (user == null) return NotFound(new { message = "User not found" });

                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating user", detail = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user == null) return NotFound(new { message = "User not found" });

                users.Remove(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting user", detail = ex.Message });
            }
        }
    }
}
