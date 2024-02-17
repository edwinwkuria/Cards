using AutoMapper;
using Cards.API.ConfigModels;
using Cards.Infrastructure.DataTypes;
using Cards.Infrastructure.Entities;
using Cards.Infrastructure.Repository.Abstract;
using Cards.Services.DTOModels;
using Cards.Services.Interfaces;
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

    public List<CardDTO> GetAllCards()
    {
        var permission = GetUserMethodPermission(user.Role, MethodPermissionsConfig.GetAllCards);
        return permission switch
        {
            "read_cards_all" => _repository.All.Select(x => _mapper.Map<CardDTO>(x)).ToList(),
            "read_cards_own" => _repository.All.Where(y => y.CreatedBy.Equals(user.Id))
                .Select(x => _mapper.Map<CardDTO>(x)).ToList(),
            _ => new List<CardDTO>()
        };
    }

    public CardDTO GetCardById(Guid id)
    {
        var card = _repository.Get(id);
        
        return CanPerformAction(MethodPermissionsConfig.GetCardById, card) 
            ? _mapper.Map<CardDTO>(card) : new CardDTO();
    }

    public CardDTO CreateCard(Card card)
    {
        card.CreatedBy = user.Id;
        card.Status = CardStatus.ToDo;
        var response = _repository.Insert(card);
        _iuow.SaveChanges();
        
        return _mapper.Map<CardDTO>(response);
    }

    public CardDTO UpdateCard(Card model)
    {
        var card = _repository.Get(model.Id);
        if (!CanPerformAction(MethodPermissionsConfig.UpdateCard, card))
            return new CardDTO();

        card.Name = model.Name;
        card.Description = model.Description;
        card.Status = model.Status;
            
        _repository.Update(card);
        _iuow.SaveChanges();
        
        return _mapper.Map<CardDTO>(card);
    }

    public bool DeleteCard(Guid id)
    {
        var card = _repository.Get(id);

        if (!CanPerformAction(MethodPermissionsConfig.DeleteCard, card))
            return false;
        
        _repository.Delete(card);
        _iuow.SaveChanges();
        return true;    }

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
}