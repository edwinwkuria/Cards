using Cards.BindingModels.CardsController;
using Cards.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CardsController : ControllerBase
{
    private readonly ICardService _cardService;

    public CardsController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet()]
    public ActionResult<IEnumerable<string>> GetCards()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public ActionResult<string> GetCard(int id)
    {
        return Ok();
    }

    [HttpPost()]
    public ActionResult CreateCards([FromBody] CardBindingModel card)
    {
        return Ok();
    }

    [HttpPatch("{id}")]
    public ActionResult UpdateCard(int id, [FromBody] CardBindingModel card)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCard(int id)
    {
        return Ok();
    }
    
}