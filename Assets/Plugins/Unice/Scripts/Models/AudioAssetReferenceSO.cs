using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Unice.Models {
    /// <summary>
    /// Audio asset reference data object.
    /// </summary>
    [CreateAssetMenu(fileName = "new Audio Asset Reference", menuName = "Unice/Models/Audio Asset Reference")]
    public class AudioAssetReferenceSO : ScriptableObject, IAudioSO {
        public AssetReferenceAudioClip AudioClipReference;

        [SerializeField] 
        AudioDetails details = default;
        public AudioDetails Details => details;
        
        /// <summary>
        /// Load audio clip asset into memory.
        /// </summary>
        public async UniTask LoadAssetAsync() {
            if (AudioClipReference.Asset == null) {
                await AudioClipReference.LoadAssetAsync();
            }
        }
    
        /// <summary>
        /// Get Loaded audio clip.
        /// </summary>
        /// <returns></returns>
        public AudioClip GetAudioClip() {
            if (AudioClipReference.Asset == null) {
                Debug.LogError("Clip not found. Did you LoadAsync?");
            }

            return AudioClipReference.Asset as AudioClip;
        }

        /// <summary>
        /// Unload audio clip from memory.
        /// </summary>
        public void Unload() {
            if (AudioClipReference.Asset != null) {
                AudioClipReference.ReleaseAsset();
            }
        }
    }
}

