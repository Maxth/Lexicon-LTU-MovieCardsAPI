namespace Domain.Exceptions.NotFound
{
    public class MovieNotFoundException : NotFoundException
    {
        public MovieNotFoundException(int Id)
            : base($"The movie with Id {Id} was not found") { }

        public MovieNotFoundException(int Id, string title)
            : base($"The movie with Id {Id} was not found", title) { }
    }
}
