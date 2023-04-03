using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeFinderAPI.Models;
using RecipeFinderAPI.Services;
using System.Security.Claims;

namespace RecipeFinderAPI.Controllers
{
    [Route("api/recipe")]
    [ApiController]
    [Authorize]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        #region GET actions
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<RecipeDto>> Get()
        {
            var result = _recipeService.GetAll();
            return Ok(result);
        }

        [HttpGet("{recipeId}")]
        public ActionResult<RecipeDto> Get([FromRoute]int recipeId)
        {
            var result = _recipeService.GetById(recipeId);
            return Ok(result);
        }
        #endregion

        #region POST actions
        [HttpPost]
        [Authorize(Roles = "Admin,ConfirmUser")]
        [Authorize(Policy = "IsAdult")]
        public ActionResult Create([FromBody]CreateRecipeDto dto)
        {
            int recipeId = _recipeService.CreateRecipe(dto);
            return Created($"api/recipe/{recipeId}", null);
        }

        [HttpPost("searchResult")]
        public ActionResult<IEnumerable<RecipeDto>> Find([FromBody] FindRecipesByIngridientsDto dto, [FromQuery]int mode)
        {
            var result = _recipeService.GetByIngridient(dto, mode);

            return Created($"api/recipe/searchResult",result);
        }
        #endregion

        #region PUT actions
        [HttpPut("{recipeId}")]
        public ActionResult Update([FromBody]UpdateRecipeDto dto, [FromRoute]int recipeId) 
        {
            _recipeService.UpdateRecipe(dto, recipeId);
            return Ok();
        }
        #endregion

        #region DELETE actions
        [HttpDelete("{recipeId}")]
        public ActionResult Delete(int recipeId)
        {
            _recipeService.RemoveById(recipeId);
            return NoContent();
        }
        #endregion
    }
}
