using Cards.Infrastructure.Entities;
using Cards.Infrastructure.Repository.Abstract;
using Cards.Services.Interfaces;

namespace Cards.Services;

public class CardService : ICardService
{
    private readonly IUnitOfWork _iuow;
    private readonly IRepository<Card> _repository;

    public CardService(IUnitOfWork iuow)
    {
        _iuow = iuow;
        _repository = _iuow.Repository<Card>();
    }

    public List<Card> GetAllCards()
    {
        var cards = _repository.All.ToList();
        return cards;
    }

    public Card GetCardById(Guid Id)
    {
        var card = _repository.Get(Id);
        return card;
    }

    public Card CreateCard(Card card)
    {
        var response = _repository.Insert(card);
        _iuow.SaveChanges();
        return response;
    }

    public Card UpdateCard(Card card)
    {
         _repository.Update(card);
         _iuow.SaveChanges();
         return card;
    }

    public bool DeleteCard(Guid id)
    {
        var card = _repository.Get(id);
        _repository.Delete(card);
        _iuow.SaveChanges();
        return true;
    }
}