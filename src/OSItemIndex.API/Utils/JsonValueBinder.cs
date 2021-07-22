using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Serilog;

namespace OSItemIndex.API.Utils
{
    internal class JsonValueBinder : IModelBinder
    {
        private readonly IObjectModelValidator _validator;

        public JsonValueBinder(IObjectModelValidator validator)
        {
            _validator = validator;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.FieldName).FirstValue;
            if (value == null)
            {
                return Task.CompletedTask;
            }

            try
            {
                var parsed = JsonSerializer.Deserialize("\"" + value + "\"", bindingContext.ModelType, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                bindingContext.Result = ModelBindingResult.Success(parsed);

                if (parsed != null)
                {
                    _validator.Validate(
                                        bindingContext.ActionContext,
                                        validationState: bindingContext.ValidationState,
                                        prefix: string.Empty,
                                        model: parsed
                                       );
                }
            }
            catch (JsonException e)
            {
                Log.Error(e, "Failed to bind parameter '{FieldName}'", bindingContext.FieldName);
                bindingContext.ActionContext.ModelState.TryAddModelError(e.Path ?? string.Empty, e,
                                                                         bindingContext.ModelMetadata);
            }
            catch (Exception e) when (e is FormatException or OverflowException)
            {
                Log.Error(e, "Failed to bind parameter '{FieldName}'", bindingContext.FieldName);
                bindingContext.ActionContext.ModelState.TryAddModelError(string.Empty, e, bindingContext.ModelMetadata);
            }

            return Task.CompletedTask;
        }
    }
}
