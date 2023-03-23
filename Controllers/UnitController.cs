using Microsoft.AspNetCore.Mvc;
using RecipeFinderAPI.Models;
using RecipeFinderAPI.Services;

namespace RecipeFinderAPI.Controllers
{
    [Route("api/unit")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService) 
        {
            _unitService = unitService;
        }

        #region GET actions
        [HttpGet]
        public ActionResult<IEnumerable<IngridientDto>> Get()
        {
            var result = _unitService.GetAll();
            return Ok(result);
        }
        [HttpGet("{unitId}")]
        public ActionResult<IEnumerable<IngridientDto>> Get([FromRoute] int unitId)
        {
            var result = _unitService.GetById(unitId);
            return Ok(result);
        }
        #endregion

        #region PUT actions
        [HttpPost]
        public ActionResult Create([FromQuery] string name)
        {
            int id = _unitService.CreateUnit(name);
            return Created($"api/unit/{id}", null);
        }
        #endregion

        [HttpDelete("{unitId}")]
        public ActionResult Delete(int unitId) 
        { 
            _unitService.RemoveById(unitId);
            return NoContent();
        }
    }
}
