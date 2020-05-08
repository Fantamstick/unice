using System.Threading;
using Unice.Models;
using Unice.Util;
using Unice.ViewHelpers.Audio;
using UnityEngine;
using UnityEngine.Audio;

namespace Unice.Services.Audio
{
    /// <summary>
    /// SFX play component.
    /// </summary>
    public class SfxPlayComponent : MonoBehaviour, IPoolable
    {
        /// <summary>
        /// The play component's audio source.
        /// </summary>
        public AudioSource AudioSource { private set; get; }
        /// <summary>
        /// The cancellation token which marks whether SFX was canceled or not.
        /// </summary>
        public CancellationToken Ct { private set; get; }
        /// <summary>
        /// The target that is being followed.
        /// </summary>
        public Transform FollowTarget { private set; get; }
        /// <summary>
        /// The priority of current SFX audio.
        /// </summary>
        public SfxPrioritySO Priority { private set; get; }
        
        public CancellationTokenSource Play(SfxAudio audioSO, AudioMixerGroup audioMixerGroup, Transform followTarget)
        {
            // setup position and priority settings
            FollowTarget = followTarget;
            Priority = audioSO.Priority;    
            
            // setup audio source
            AudioSource = GetComponent<AudioSource>();
            AudioSource.outputAudioMixerGroup = audioMixerGroup;
            AudioSource.clip = audioSO.Audio.AudioClip;
            AudioSource.loop = audioSO.Audio.Looping;
            
            // create cancellation token
            var cts = new CancellationTokenSource();
            Ct = cts.Token;
            
            // play
            AudioSource.Play();

            return cts;
        }

        public int GetRank()
        {
            return Priority.GetRank(FollowTarget);
        }
        
        void IPoolable.Clean()
        {
        }
    }
}
