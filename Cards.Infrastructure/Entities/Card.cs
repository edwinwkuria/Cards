using Cards.Infrastructure.DataTypes;

namespace Cards.Infrastructure.Entities;

public class Card : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Colour { get; set; }
    public CardStatus Status { get; set; }
}