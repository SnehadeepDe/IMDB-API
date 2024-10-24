using System;

namespace IMDB_API.CustomException
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base() { }

        public BadRequestException(string message) : base(message) { }

    }
}
