
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

public class DecimalModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult != ValueProviderResult.None)
        {
            var rawValue = valueProviderResult.FirstValue;

            // Replace commas with periods for proper parsing
            rawValue = rawValue.Replace(',', '.');

            if (decimal.TryParse(
                rawValue,
                NumberStyles.Number | NumberStyles.AllowCurrencySymbol,
                CultureInfo.InvariantCulture, out var parsedValue))
            {
                bindingContext.Result = ModelBindingResult.Success(parsedValue);
            }
            else
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid decimal format");
            }
        }

        return Task.CompletedTask;
    }
}
