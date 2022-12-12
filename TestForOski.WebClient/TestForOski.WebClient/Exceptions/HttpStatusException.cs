using System;
using System.Net;

namespace TestForOski.WebClient.Exceptions
{
    public class HttpStatusException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public HttpStatusException(HttpStatusCode statusCode, string message) : base(message)
        {
            HttpStatusCode = statusCode;
        }
    }
}
