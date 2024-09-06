using System.IO.Compression;
using AutoMapper;
using Domain.Contracts.Interfaces;
using Infrastructure.Data;
using Service.Contracts;

namespace Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IMovieService> _movieService;
    private readonly Lazy<IDirectorService> _directorService;
    public IMovieService MovieService => _movieService.Value;
    public IDirectorService DirectorService => _directorService.Value;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _movieService = new Lazy<IMovieService>(() => new MovieService(repositoryManager, mapper));
        _directorService = new Lazy<IDirectorService>(
            () => new DirectorService(repositoryManager, mapper)
        );
    }
}
