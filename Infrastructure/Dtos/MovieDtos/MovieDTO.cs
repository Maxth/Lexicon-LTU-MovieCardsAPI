namespace Infrastructure.Dtos.MovieDtos
{
#nullable disable
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public double? Rating { get; set; }
    }
}
