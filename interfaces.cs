namespace MovieCards.Interfaces
{
    public interface IMovieCreationOrUpdateDto
    {
        public string Title { get; set; }

        DateOnly ReleaseDate { get; set; }
    }
}
