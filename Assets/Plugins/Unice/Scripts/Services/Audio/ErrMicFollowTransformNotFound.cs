using System;

namespace Unice.Services.Audio
{
    public class ErrMicFollowTransformNotFound : Exception
    {
        public ErrMicFollowTransformNotFound() { }
        public ErrMicFollowTransformNotFound(string message) : base(message) { }
        public ErrMicFollowTransformNotFound(string message, Exception inner) : base(message, inner) { }
    }
}