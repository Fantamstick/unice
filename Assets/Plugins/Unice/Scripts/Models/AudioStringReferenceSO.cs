using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Unice.Models {
    /// <summary>
    /// Audio string reference data object.
    /// </summary>
    [CreateAssetMenu(fileName = "new Audio String Reference", menuName = "Unice/Models/Audio String Reference")]
    public class AudioStringReferenceSO : ScriptableObject, IAudioSO {
        /// <summary>
        /// String reference in Addressables.
        /// </summary>
        public string StringReference = default;

        /// <summary>
        /// Asset reference handle.
        /// </summary>
        public AsyncOperationHandle<AudioClip>? Handle;

        [SerializeField] 
        AudioDetails details = default;
        public AudioDetails Details => details;
        
        /// <summary>
        /// Load audio clip asset into memory.
        /// </summary>
        public async UniTask LoadAsync() {
            if (!Handle.HasValue) {
                Handle = Addressables.LoadAssetAsync<AudioClip>(StringReference);
                await Handle.Value.Task;
            }
        }
    
        /// <summary>
        /// Get Loaded audio clip.
        /// </summary>
        /// <returns></returns>
        public AudioClip GetAudioClip() {
            if (!Handle.HasValue) {
                Debug.LogError("Clip not found. Did you LoadAsync?");
                return null;
            }

            return Handle.Value.Result;
        }

        /// <summary>
        /// Unload audio clip from memory.
        /// </summary>
        public void Unload() {
            if (Handle.HasValue) {
                Addressables.Release(Handle.Value);
                Handle = null;
            }
        }
    }
}