using System.ComponentModel.DataAnnotations;
using Cards.API.BindingModels.Validation;
using Cards.Infrastructure.DataTypes;

namespace Cards.BindingModels.CardsController;

public class EditCardBindingModel
{
    [Required(ErrorMessage = "Card name is required.")]
    public string Name { get; set; }
    public string? Description { get; set; }
    [ColorFormat(ErrorMessage = "Color should be 6 alphanumeric characters prefixed with a #.")]
    public string? Colour { get; set; }
    [Range(0, 2, ErrorMessage = "The value must be between 0 for To-do, 1 for In Progress and 2 for Done.")]
    public CardStatus Status { get; set; }
}