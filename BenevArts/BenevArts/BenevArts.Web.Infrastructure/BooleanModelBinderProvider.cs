using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BenevArts.Web.Infrastructure
{
    public class BooleanModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(bool))
            {
                return new BooleanModelBinder();
            }

            return null;
        }
    }
}
