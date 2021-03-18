using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IAudioSO {
    AudioClip GetAudioClip();
    AudioDetails Details { get; }
    UniTask LoadAsync();
    void Unload();
}
