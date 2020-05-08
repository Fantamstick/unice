using UnityEngine;

namespace Unice.Models
{
    /// <summary>
    /// AnimationCurve data object.
    /// </summary>
    [CreateAssetMenu(fileName = "new AnimationCurve", menuName = "Unice/Models/AnimationCurve")]
    public class AnimationCurveSO : ScriptableObject
    {
        public AnimationCurve Value;
    }
}
