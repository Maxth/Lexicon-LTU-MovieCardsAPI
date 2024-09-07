using Service.Contracts;

namespace Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IMovieService> _movieService;
    private readonly Lazy<IDirectorService> _directorService;
    public IMovieService MovieService => _movieService.Value;
    public IDirectorService DirectorService => _directorService.Value;

    public ServiceManager(Lazy<IMovieService> movieService, Lazy<IDirectorService> directorService)
    {
        _movieService = movieService;
        _directorService = directorService;
    }
}
