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
    private readonly ILogger<CardsController> _logger;
    private readonly ICardService _cardService;
    private readonly IMapper _mapper;
    public CardsController(ILogger<CardsController> logger, ICardService cardService, IMapper mapper)
    {
        _logger = logger;
        _cardService = cardService;
        _mapper = mapper;
    }

    [HttpGet(Name = "Get All Cards")]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<CardDTO>))]
    public async Task<ActionResult> GetCards(int offset = 0, int limit = 10, string? sortBy = "createdon",
        string? sortOrder = "asc")
    {
        try
        {
            var model = new GetCardsDTO(offset, limit, sortBy, sortOrder);
            var response = await _cardService.GetAllCards(model);
            return response.statusCode == HttpStatusCode.OK
                ? StatusCode((int)response.statusCode, response.data)
                : StatusCode((int)response.statusCode, new ErrorResponse(response.message));
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Server Error"));
        }
    }

    [HttpGet("{id}", Name = "Get Card")]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardDTO))]
    public async Task<ActionResult> GetCard(Guid id)
    {
        try
        {
            var response = await _cardService.GetCardById(id);
            return response.statusCode == HttpStatusCode.OK
                ? StatusCode((int)response.statusCode, response.data)
                : StatusCode((int)response.statusCode, new ErrorResponse(response.message));
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Server Error"));
        }
    }

    [HttpPost(Name = "Create Card")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SerializableError))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CardDTO))]
    public async Task<ActionResult> CreateCards([FromBody] CardBindingModel model)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode((int) HttpStatusCode.BadRequest,ModelState);
        }
        
        try
        {
            var cardModel = _mapper.Map<Card>(model);
            var response = await _cardService.CreateCard(cardModel);
            return response.statusCode == HttpStatusCode.Created
                ? StatusCode((int)response.statusCode, response.data)
                : StatusCode((int)response.statusCode, new ErrorResponse(response.message));
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Server Error"));
        }
    }

    [HttpPatch("{id}", Name= "Update Card")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SerializableError))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardDTO))]
    public async Task<ActionResult> UpdateCard(Guid id, [FromBody] EditCardBindingModel model)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode((int) HttpStatusCode.BadRequest, ModelState);
        }
        
        try
        {
            var card = _mapper.Map<Card>(model);
            card.Id = id;
            var response = await _cardService.UpdateCard(card);
            return response.statusCode == HttpStatusCode.OK
                ? StatusCode((int)response.statusCode, response.data)
                : StatusCode((int)response.statusCode, new ErrorResponse(response.message));
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Server Error"));
        }
    }

    [HttpDelete("{id}", Name = "Delete Card")]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardDTO))]
    public async Task<ActionResult> DeleteCard(Guid id)
    {
        try
        {
            var response = await _cardService.DeleteCard(id);
            return response.statusCode == HttpStatusCode.OK
                ? StatusCode((int)response.statusCode, response.data)
                : StatusCode((int)response.statusCode, new ErrorResponse(response.message));
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Server Error"));
        }
    }
    
    [HttpGet("search", Name = "Search Card")]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CardDTO>))]
    public async Task<ActionResult> SearchCard(string? name = null, string? color = null, CardStatus? status = null,
        DateTime? createdDate = null, int offset = 0, int limit = 10, string sortBy = "name",
        string sortOrder = "asc")
    {
        try
        {
            var model = new SearchDTO(name, color, status, createdDate, offset, limit, sortBy, sortOrder);
            var response = await _cardService.SearchCard(model);
            return response.statusCode == HttpStatusCode.OK
                ? StatusCode((int)response.statusCode, response.data)
                : StatusCode((int)response.statusCode, new ErrorResponse(response.message));
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse("Server Error"));
        }
    }
    
}