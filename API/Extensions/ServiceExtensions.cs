using API.ModelBinding;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureControllers(this IServiceCollection services)
        {
            services
                .AddControllers(options =>
                {
                    options.ModelBinderProviders.Insert(0, new CustomModelBinderProvider());
                    options.ReturnHttpNotAcceptable = true;
                })
                .AddNewtonsoftJson()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
        }
    }
}
