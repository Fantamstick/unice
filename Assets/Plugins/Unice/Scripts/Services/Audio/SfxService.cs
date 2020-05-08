using System.Threading;
using Unice.Util;
using Unice.ViewHelpers.Audio;
using UnityEngine;
using UnityEngine.Audio;

namespace Unice.Services.Audio
{
    /// <summary>
    /// Service for handling SFX.
    /// </summary>
    [CreateAssetMenu(fileName = "new sfx service", menuName = "Unice/Services/Sfx")]
    public class SfxService : ScriptableObject
    {
        public SfxPoolTracker PoolTracker { private set; get; }
        
        [SerializeField, Tooltip("the Audio Mixer Group this Sfx service sends audio to.")]
        AudioMixerGroup audioMixerGroup = null;

        [SerializeField]
        int PoolSize = 1;

        /// <summary>
        /// Play Sfx audio track.
        /// </summary>
        /// <param name="audio">Audio clip and settings SO</param>
        /// <param name="followTarget">Target for the sfx to follow when playing.</param>
        public CancellationTokenSource Play(SfxAudio audio, Transform followTarget = null)
        {
            PoolTracker = PoolTracker ?? new SfxPoolTracker(PoolSize);

            try
            {
                // set transform parent
                if (followTarget == null) followTarget = Mic.Transform;
                
                // borrow sfx from pool
                SfxPlayComponent sfxPlayComponent = PoolTracker.Borrow(audio, followTarget);

                // play sfx and return cancellation token source
                return sfxPlayComponent.Play(audio, audioMixerGroup, followTarget);
            }
            catch (ErrPoolExhausted)
            {
                Debug.Log("Pool is full and audio is low priority. Skipping...");

                return null;
            }
        }
    }
}