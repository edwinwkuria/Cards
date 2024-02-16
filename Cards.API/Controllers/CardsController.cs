using AutoMapper;
using Cards.BindingModels.CardsController;
using Cards.Infrastructure.Entities;
using Cards.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class CardsController : ControllerBase
{
    private readonly ICardService _cardService;
    private readonly IMapper _mapper;
    public CardsController(ICardService cardService, IMapper mapper)
    {
        _cardService = cardService;
        _mapper = mapper;
    }

    [HttpGet()]
    public ActionResult<IEnumerable<string>> GetCards()
    {
        var cards = _cardService.GetAllCards();
        return Ok(cards);
    }

    [HttpGet("{id}")]
    public ActionResult<string> GetCard(Guid id)
    {
        var card = _cardService.GetCardById(id);
        return Ok(card);
    }

    [HttpPost()]
    public ActionResult CreateCards([FromBody] CardBindingModel model)
    {
        var card = _mapper.Map<Card>(model);

        var response = _cardService.CreateCard(card);
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public ActionResult UpdateCard(Guid id, [FromBody] CardBindingModel model)
    {
        var card = _mapper.Map<Card>(model);

        card.Id = id;
        var response = _cardService.UpdateCard(card);
        
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCard(Guid id)
    {
        var response = _cardService.DeleteCard(id);
        
        return Ok(response);
    }
    
}