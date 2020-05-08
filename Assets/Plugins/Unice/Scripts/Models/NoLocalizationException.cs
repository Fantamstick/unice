using System;

namespace Unice.Models
{
    public class NoLocalizationException : Exception
    {
        public NoLocalizationException() { }
        public NoLocalizationException(string message) : base(message) { }
        public NoLocalizationException(string message, Exception inner) : base(message, inner) { }
    }
}