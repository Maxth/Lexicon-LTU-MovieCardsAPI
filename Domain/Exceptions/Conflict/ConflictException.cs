using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Exceptions.Conflict
{
    public abstract class ConflictException : Exception
    {
        public string Title { get; }

        protected ConflictException(string message, string title = "Conflict")
            : base(message)
        {
            Title = title;
        }
    }
}
