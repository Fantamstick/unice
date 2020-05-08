using System;

namespace Unice.Util
{
    public class ErrWrongPool : Exception
    {
        public ErrWrongPool() { }
        public ErrWrongPool(string message) : base(message) { }
        public ErrWrongPool(string message, Exception inner) : base(message, inner) { }
    }
}