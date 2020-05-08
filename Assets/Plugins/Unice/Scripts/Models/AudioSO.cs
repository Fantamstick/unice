using UnityEngine;

namespace Unice.Models
{
    /// <summary>
    /// Audio data object.
    /// </summary>
    [CreateAssetMenu(fileName = "new Audio", menuName = "Unice/Models/Audio")]
    public class AudioSO : ScriptableObject
    {
        public AudioClip AudioClip = null;

        /// <summary>
        /// "Volume slider. -1 is mute and 1 is max volume."
        /// </summary>
        [Range(-1.0f, 1.0f), Tooltip("Volume slider. -1 is mute and 1 is max volume.")]
        public float Volume = 0f;
        
        public bool Looping = false;

        /// <summary>
        /// The maximum volume this clip is to be played from the AudioSource's perspective.
        /// </summary>
        public float MaxVolume => (Volume + 1) / 2f;
    }
}
