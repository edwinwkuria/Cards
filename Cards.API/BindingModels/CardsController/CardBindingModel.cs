using System.ComponentModel.DataAnnotations;
using Cards.Infrastructure.DataTypes;

namespace Cards.BindingModels.CardsController;

public class CardBindingModel
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Colour { get; set; }
    public CardStatus Status { get; set; }
}