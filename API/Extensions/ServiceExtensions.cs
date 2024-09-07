using API.ModelBinding;
using Domain.Contracts.Interfaces;
using Infrastructure.Repository;
using Service;
using Service.Contracts;

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

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IDirectorInfoRepository, DirectorInfoRepository>();
            services.AddScoped<IMovieInfoRepository, MovieInfoRepository>();
            services.AddScoped(provider => new Lazy<IDirectorInfoRepository>(
                () => provider.GetRequiredService<IDirectorInfoRepository>()
            ));
            services.AddScoped(provider => new Lazy<IMovieInfoRepository>(
                () => provider.GetRequiredService<IMovieInfoRepository>()
            ));
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IDirectorService, DirectorService>();
            services.AddScoped(provider => new Lazy<IMovieService>(
                () => provider.GetRequiredService<IMovieService>()
            ));
            services.AddScoped(provider => new Lazy<IDirectorService>(
                () => provider.GetRequiredService<IDirectorService>()
            ));
        }
    }
}
