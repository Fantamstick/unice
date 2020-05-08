using System;

namespace Unice.Util
{
    public class ErrPoolExhausted : Exception
    {
        public ErrPoolExhausted() { }
        public ErrPoolExhausted(string message) : base(message) { }
        public ErrPoolExhausted(string message, Exception inner) : base(message, inner) { }
    }
}