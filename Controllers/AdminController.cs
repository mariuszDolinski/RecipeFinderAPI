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

        #region GET actions
        [HttpGet("users")]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var users = _adminService.GetAllUsers();
            return Ok(users);
        }
        #endregion

        #region PATCH actions
        [HttpPatch("user/{userId}")]
        public ActionResult ChangeRole([FromRoute] int userId, [FromQuery] string roleName)
        {
            _adminService.ChangeRole(userId, roleName);
            return Ok();
        }

        [HttpPatch("recipe/{recipeId}")]
        public ActionResult UpdateRecipe(int recipeId, [FromQuery] int authorId)
        {
            _adminService.UpdateRecipeAuthor(recipeId, authorId);
            return Ok(); 
        }

        #endregion
    }
}
