using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Exceptions.UnprocessableEntity
{
    public abstract class UnprocessableEntityException : Exception
    {
        public string Title { get; }

        protected UnprocessableEntityException(
            string message,
            string title = "Unprocessable Entity"
        )
            : base(message)
        {
            Title = title;
        }
    }
}
