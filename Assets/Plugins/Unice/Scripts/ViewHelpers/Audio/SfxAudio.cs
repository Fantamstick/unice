﻿using System;
using Unice.Models;
using UnityEngine;

namespace Unice.ViewHelpers.Audio
{
    [Serializable]
    public class SfxAudio : ISfxAudio
    {
        [SerializeField] AudioSO audio = default;
        [SerializeField] SfxPrioritySO priority = default;

        public IAudioSO Audio => audio;
        public SfxPrioritySO Priority => priority;
    }
}
