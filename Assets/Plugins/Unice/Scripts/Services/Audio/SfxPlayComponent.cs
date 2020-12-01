using System.Threading;
using Cysharp.Threading.Tasks;
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
        
        public CancellationTokenSource Play(ISfxAudio audioSO, AudioMixerGroup audioMixerGroup, Transform followTarget)
        {
            // setup position and priority settings
            FollowTarget = followTarget;
            Priority = audioSO.Priority;    
            
            // setup audio source
            audioSO.Audio.LoadAsync().ContinueWith(() => {
                AudioSource = GetComponent<AudioSource>();
                AudioSource.outputAudioMixerGroup = audioMixerGroup;
                AudioSource.clip = audioSO.Audio.GetAudioClip();
                AudioSource.loop = audioSO.Audio.Details.Looping;
                AudioSource.volume = audioSO.Audio.Details.MaxVolume;
                AudioSource.gameObject.SetActive(true);
                
                // play
                AudioSource.Play();
            });
            
            // create cancellation token
            var cts = new CancellationTokenSource();
            Ct = cts.Token;

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
