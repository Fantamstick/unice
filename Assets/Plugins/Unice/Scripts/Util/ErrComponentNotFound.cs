using System;

namespace Unice.Util
{
    public class ErrComponentNotFound : Exception
    {
        public ErrComponentNotFound() { }
        public ErrComponentNotFound(string message) : base(message) { }
        public ErrComponentNotFound(string message, Exception inner) : base(message, inner) { }
    }
}