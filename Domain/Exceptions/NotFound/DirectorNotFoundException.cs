namespace Domain.Exceptions.NotFound
{
    public class DirectorNotFoundException : NotFoundException
    {
        public DirectorNotFoundException(int Id, string title = "Not found")
            : base($"The director with Id {Id} was not found", title) { }
    }
}
