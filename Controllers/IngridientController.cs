using Microsoft.AspNetCore.Mvc;
using RecipeFinderAPI.Models;
using RecipeFinderAPI.Services;

namespace RecipeFinderAPI.Controllers
{
    [Route("api/ingridient")]
    [ApiController]
    public class IngridientController : ControllerBase    
    {
        private readonly IIngridientService _ingridientService;

        public IngridientController(IIngridientService ingridientService) 
        {
            _ingridientService = ingridientService;
        }

        #region GET actions
        [HttpGet]
        public ActionResult<IEnumerable<IngridientDto>> Get() 
        {
            var result = _ingridientService.GetAll();
            return Ok(result);
        }
        [HttpGet("{ingridientId}")]
        public ActionResult<IEnumerable<IngridientDto>> Get([FromRoute]int ingridientId)
        {
            var result = _ingridientService.GetById(ingridientId);
            return Ok(result);
        }
        #endregion

        #region POST actions
        [HttpPost]
        public ActionResult Create([FromQuery]string name)
        {
            int id = _ingridientService.CreateIngridient(name);
            return Created($"api/ingridient/{id}", null);
        }
        #endregion

        #region DELETE actions
        [HttpDelete("{ingridientId}")]
        public ActionResult Delete(int ingridientId)
        {
            _ingridientService.RemoveById(ingridientId);
            return NoContent();
        }
        #endregion
    }
}
