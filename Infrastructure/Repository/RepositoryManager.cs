using Domain.Contracts.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly MovieCardsDbContext _context;
        private readonly Lazy<IDirectorInfoRepository> _directorInfoRepository;
        private readonly Lazy<IMovieInfoRepository> _movieInfoRepository;
        public IDirectorInfoRepository DirectorInfoRepository => _directorInfoRepository.Value;
        public IMovieInfoRepository MovieInfoRepository => _movieInfoRepository.Value;

        public RepositoryManager(
            Lazy<IMovieInfoRepository> movieInfoRepository,
            Lazy<IDirectorInfoRepository> directorInfoRepository,
            MovieCardsDbContext context
        )
        {
            _directorInfoRepository = directorInfoRepository;
            _movieInfoRepository = movieInfoRepository;
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
