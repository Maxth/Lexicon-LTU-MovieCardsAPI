namespace Domain.Exceptions.BadRequest
{
    public abstract class BadRequestException : Exception
    {
        public string Title { get; }

        protected BadRequestException(string message, string title = "Bad request")
            : base(message)
        {
            Title = title;
        }
    }
}
