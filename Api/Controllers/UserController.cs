using Api.Models;
using Api.Repositories;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateUser(User user)
        {
            var result = await _userService.CreateAsync(user);
            if (result != null)
                return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
            else
                return BadRequest();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var result = await _userService.UpdateAsync(user);
            if (result != null)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteAsync(id: id);
            if (result)
                return NoContent();
            else
                return NotFound();
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userService.GetAllAsync();
            if (!users.Any())
                return NotFound();
            else
                return Ok(users);
        }

        [HttpGet("getbyid/{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user is null)
                return NotFound();
            else
                return Ok(user);
        }
    }
}
