using System.ComponentModel.DataAnnotations;

namespace Cards.API.BindingModels.Validation;

public class ColorFormatAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return ValidationResult.Success;
        }

        string color = value.ToString();

        if (color.Length != 7 || color[0] != '#')
        {
            return new ValidationResult("Color should be 6 alphanumeric characters prefixed with a #.");
        }
        
        for (int i = 1; i < color.Length; i++)
        {
            if (!char.IsLetterOrDigit(color[i]))
            {
                return new ValidationResult("Color should be 6 alphanumeric characters prefixed with a #.");
            }
        }

        return ValidationResult.Success;
    }
}