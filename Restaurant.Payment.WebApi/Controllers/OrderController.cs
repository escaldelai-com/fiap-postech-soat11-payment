using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.Facade;

namespace Restaurant.Payment.WebApi.Controllers;

[Route("[controller]")]
public class OrderController(
    IOrderFacade facade) : Controller
{

    [HttpPost("pay")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PixInfoDto))]
    public async Task<IActionResult> Pay([FromBody] OrderDto data)
    {
        var result = await facade.SaveOrderToPayment(data);

        return Ok(result);
    }

    [HttpPost("pay/send")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PixInfoDto))]
    public async Task<IActionResult> SendPayment([FromBody] OrderDto data)
    {
        var result = await facade.SendPayment(data?.Id!);

        return Ok(result);
    }

    [HttpPost("pay/confirm")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PayConfirm([FromBody] JObject data)
    {
        await facade.Confirm(data);

        return NoContent();
    }

}
