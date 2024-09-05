namespace Domain.Exceptions.NotFound
{
    public class DirectorNotFoundException : NotFoundException
    {
        public DirectorNotFoundException(int Id)
            : base($"The director with Id {Id} was not found") { }

        public DirectorNotFoundException(int Id, string title)
            : base($"The director with Id {Id} was not found", title) { }
    }
}
