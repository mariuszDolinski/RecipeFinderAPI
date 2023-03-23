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

        #region POST methods
        [HttpPost]
        public ActionResult Create(int recipeId, [FromBody] CreateRecipeIngridientDto dto)
        {
            int id = _recipeIngridientService.Create(recipeId, dto);

            return Created($"api/recipe/{recipeId}/ingridient/{id}", null);
        }
        #endregion

        #region GET methods
        [HttpGet]
        public ActionResult<IEnumerable<RecipeIngridientDto>> Get(int recipeId) 
        { 
            var result = _recipeIngridientService.GetAll(recipeId);
            return Ok(result);
        }
        [HttpGet("{ingridientId}")]
        public ActionResult<RecipeIngridientDto> Get(int recipeId, [FromRoute]int ingridientId)
        {
            var result = _recipeIngridientService.GetById(recipeId, ingridientId);
            return Ok(result);
        }
        #endregion

        #region PUT methods
        [HttpPut("{ingridientId}")]
        public ActionResult Update(int recipeId, [FromBody]CreateRecipeIngridientDto dto, [FromRoute]int ingridientId)
        {
            _recipeIngridientService.Update(recipeId, dto, ingridientId);
            return Ok();
        }
        #endregion
    }
}
