using System.ComponentModel.DataAnnotations;
using Cards.API.BindingModels.Validation;
using Cards.Infrastructure.DataTypes;

namespace Cards.BindingModels.CardsController;

public class CardBindingModel
{
    [Required(ErrorMessage = "Card name is required.")]
    public string Name { get; set; }
    public string? Description { get; set; }
    [ColorFormat(ErrorMessage = "Color should be 6 alphanumeric characters prefixed with a #.")]
    public string? Colour { get; set; }
}