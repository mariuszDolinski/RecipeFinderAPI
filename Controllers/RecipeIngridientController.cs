using Microsoft.AspNetCore.Mvc;
using RecipeFinderAPI.Models;
using RecipeFinderAPI.Services;

namespace RecipeFinderAPI.Controllers
{
    [Route("api/recipe/{recipeId}/ingridient")]
    [ApiController]
    public class RecipeIngridientController : ControllerBase
    {
        private IRecipeIngridientService _recipeIngridientService;
        public RecipeIngridientController(IRecipeIngridientService recipeIngridientService)
        {
            _recipeIngridientService = recipeIngridientService;
        }

        [HttpPost]
        public ActionResult Create(int recipeId, [FromBody] CreateRecipeIngridientDto dto)
        {
            int id = _recipeIngridientService.Create(recipeId, dto);

            return Created($"api/recipe/{recipeId}/ingridient/{id}", null);
        }
    }
}
