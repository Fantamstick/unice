using System;
using UnityEngine;

namespace Unice.Models
{
    /// <summary>
    /// Base class for primitive object data.
    /// </summary>
    /// <typeparam name="T">Primitive type.</typeparam>
    public abstract class PrimitiveSO<T> : ScriptableObject
    {
        [SerializeField]
        T value = default;

        /// <summary>
        /// Set value.
        /// </summary>
        /// <param name="value">Value to be assigned.</param>
        public void Set(T value)
        {
            this.value = value;

            // propagate changed value to event listeners.
            OnUpdate?.Invoke(this.value);
        }
        
        // Propagate an inspector changed value to OnUpdate event's listeners. (Editor-only)
        void OnValidate()
        {
            Set(value);
        }

        /// <summary>
        /// Update event when value changes.
        /// </summary>
        public event Action<T> OnUpdate;
        
        // implicit cast directly to value
        public static implicit operator T(PrimitiveSO<T> value) => value.value;
    }
}
