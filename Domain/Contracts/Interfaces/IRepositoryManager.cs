namespace Domain.Contracts.Interfaces
{
    public interface IRepositoryManager
    {
        IMovieInfoRepository MovieInfoRepository { get; }
        IDirectorInfoRepository DirectorInfoRepository { get; }
        Task CompleteAsync();
    }
}
