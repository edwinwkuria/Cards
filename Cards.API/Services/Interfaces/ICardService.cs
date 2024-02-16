using Cards.Infrastructure.Entities;
using Cards.Services.DTOModels;

namespace Cards.Services.Interfaces;

public interface ICardService
{
    List<CardDTO> GetAllCards();
    CardDTO GetCardById(Guid id);
    CardDTO CreateCard(Card card);
    CardDTO UpdateCard(Card card);
    bool DeleteCard(Guid id);

}