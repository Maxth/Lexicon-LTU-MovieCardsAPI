namespace MovieCardsApi.Models
{
#nullable disable
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public ICollection<Movie> Movie { get; set; }
    }
}
