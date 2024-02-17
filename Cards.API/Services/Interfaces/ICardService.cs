using System.Net;
using Cards.Infrastructure.Entities;
using Cards.Services.DTOModels;

namespace Cards.Services.Interfaces;

public interface ICardService
{
    (HttpStatusCode statusCode, string message, List<CardDTO> data) GetAllCards();
    (HttpStatusCode statusCode, string message, CardDTO data) GetCardById(Guid id);
    (HttpStatusCode statusCode, string message, CardDTO data) CreateCard(Card card);
    (HttpStatusCode statusCode, string message, CardDTO data) UpdateCard(Card card);
    (HttpStatusCode statusCode, string message, bool data) DeleteCard(Guid id);
    (HttpStatusCode statusCode, string message, List<CardDTO> data) SearchCard(SearchDTO model);

}