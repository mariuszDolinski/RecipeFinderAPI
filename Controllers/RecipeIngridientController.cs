using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeFinderAPI.Models;
using RecipeFinderAPI.Services;
using System.Security.Claims;

namespace RecipeFinderAPI.Controllers
{
    [Route("api/recipe/{recipeId}/ingridient")]
    [ApiController]
    [Authorize]
    public class RecipeIngridientController : ControllerBase
    {
        private IRecipeIngridientService _recipeIngridientService;
        public RecipeIngridientController(IRecipeIngridientService recipeIngridientService)
        {
            _recipeIngridientService = recipeIngridientService;
        }

        #region POST methods
        [HttpPost]
        [Authorize(Roles = "Admin,ConfirmUser")]
        public ActionResult Create([FromRoute]int recipeId, [FromBody] CreateRecipeIngridientDto dto)
        {
            int id = _recipeIngridientService.Create(recipeId, dto);

            return Created($"api/recipe/{recipeId}/ingridient/{id}", null);
        }
        #endregion

        #region GET methods
        [HttpGet]
        public ActionResult<IEnumerable<RecipeIngridientDto>> Get([FromRoute]int recipeId) 
        { 
            var result = _recipeIngridientService.GetAll(recipeId);
            return Ok(result);
        }
        [HttpGet("{ingridientId}")]
        public ActionResult<RecipeIngridientDto> Get([FromRoute]int recipeId, [FromRoute]int ingridientId)
        {
            var result = _recipeIngridientService.GetById(recipeId, ingridientId);
            return Ok(result);
        }
        #endregion

        #region PUT methods
        [HttpPut("{ingridientId}")]
        public ActionResult Update([FromRoute]int recipeId, [FromBody]UpdateRecipeIngridientDto dto, [FromRoute]int ingridientId)
        {
            _recipeIngridientService.Update(recipeId, dto, ingridientId);
            return Ok();
        }
        #endregion

        #region DELETE methods
        [HttpDelete]
        public ActionResult Delete([FromRoute]int recipeId) 
        {
            _recipeIngridientService.Delete(recipeId);
            return NoContent();
        }
        [HttpDelete("{ingridientId}")]
        public ActionResult Delete([FromRoute]int recipeId, [FromRoute]int ingridientId)
        {
            _recipeIngridientService.DeleteById(recipeId, ingridientId);
            return NoContent();
        }
        #endregion
    }
}
