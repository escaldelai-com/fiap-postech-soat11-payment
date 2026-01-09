using Microsoft.AspNetCore.Mvc;
using Restaurant.Payment.Application.DTO;
using Restaurant.Payment.Application.Interfaces.Facade;

namespace Restaurant.Payment.WebApi.Controllers;

[Route("[controller]")]
public class OrderController(
    IOrderFacade facade) : Controller
{

    [HttpPost("pay")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Pay([FromBody]OrderDto data)
    {
        return NoContent();
    }

    [HttpPost("pay/confirm")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> PayConfirm([FromBody]OrderDto data)
    {
        await facade.Confirm(data);

        return NoContent();
    }

}
