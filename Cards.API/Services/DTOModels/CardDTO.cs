using Cards.Infrastructure.DataTypes;

namespace Cards.Services.DTOModels;

public class CardDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Colour { get; set; }
    public CardStatus Status { get; set; }
}