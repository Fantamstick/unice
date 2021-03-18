using System.Collections.Generic;
using System.Threading;
using Unice.Models;
using Unice.Util;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace Unice.Services.Audio
{
    /// <summary>
    /// Service for handling BGM.
    /// </summary>
    [CreateAssetMenu(fileName = "new bgm service", menuName = "Unice/Services/Bgm")]
    public class BgmService : ScriptableObject
    {
        [SerializeField, Tooltip("the Audio Mixer Group this Bgm service sends audio to")]
        AudioMixerGroup audioMixerGroup = null;

        readonly List<BgmReference> audioReferences = new List<BgmReference>();
        
        /// <summary>
        /// Play Bgm audio track.
        /// </summary>
        /// <param name="audio">Audio clip and settings SO</param>
        /// <param name="transition">Audio transition SO</param>
        public void Play(IAudioSO audio, BgmTransitionSO transition)
        {
            // Stop playing bgm(s) currently playing
            Stop(transition.Outgoing);

            // setup audio source
            audio.LoadAsync().ContinueWith(() => {
                // play bgm
                var audioRef = CreateBgmReference(audio);
                audioRef.AudioSource.Play();

                // apply volume transition
                ApplyVolumeTransition(audioRef, audio.Details.MaxVolume, transition.Incoming).Forget();
            
                audioReferences.Add(audioRef);
            });
        }

        BgmReference CreateBgmReference(IAudioSO audio)
        {
            // create new bgm GameObject
            var audioSourceObj = new GameObject("Bgm Source");
            audioSourceObj.transform.SetParent(Mic.Transform);
            audioSourceObj.transform.localPosition = Vector3.zero;

            // create and setup bgm audio source
            var audioSource = audioSourceObj.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = audioMixerGroup;
            audioSource.clip = audio.GetAudioClip();
            audioSource.loop = audio.Details.Looping;

            return new BgmReference(audio, audioSourceObj, audioSource);
        }
        
        async UniTask ApplyVolumeTransition(BgmReference bgm, float maxVolume, AnimationCurveSO curve)
        {
            var cancellationToken = bgm.CreateTransitionCt();
            
            await Curve.EvaluateAsync(curve.Value, OnVolumeChanged, ct: cancellationToken);

            void OnVolumeChanged(float volume)
            {
                bgm.AudioSource.volume = volume * maxVolume;
            }
        }

        /// <summary>
        /// Stop all Bgm audio tracks.
        /// </summary>
        /// <param name="outgoingTransition">Outgoing volume transition curve</param>
        public void Stop(AnimationCurveSO outgoingTransition)
        {
            for (int i = audioReferences.Count - 1; i >= 0; i--)
            {
                StopTrackAsync(audioReferences[i], outgoingTransition).Forget();
            }
        }
        
        async UniTask StopTrackAsync(BgmReference bgm, AnimationCurveSO outgoingTransition)
        {
            // Cancel any transitions if not complete
            bgm.CancelVolumeTransition();
            
            // Remove from referencable list
            audioReferences.Remove(bgm);

            //BUG: If a track is fading in at this time, it will instantly play at "MaxVolume" before fading out.
            await ApplyVolumeTransition(bgm, bgm.AudioSO.Details.MaxVolume, outgoingTransition);

            bgm.AudioSO.Unload();
            
            // Destroy when outgoing transition is complete
            Destroy(bgm.GameObject);
        }

        /// <summary>
        /// Transition audio mixer to specified snapshot.
        /// </summary>
        /// <param name="snapshotName">Name of snapshot to transition to.</param>
        /// <param name="transitionTime">Duration from current to specified snapshot.</param>
        public void TransitionToSnapshot(string snapshotName, float transitionTime) 
        {
            var snapshot = audioMixerGroup.audioMixer.FindSnapshot(snapshotName);
            if (snapshot == null) 
            {
                Debug.LogWarning($"Unable to find audio mixer snapshot: {snapshotName}");
                return;
            }
            
            snapshot.TransitionTo(transitionTime);
        }
        
        class BgmReference
        {
            public readonly IAudioSO AudioSO;
            public readonly GameObject GameObject;
            public readonly AudioSource AudioSource;
            
            CancellationTokenSource cts;

            public BgmReference(IAudioSO audioSO, GameObject gameObject, AudioSource audioSource)
            {
                AudioSO = audioSO;
                GameObject = gameObject;
                AudioSource = audioSource;
            }
            
            public CancellationToken CreateTransitionCt()
            {
                cts = new CancellationTokenSource();
                
                return cts.Token;
            }

            public void CancelVolumeTransition() => cts.Cancel();
        }
    }
}