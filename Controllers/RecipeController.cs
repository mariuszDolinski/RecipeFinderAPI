using Microsoft.AspNetCore.Mvc;
using RecipeFinderAPI.Models;
using RecipeFinderAPI.Services;

namespace RecipeFinderAPI.Controllers
{
    [Route("api/recipe")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        #region GET actions
        [HttpGet]
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
        public ActionResult Create([FromBody]CreateRecipeDto dto)
        {
            int id = _recipeService.CreateRecipe(dto);
            return Created($"api/recipe/{id}", null);
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
    }
}
