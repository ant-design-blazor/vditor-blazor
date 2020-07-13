using Vditor;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddVditor(this IServiceCollection services)
        {
            services.AddScoped<EditorService>();
        }
    }
}