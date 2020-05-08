using Unice.Models;
using UnityEngine;

namespace Unice.Services.Time
{
    /// <summary>
    /// Time service.
    /// </summary>
    [CreateAssetMenu(fileName = "new TimeService", menuName = "Unice/Services/Time")]
    public class TimeServiceSO : ScriptableObject
    {
        /// <summary>
        /// Time multiplier.<para />
        /// e.g. Multiplier = 0 (time stops)
        /// </summary>
        public FloatSO Multiplier;
        
        /// <summary>
        /// Return delta time after applying multiplier
        /// </summary>
        public float DeltaTime => UnityEngine.Time.deltaTime * Multiplier;
    }
}
