using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Payment.WebApi.Controllers;

public class MainController : Controller
{

    [HttpGet("/")]
    public IActionResult LifeTest()
    {
        return NoContent();
    }

}
