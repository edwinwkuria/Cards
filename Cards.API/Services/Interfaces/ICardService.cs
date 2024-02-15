using Cards.Infrastructure.Entities;

namespace Cards.Services.Interfaces;

public interface ICardService
{
    List<Card> GetAllCards();
    Card GetCardById(Guid id);
    Card CreateCard(Card card);
    Card UpdateCard(Card card);
    bool DeleteCard(Guid id);

}