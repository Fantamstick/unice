using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Unice.Models
{
    /// <summary>
    /// Audio data object.
    /// </summary>
    [CreateAssetMenu(fileName = "new Audio", menuName = "Unice/Models/Audio")]
    public class AudioSO : ScriptableObject, IAudioSO
    {
        [SerializeField]
        AudioClip audioClip = null;

        [SerializeField] 
        AudioDetails details = null;
        public AudioDetails Details => details;

        public UniTask LoadAsync() => UniTask.CompletedTask;

        public AudioClip GetAudioClip() => audioClip;
    }
}
