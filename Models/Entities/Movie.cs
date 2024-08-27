namespace MovieCardsApi.Entities
{
#nullable disable
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }

        //Foreign key
        public int DirectorId { get; set; }

        //Navigation props
        public Director Director { get; set; }
        public ICollection<Actor> Actor { get; set; }
        public ICollection<Genre> Genre { get; set; }
    }
}
