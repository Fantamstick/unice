using System;

namespace Unice.Util
{
    public class ErrResourceNotFound : Exception
    {
        public ErrResourceNotFound() { }
        public ErrResourceNotFound(string message) : base(message) { }
        public ErrResourceNotFound(string message, Exception inner) : base(message, inner) { }
    }
}