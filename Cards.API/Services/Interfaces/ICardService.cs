using System.Net;
using Cards.Infrastructure.Entities;
using Cards.Services.DTOModels;

namespace Cards.Services.Interfaces;

public interface ICardService
{
    Task<(HttpStatusCode statusCode, string message, List<CardDTO> data)> GetAllCards(GetCardsDTO cards);
    Task<(HttpStatusCode statusCode, string message, CardDTO data)> GetCardById(Guid id);
    Task<(HttpStatusCode statusCode, string message, CardDTO data)> CreateCard(Card card);
    Task<(HttpStatusCode statusCode, string message, CardDTO data)> UpdateCard(Card card);
    Task<(HttpStatusCode statusCode, string message, bool data)> DeleteCard(Guid id);
    Task<(HttpStatusCode statusCode, string message, List<CardDTO> data)> SearchCard(SearchDTO model);

}