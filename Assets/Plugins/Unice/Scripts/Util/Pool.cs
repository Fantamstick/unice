using System.Collections.Generic;
using UnityEngine;

namespace Unice.Util
{
    /// <summary>
    /// Pool of GameObjects with an attached component T.
    /// </summary>
    /// <typeparam name="T">Type of attached component.</typeparam>
    public class Pool<T> where T : Component, IPoolable
    {
        /// <summary>
        /// The number of items available in the pool.
        /// </summary>
        public int ItemsAvailable => available.Count;
        
        List<T> available;
        List<T> onLoan;

        /// <summary>
        /// Create pool.
        /// </summary>
        /// <param name="size">Initial size of pool.</param>
        /// <param name="prefabFilepath">Filepath of prefab found in a Resources folder.</param>
        /// <exception cref="ErrResourceNotFound">Prefab not found exception.</exception>
        public Pool(int size, string prefabFilepath)
        {
            var prefab = Resources.Load<GameObject>(prefabFilepath) ?? throw new ErrResourceNotFound();

            CreateInstances(size, prefab);
        }

        /// <summary>
        /// Create pool.
        /// </summary>
        /// <param name="size">Initial size of pool.</param>
        /// <param name="prefab">Prefab of instance.</param>
        public Pool(int size, GameObject prefab)
        {
            CreateInstances(size, prefab.gameObject);
        }

        void CreateInstances(int size, GameObject prefab)
        {
            available = new List<T>();
            onLoan = new List<T>();
            
            for (var i = 0; i < size; i++)
            {
                GameObject instance = Object.Instantiate(prefab);
                instance.SetActive(false);

                T component = instance.GetComponent<T>();
                if(component == null) throw new ErrComponentNotFound();
                
                component.Clean();
                
                available.Add(component);
            }
        }
        
        /// <summary>
        /// Obtain instance from pool.
        /// </summary>
        /// <returns>Pool instance.</returns>
        /// <exception cref="ErrPoolExhausted">Pool is empty.</exception>
        public T Borrow()
        {
            if (available.Count == 0) throw new ErrPoolExhausted();

            T give = available[0];
            give.gameObject.SetActive(true);
            available.Remove(give);
            onLoan.Add(give);
            
            return give;
        }

        /// <summary>
        /// Return borrowed instance back to pool.
        /// </summary>
        /// <param name="borrowed">Borrowed instance</param>
        /// <exception cref="ErrWrongPool">Instance is not from this pool.</exception>
        public void Return(T borrowed)
        {
            if (!onLoan.Contains(borrowed)) throw new ErrWrongPool();

            borrowed.gameObject.SetActive(false);
            borrowed.transform.SetParent(null);
            borrowed.Clean();
            
            onLoan.Remove(borrowed);
            available.Add(borrowed);
        }
    }
}