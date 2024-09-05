using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Exceptions.BadRequest
{
    public class NoJsonPatchException : BadRequestException
    {
        public NoJsonPatchException(
            string message = "No jsonpatch document",
            string title = "Bad request"
        )
            : base(message, title) { }
    }
}
