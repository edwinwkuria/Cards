using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using Cards.API.ConfigModels;
using Cards.Infrastructure.DataTypes;
using Cards.Infrastructure.Entities;
using Cards.Infrastructure.Repository.Abstract;
using Cards.Services.DTOModels;
using Cards.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Cards.Services;

public class CardService : ICardService
{
    private readonly IUnitOfWork _iuow;
    private readonly IRepository<Card> _repository;
    private readonly IMapper _mapper;
    private readonly PermissionsConfig _config;
    private readonly CurrentUserDTO user;
    public CardService(IUnitOfWork iuow, IMapper mapper, IOptions<PermissionsConfig> config,
        IAuthService authService)
    {
        _iuow = iuow;
        _repository = _iuow.Repository<Card>();
        _mapper = mapper;
        _config = config.Value;
        user = authService.GetCurrentUser();
    }

    public async Task<(HttpStatusCode statusCode, string message, List<CardDTO> data)> GetAllCards(GetCardsDTO model)
    {
        var canViewAllCards = CanViewAllCards(MethodPermissionsConfig.DeleteCard);
        
        var cards = _repository.Get(
                filter: c => (canViewAllCards || c.CreatedBy.Equals(user.Id)), 
                orderBy: GetCardOrderBy(model.SortBy, model.SortOrder))
            .Skip(model.Offset).Take(model.Limit).Select(x => _mapper.Map<CardDTO>(x)).ToList();
        
        return (HttpStatusCode.OK, "Success", cards);
    }

    public async Task<(HttpStatusCode statusCode, string message, CardDTO data)> GetCardById(Guid id)
    {
        var card = await _repository.GetAsync(id);
        if(card == null)
            return (HttpStatusCode.BadRequest, "Card not found", _mapper.Map<CardDTO>(card));
        
        return CanPerformAction(MethodPermissionsConfig.GetCardById, card) 
            ? (HttpStatusCode.OK, "Success", _mapper.Map<CardDTO>(card))
            : (HttpStatusCode.Forbidden, "User permission not allowed",new CardDTO());
    }

    public async Task<(HttpStatusCode statusCode, string message, CardDTO data)> CreateCard(Card card)
    {
        card.CreatedBy = user.Id;
        card.Status = CardStatus.ToDo;
        var response = _repository.Insert(card);
        _iuow.SaveChanges();
        
        return (HttpStatusCode.Created, "Success", _mapper.Map<CardDTO>(response));
    }

    public async Task<(HttpStatusCode statusCode, string message, CardDTO data)> UpdateCard(Card model)
    {
        var card = _repository.Get(model.Id);
        if (card == null)
            return (HttpStatusCode.BadRequest, "Card not found", _mapper.Map<CardDTO>(card));
            
        if (!CanPerformAction(MethodPermissionsConfig.UpdateCard, card))
            return (HttpStatusCode.Forbidden, "User permission not allowed",new CardDTO());

        card.Name = model.Name;
        card.Description = model.Description;
        card.Status = model.Status;
            
        _repository.Update(card);
        _iuow.SaveChanges();
        
        return (HttpStatusCode.OK, "Success", _mapper.Map<CardDTO>(card));
    }

    public async Task<(HttpStatusCode statusCode, string message, bool data)> DeleteCard(Guid id)
    {
        var card = _repository.Get(id);
        if (card == null)
            return (HttpStatusCode.BadRequest, "Card not found", false);

        if (!CanPerformAction(MethodPermissionsConfig.DeleteCard, card))
            return (HttpStatusCode.Forbidden, "User Permission not found",false);
        
        _repository.Delete(card);
        _iuow.SaveChanges();
        return (HttpStatusCode.OK, "Success", true);
    }

    public async Task<(HttpStatusCode statusCode, string message, List<CardDTO> data)> SearchCard(SearchDTO model)
    {
        var canViewAllCards = CanViewAllCards(MethodPermissionsConfig.DeleteCard);
        
        var cards = _repository.Get(
                filter: GetCardFilterExpression(model), orderBy: GetCardOrderBy(model.SortBy, model.SortOrder))
            .Where(c => canViewAllCards || c.CreatedBy.Equals(user.Id))
            .Skip(model.Offset).Take(model.Limit).Select(x => _mapper.Map<CardDTO>(x)).ToList();

        return (HttpStatusCode.OK, "success",cards);
    }

    private string? GetUserMethodPermission(UserRoles? userRoles, string method)
    {
        var roles = userRoles switch
        {
            UserRoles.Admin => _config.Admin,
            UserRoles.Member => _config.Member,
            _ => Array.Empty<string>()
        };
        return roles.Length < 1 ? String.Empty : roles.FirstOrDefault(x => x.StartsWith(method));
    }
    
    private bool CanPerformAction(string methodPermission,Card card)
    {
        var permission = GetUserMethodPermission(user.Role, methodPermission);
        if (String.IsNullOrWhiteSpace(permission))
            return false;
        if (permission.Equals(methodPermission + "_all", StringComparison.OrdinalIgnoreCase)
            || card.CreatedBy.Equals(user.Id)) 
            return true;

        return false;
    }

    private bool CanViewAllCards(string methodPermission)
    {
        var permission = GetUserMethodPermission(user.Role,methodPermission) + "_all";
        if (String.IsNullOrWhiteSpace(permission))
            return false;
        return  "search_cards_all".Equals(permission, StringComparison.OrdinalIgnoreCase);
    }
    
    private Expression<Func<Card, bool>> GetCardFilterExpression(SearchDTO card)
    {
        return c =>
            (string.IsNullOrEmpty(card.Name) || c.Name.Contains(card.Name)) &&
            (string.IsNullOrEmpty(card.Colour) || c.Colour == card.Colour) &&
            (string.IsNullOrEmpty(card.Status.ToString()) || c.Status == card.Status) &&
            (!card.CreatedDate.HasValue || c.CreatedOn.Date == card.CreatedDate.Value.Date);
    }
    
    private Func<IQueryable<Card>, IOrderedQueryable<Card>> GetCardOrderBy(string sortBy, string sortOrder)
    {
        switch (sortBy.ToLower())
        {
            case "colour":
                return sortOrder.ToLower() == "asc" ? q => q.OrderBy(c => c.Colour) : q => q.OrderByDescending(c => c.Colour);
            case "status":
                return sortOrder.ToLower() == "asc" ? q => q.OrderBy(c => c.Status) : q => q.OrderByDescending(c => c.Status);
            case "createdon":
                return sortOrder.ToLower() == "asc" ? q => q.OrderBy(c => c.CreatedOn) : q => q.OrderByDescending(c => c.CreatedOn);
            default:
                return sortOrder.ToLower() == "asc" ? q => q.OrderBy(c => c.Name) : q => q.OrderByDescending(c => c.Name);
        }
    }
}