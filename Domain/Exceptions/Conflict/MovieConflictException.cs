using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Exceptions.Conflict
{
    public class MovieConflictException : ConflictException
    {
        public MovieConflictException(string title)
            : base($"A movie with the title {title} already exists") { }
    }
}
