using UnityEngine;

namespace Unice.Models
{
    /// <summary>
    /// Bgm transition data object.
    /// </summary>
    [CreateAssetMenu(fileName = "new Bgm Transition", menuName = "Unice/Models/Bgm Transition")]
    public class BgmTransitionSO : ScriptableObject
    {
        /// <summary>
        /// Incoming volume curve over a duration.
        /// </summary>
        [Tooltip("Incoming volume curve over a duration.")]
        public AnimationCurveSO Incoming = null;

        /// <summary>
        /// Outgoing volume curve over a duration.
        /// </summary>
        [Tooltip("Outgoing volume curve over a duration.")]
        public AnimationCurveSO Outgoing = null;
    }
}
