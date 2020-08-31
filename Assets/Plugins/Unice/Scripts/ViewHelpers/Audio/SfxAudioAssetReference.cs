using System;
using Unice.Models;
using UnityEngine;

namespace Unice.ViewHelpers.Audio
{
    [Serializable]
    public class SfxAudioAssetReference : ISfxAudio
    {
        [SerializeField] AudioAssetReferenceSO audio = default;
        [SerializeField] SfxPrioritySO priority = default;

        public IAudioSO Audio => audio;
        public SfxPrioritySO Priority => priority;
    }
}