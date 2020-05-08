using System;
using System.Threading;
using Unice.Attributes;
using Unice.ViewHelpers.Audio;
using UnityEngine;

namespace Unice.Tests.Services.SfxService
{
    public class SfxServiceTester : MonoBehaviour
    {
        [SerializeField]
        Unice.Services.Audio.SfxService sfxService = null;

        [SerializeField]
        SfxAudio[] sfxArray = null;

        CancellationTokenSource[] ctsArray = null;

        void Awake()
        {
            ctsArray = new CancellationTokenSource[sfxArray.Length];
        }
        
        [MethodButton("Play Sfx")]
        void PlaySfx(int index)
        {
            try
            {
                ctsArray[index] = sfxService.Play(sfxArray[index]);
            }
            catch (IndexOutOfRangeException)
            {
                Debug.LogError($"Index out of range! Use indices from 0 to {sfxArray.Length - 1}");
            }
        }
        
        [MethodButton("Cancel Sfx")]
        void CancelSfx(int index)
        {
            try
            {
                ctsArray[index].Cancel();
            }
            catch (IndexOutOfRangeException)
            {
                Debug.LogError($"Index out of range! Use indices from 0 to {sfxArray.Length - 1}");
            }
            catch (NullReferenceException)
            {
                Debug.LogError($"Token source doesn't exist for {index}");
            }
        }
    }
}
