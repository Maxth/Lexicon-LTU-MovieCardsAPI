namespace Infrastructure.Dtos.MovieDtos
{
    // [ValidateGetMoviesQueryParams]
    public class GetMoviesQueryParamDTO
    {
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public string? ActorName { get; set; }
        public string? DirectorName { get; set; }
        public string? SortParam { get; set; }
        public string? SortOrder { get; set; }
        public bool? IncludeActors { get; set; }
    }
}
