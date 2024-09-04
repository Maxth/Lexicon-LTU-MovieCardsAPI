namespace Service.Contracts;

public interface IServiceManager
{
    IMovieService MovieService { get; }
    IDirectorService DirectorService { get; }
    Task CompleteAsync();
}
