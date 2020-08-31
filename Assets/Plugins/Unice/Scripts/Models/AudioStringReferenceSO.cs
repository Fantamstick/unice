using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

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

        AudioClip clip;
        
        [SerializeField] 
        AudioDetails details = default;
        public AudioDetails Details => details;
        
        /// <summary>
        /// Load audio clip asset into memory.
        /// </summary>
        public async UniTask LoadAsync() {
            if (clip == null) {
                clip = await Addressables.LoadAssetAsync<AudioClip>(StringReference).Task;
            }
        }
    
        /// <summary>
        /// Get Loaded audio clip.
        /// </summary>
        /// <returns></returns>
        public AudioClip GetAudioClip() {
            if (clip == null) {
                Debug.LogError("Clip not found. Did you LoadAsync?");
            }

            return clip;
        }

        /// <summary>
        /// Unload audio clip from memory.
        /// </summary>
        public void Unload() {
            if (clip != null) {
                Addressables.Release(clip);
            }
        }
    }
}