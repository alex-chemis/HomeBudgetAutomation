using HomeBudgetAutomation.Dtos.Balance;
using HomeBudgetAutomation.Dtos.Balance;
using HomeBudgetAutomation.ServiceResponder;
using HomeBudgetAutomation.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudgetAutomation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalancesController : ControllerBase
    {
        private readonly IBalancesService _service;
        private readonly ILogger<BalancesController> _logger;

        public BalancesController(IBalancesService service, ILogger<BalancesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BalanceDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<BalanceDto>> GetAll()
        {
            var balances = _service.GetAll();

            if (balances.Message == ServiceMessageType.InternalServerError)
            {
                ModelState.AddModelError("", $"Something went wrong in the service layer when getting all balances");
                return StatusCode(500, ModelState);
            }

            return Ok(balances.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BalanceDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BalanceDto> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest(id);
            }

            var balance = _service.GetById(id);

            if (balance.Message == ServiceMessageType.NotFound)
            {
                return NotFound();
            }

            return Ok(balance.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BalanceDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BalanceDto> Create([FromBody] FormBalanceDto balance)
        {
            if (balance == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newBalance = _service.Form(balance);

            if (newBalance.ErrorMessages is not null)
            {
                foreach (var error in newBalance.ErrorMessages)
                {
                    _logger.LogError(error);
                }
            }

            if (newBalance.Message == ServiceMessageType.InternalServerError)
            {
                ModelState.AddModelError("", $"Something went wrong in the service layer when adding balance {balance}");
                return StatusCode(500, ModelState);
            }

            return Ok(newBalance.Data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BalanceDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BalanceDto> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(id);
            }

            var balance = _service.SoftDelete(id);

            if (balance.ErrorMessages is not null)
            {
                foreach (var error in balance.ErrorMessages)
                {
                    _logger.LogError(error);
                }
            }

            if (balance.Message == ServiceMessageType.NotFound)
            {
                return NotFound();
            }

            if (balance.Message == ServiceMessageType.InternalServerError)
            {
                ModelState.AddModelError("", $"Something went wrong in the service layer when getting all balances");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }
}
