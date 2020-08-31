using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Unice.Models {
    [Serializable]
    public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
    {
        public AssetReferenceAudioClip(string guid) : base(guid) { }
    }

}

