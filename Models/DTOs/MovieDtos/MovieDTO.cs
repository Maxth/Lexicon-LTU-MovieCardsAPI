namespace MovieCardsAPI.DTOs
{
#nullable disable
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Rating { get; set; }
    }
}
