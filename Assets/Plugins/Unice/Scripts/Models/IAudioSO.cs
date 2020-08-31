using UnityEngine;

public interface IAudioSO {
    AudioClip GetAudioClip();
    AudioDetails Details { get; }
}
