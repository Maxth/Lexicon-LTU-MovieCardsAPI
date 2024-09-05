using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Exceptions.Conflict
{
    public class DirectorConflictException : ConflictException
    {
        public DirectorConflictException(DateOnly dob, string name)
            : base($"A director named {name} with birthdate {dob} already exists") { }
    }
}
