namespace Unice.Util
{
    public interface IPoolable
    {
        /// <summary>
        /// Clean poolable Component when returning to a pool.
        /// </summary>
        void Clean();
    }
}