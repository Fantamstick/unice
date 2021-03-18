using System;
using UnityEngine;

[Serializable]
public class AudioDetails {
    /// <summary>
    /// "Volume slider. -1 is mute and 1 is max volume."
    /// </summary>
    [Range(-1.0f, 1.0f), Tooltip("Volume slider. -1 is mute and 1 is max volume.")]
    public float Volume = 0f;
        
    /// <summary>
    /// Audio plays repeating.
    /// </summary>
    public bool Looping = false;

    /// <summary>
    /// Audio plays one time and is then unloaded.
    /// </summary>
    public bool UnloadAfterPlay = false;
    
    /// <summary>
    /// The maximum volume this clip is to be played from the AudioSource's perspective.
    /// </summary>
    public float MaxVolume => (Volume + 1) / 2f;
}
