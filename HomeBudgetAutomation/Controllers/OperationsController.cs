using HomeBudgetAutomation.Dtos;
using HomeBudgetAutomation.Dtos.Operation;
using HomeBudgetAutomation.ServiceResponder;
using HomeBudgetAutomation.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudgetAutomation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationsService _service;
        private readonly ILogger<OperationsController> _logger;

        public OperationsController(IOperationsService service, ILogger<OperationsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OperationDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<OperationDto>> GetAll()
        {
            var operations = _service.GetAll();

            if (operations.Message == ServiceMessageType.InternalServerError)
            {
                ModelState.AddModelError("", $"Something went wrong in the service layer when getting all operations");
                return StatusCode(500, ModelState);
            }

            return Ok(operations.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OperationDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OperationDto> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest(id);
            }

            var operation = _service.GetById(id);

            if (operation.Message == ServiceMessageType.NotFound)
            {
                return NotFound();
            }

            return Ok(operation.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OperationDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OperationDto> Create([FromBody] CreateOperationDto operation)
        {
            if (operation == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newOperation = _service.Add(operation);

            if (newOperation.ErrorMessages is not null)
            {
                foreach (var error in newOperation.ErrorMessages)
                {
                    _logger.LogError(error);
                }
            }

            if (newOperation.Message == ServiceMessageType.InternalServerError)
            {
                ModelState.AddModelError("", $"Something went wrong in the service layer when adding operation {operation}");
                return StatusCode(500, ModelState);
            }


            return Ok(newOperation.Data);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OperationDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OperationDto> Update(int id, [FromBody] UpdateOperationDto operation)
        {
            if (operation == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newOperation = _service.Update(id, operation);

            if (newOperation.ErrorMessages is not null)
            {
                foreach (var error in newOperation.ErrorMessages)
                {
                    _logger.LogError(error);
                }
            }

            if (newOperation.Message == ServiceMessageType.NotFound)
            {
                return NotFound();
            }

            if (newOperation.Message == ServiceMessageType.InternalServerError)
            {
                ModelState.AddModelError("", $"Something went wrong in the service layer when updating operation {operation}");
                return StatusCode(500, ModelState);
            }

            return Ok(newOperation.Data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OperationDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OperationDto> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(id);
            }

            var operation = _service.DeleteById(id);

            if (operation.ErrorMessages is not null)
            {
                foreach (var error in operation.ErrorMessages)
                {
                    _logger.LogError(error);
                }
            }

            if (operation.Message == ServiceMessageType.NotFound)
            {
                return NotFound();
            }

            if (operation.Message == ServiceMessageType.InternalServerError)
            {
                ModelState.AddModelError("", $"Something went wrong in the service layer when getting all operations");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpPut("stats")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OperationDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<OperationDto>> GetByDate([FromBody] FunctionParamsDto functionParams)
        {
            var operations = _service.GetByDate(functionParams);

            if (operations.ErrorMessages is not null)
            {
                foreach (var error in operations.ErrorMessages)
                {
                    _logger.LogError(error);
                }
            }

            if (operations.Message == ServiceMessageType.InternalServerError)
            {
                ModelState.AddModelError("", $"Something went wrong in the service layer when getting all operations");
                return StatusCode(500, ModelState);
            }

            return Ok(operations.Data);
        }
    }
}
