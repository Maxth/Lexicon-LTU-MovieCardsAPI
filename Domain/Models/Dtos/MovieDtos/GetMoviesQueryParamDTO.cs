using System.ComponentModel.DataAnnotations;
using Domain.Validations;

namespace Domain.Models.Dtos.MovieDtos
{
    // [ValidateGetMoviesQueryParams]
    public class GetMoviesQueryParamDTO
    {
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public DateOnly? ReleaseDateFrom { get; set; }
        public DateOnly? ReleaseDateTo { get; set; }
        public string? ActorName { get; set; }
        public string? DirectorName { get; set; }
        public List<string>? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public bool? IncludeActors { get; set; }
    }
}
