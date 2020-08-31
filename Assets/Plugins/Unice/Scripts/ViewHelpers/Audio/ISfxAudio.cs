using Unice.Models;

public interface ISfxAudio
{
    IAudioSO Audio { get; }
    SfxPrioritySO Priority { get; }
}
