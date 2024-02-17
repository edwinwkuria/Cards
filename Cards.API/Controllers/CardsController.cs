using System.Net;
using AutoMapper;
using Cards.API.Responses;
using Cards.BindingModels.CardsController;
using Cards.Infrastructure.DataTypes;
using Cards.Infrastructure.Entities;
using Cards.Services.DTOModels;
using Cards.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cards.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
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

    [HttpGet(Name = "Get All Cards")]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<CardDTO>))]
    public ActionResult GetCards()
    {
        try
        {
            var response = _cardService.GetAllCards();
            return response.statusCode == HttpStatusCode.OK
                ? StatusCode((int)response.statusCode, response.data)
                : StatusCode((int)response.statusCode, new ErrorResponse(response.message));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Server Error"));
        }
    }

    [HttpGet("{id}", Name = "Get Card")]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardDTO))]
    public ActionResult<string> GetCard(Guid id)
    {
        try
        {
            var response = _cardService.GetCardById(id);
            return response.statusCode == HttpStatusCode.OK
                ? StatusCode((int)response.statusCode, response.data)
                : StatusCode((int)response.statusCode, new ErrorResponse(response.message));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Server Error"));
        }
    }

    [HttpPost(Name = "Create Card")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SerializableError))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CardDTO))]
    public ActionResult CreateCards([FromBody] CardBindingModel model)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode((int) HttpStatusCode.BadRequest,ModelState);
        }
        
        try
        {
            var cardModel = _mapper.Map<Card>(model);
            var response = _cardService.CreateCard(cardModel);
            return response.statusCode == HttpStatusCode.Created
                ? StatusCode((int)response.statusCode, response.data)
                : StatusCode((int)response.statusCode, new ErrorResponse(response.message));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Server Error"));
        }
    }

    [HttpPatch("{id}", Name= "Update Card")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SerializableError))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardDTO))]
    public ActionResult UpdateCard(Guid id, [FromBody] EditCardBindingModel model)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode((int) HttpStatusCode.BadRequest, ModelState);
        }
        
        try
        {
            var card = _mapper.Map<Card>(model);
            card.Id = id;
            var response = _cardService.UpdateCard(card);
            return response.statusCode == HttpStatusCode.OK
                ? StatusCode((int)response.statusCode, response.data)
                : StatusCode((int)response.statusCode, new ErrorResponse(response.message));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Server Error"));
        }
    }

    [HttpDelete("{id}")]
    [HttpPost(Name = "Delete Card")]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardDTO))]
    public ActionResult DeleteCard(Guid id)
    {
        try
        {
            var response = _cardService.DeleteCard(id);
            return response.statusCode == HttpStatusCode.OK
                ? StatusCode((int)response.statusCode, response.data)
                : StatusCode((int)response.statusCode, new ErrorResponse(response.message));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Server Error"));
        }
    }
    
    [HttpGet("search", Name = "Search Card")]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CardDTO>))]
    public ActionResult SearchCard(string? name = null, string? color = null, CardStatus? status = null,
        DateTime? createdDate = null, int page = 1, int size = 10, int offset = 0, int limit = 10, string sortBy = "name",
        string sortOrder = "asc")
    {
        try
        {
            var model = new SearchDTO(name, color, status, createdDate, page, size, offset, limit, sortBy, sortOrder);
            var response = _cardService.SearchCard(model);
            return response.statusCode == HttpStatusCode.OK
                ? StatusCode((int)response.statusCode, response.data)
                : StatusCode((int)response.statusCode, new ErrorResponse(response.message));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Server Error"));
        }
    }
    
}