using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeFinderAPI.Models;
using RecipeFinderAPI.Services;

namespace RecipeFinderAPI.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("users")]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var users = _adminService.GetAllUsers();
            return Ok(users);
        }

        [HttpPatch("user/{userId}")]
        public ActionResult ChangeRole([FromRoute] int userId, [FromQuery] string role)
        {
            _adminService.ChangeRole(userId, role);
            return Ok();
        }
    }
}
