namespace Domain.Exceptions.NotFound
{
    public class MovieNotFoundException : NotFoundException
    {
        public MovieNotFoundException(int Id, string title = "Not found")
            : base($"The movie with Id {Id} was not found", title) { }
    }
}
