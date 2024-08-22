namespace MovieCardsApi.Models;

public class MovieItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string Description { get; set; }
    private double _rating;
    public double Rating
    {
        get { return Math.Round(_rating, 1); }
        set { _rating = value; }
    }
}
