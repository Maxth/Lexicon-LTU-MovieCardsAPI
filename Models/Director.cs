namespace MovieCardsApi.Models
{
#nullable disable
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public ICollection<Movie> Movie { get; set; }
        public ContactInformation ContactInformation { get; set; }
    }
}
