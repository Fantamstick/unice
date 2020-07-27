using System.Threading;
using Unice.Util;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Unice.Services.Audio
{
    /// <summary>
    /// Global Mic (Audio Listener) class.
    /// </summary>
    public static class Mic
    {
        public static Transform Transform = null;
        
        static Transform followTransform = null;
        static bool isFollowMode = false;
        static CancellationTokenSource cts;
        
        /// <summary>
        /// Initialize mic automatically immediately after first scene is loaded.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void Init()
        {
            // create permanent microphone (audio listener)
            Transform = new GameObject("Mic", typeof(AudioListener)).transform;
            Object.DontDestroyOnLoad(Transform);
            
            // destroy other audio listeners
            DestroyDuplicateAudioListeners();
            
            // follow default camera
            var cam = FindDefaultCamera();
            Follow(cam.transform);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        static void DestroyDuplicateAudioListeners()
        {
            var micListener = Transform.GetComponent<AudioListener>();
            var audioListeners = Object.FindObjectsOfType<AudioListener>();
            foreach (var listener in audioListeners)
            {
                // don't destroy the mic's audio listener.
                if (listener == micListener) continue;

                Object.Destroy(listener);
            }
        }

        static Camera FindDefaultCamera()
        {
            // Main camera is the default camera if it exists.
            if (Camera.main != null) return Camera.main;

            var cameras = Object.FindObjectsOfType<Camera>();
            
            return cameras.Length > 0 ? cameras[0] : null;
        }
        
        static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            DestroyDuplicateAudioListeners();
        }

        /// <summary>
        /// Assign transform for the mic to follow.
        /// </summary>
        /// <param name="followTransform">Transform that mic will follow.</param>
        public static void Follow(Transform followTransform)
        {
            if(followTransform == null) throw new ErrMicFollowTransformNotFound();
            
            Mic.followTransform = followTransform;

            // begin to update mic position if we enter follow mode.
            if (!isFollowMode)
            {
                cts = new CancellationTokenSource();
                UpdateMicPositionAsync(cts.Token).Forget();
            }
            
            isFollowMode = true;
        }

        /// <summary>
        /// Set mic position
        /// </summary>
        /// <param name="position">Position for mic to stay.</param>
        public static void SetPosition(Vector3 position)
        {
            Transform.position = position;
            
            // cancel following transform.
            if (isFollowMode)
            {
                cts.Cancel();
            }
            
            isFollowMode = false;
        }

        static async UniTask UpdateMicPositionAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                await Delay.NextFrame(ct);
 
                // switch modes if the followed transform was destroyed.
                if (followTransform == null)
                {
                    isFollowMode = false;
                    Debug.LogError("Follow transform no longer found! Consider using SetPosition");
                    continue;
                }

                // update mic position.
                Transform.position = followTransform.position;
            }
        }
    }
}
