using BusinessLogic.Data;
using BusinessLogic.DTO;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationsController : ControllerBase
    {
        private  ICalculetionsService _calculetionsService;
        public CalculationsController(ICalculetionsService calculetionsService)
        {
            _calculetionsService = calculetionsService;
        }

        [HttpPost("CalculetinResult")]
        public ActionResult<CalculationResultDTO> CalculetinResult([FromBody] CalculetinsDTO obj)
        {
            var result = _calculetionsService.CalculetinResult(obj);

            if (result.Result.StartsWith("שגיאה"))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("GetOperations")]
        public ActionResult<List<string>> GetOperations()
        {
            var operations = _calculetionsService.GetOperations();
            return Ok(operations);
        }


    }
}
