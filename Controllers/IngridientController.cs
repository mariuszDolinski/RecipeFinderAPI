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

        [HttpGet]
        public ActionResult<IEnumerable<IngridientDto>> Get() 
        {
            var result = _ingridientService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public ActionResult CreateIngridient([FromQuery]string name)
        {
            int id = _ingridientService.CreateIngridient(name);
            return Created($"api/ingridient/{id}", null);
        }
    }
}
