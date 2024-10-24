using System;

namespace IMDB_API.CustomException
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }

    }
}
