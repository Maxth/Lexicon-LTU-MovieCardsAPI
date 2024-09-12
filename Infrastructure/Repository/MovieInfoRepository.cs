using System.Linq.Expressions;
using Domain.Contracts.Interfaces;
using Domain.Models.Dtos.MovieDtos;
using Domain.Models.Entities;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class MovieInfoRepository : RepositoryBase<Movie>, IMovieInfoRepository
    {
        public MovieInfoRepository(MovieCardsDbContext context)
            : base(context) { }

        public async Task AddMovieAsync(Movie movie) => await CreateAsync(movie);

        public async Task<int> DeleteMovieAsync(int Id) =>
            await GetByCondition(m => m.Id == Id).ExecuteDeleteAsync();

        public async Task<bool> Exists(int Id) => await GetByCondition(m => m.Id == Id).AnyAsync();

        public async Task<Movie?> GetMovieDetailsAsync(int Id, bool trackChanges = false) =>
            await GetByCondition(m => m.Id == Id, trackChanges)
                .Include(m => m.Genre)
                .Include(m => m.Actor)
                .Include(m => m.Director)
                .ThenInclude(d => d.ContactInformation)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<Movie>> GetMoviesAsync(
            GetMoviesQueryParamDTO? paramDTO,
            bool trackChanges = false
        )
        {
            var query = GetAll(trackChanges);

            //INCLUDING RELATED ENTITIES DEPENDING ON QUERY PARAMS
            if (paramDTO?.IncludeActors == true || paramDTO?.ActorName != null)
            {
                query = query.Include(m => m.Actor);
            }
            if (paramDTO?.IncludeDirector == true || paramDTO?.DirectorName != null)
            {
                query = query.Include(m => m.Director);
            }
            if (paramDTO?.IncludeGenres == true || paramDTO?.Genre != null)
            {
                query = query.Include(m => m.Genre);
            }
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>


            //SEARCHING ---------------SEARCHING------------------SEARCHING
            if (paramDTO?.Title != null)
            {
                query = query.Where(m => m.Title.Contains(paramDTO.Title));
            }

            if (paramDTO?.DirectorName != null)
            {
                query = query.Where(m => m.Director.Name.Contains(paramDTO.DirectorName));
            }

            if (paramDTO?.ActorName != null)
            {
                query = query.Where(m =>
                    m.Actor.Select(a => a.Name).Any(n => n.Contains(paramDTO.ActorName))
                );
            }

            if (paramDTO?.Genre != null)
            {
                query = query.Where(m =>
                    m.Genre.Select(a => a.Name).Any(n => n.Contains(paramDTO.Genre))
                );
            }
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>


            //FILTERING-------------FILTERING---------------------FILTERING

            if (paramDTO?.ReleaseDateFrom != null)
            {
                query = query.Where(m => m.ReleaseDate >= paramDTO.ReleaseDateFrom);
            }

            if (paramDTO?.ReleaseDateTo != null)
            {
                query = query.Where(m => m.ReleaseDate <= paramDTO.ReleaseDateTo);
            }
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            //SORTING-----------SORTING--------------------------SORTING
            query = query.CustomOrderBy(paramDTO?.SortBy ?? []);
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            return await query.ToListAsync();
        }

        public async Task<Movie?> GetSingleMovieAsync(int Id, bool trackChanges = false) =>
            await GetByCondition(m => m.Id == Id, trackChanges).FirstOrDefaultAsync();
    }
}
