using Unice.Services.Audio;
using UnityEngine;

namespace Unice.Models
{
    /// <summary>
    /// Sfx priority data object.
    /// </summary>
    [CreateAssetMenu(fileName = "new SfxPriority", menuName = "Unice/Models/SfxPriority")]
    public class SfxPrioritySO : ScriptableObject
    {
        /// <summary>
        /// Value of priority. The higher the number, the higher the priority.
        /// </summary>
        public int Value;

        /// <summary>
        /// Get the priority rank.
        /// </summary>
        /// <param name="followTarget">The target that that audio source is following.</param>
        /// <returns>Priority rank.</returns>
        public int GetRank(Transform followTarget)
        {
            return Value - (int)Vector3.Distance(followTarget.position, Mic.Transform.position);
        }
    }
}
