using Unice.Attributes;
using Unice.Models;
using UnityEngine;

namespace Unice.Tests.Services.BgmService
{
    public class BgmServiceTester : MonoBehaviour
    {
        [SerializeField]
        Unice.Services.Audio.BgmService bgmService = null;

        [SerializeField]
        AudioSO bgm1 = null;
        
        [SerializeField]
        AudioSO bgm2 = null;
        
        [SerializeField]
        BgmTransitionSO transition = null;


        [MethodButton("Play Bgm1")]
        void PlayBgm1() => PlayBgm(bgm1);

        [MethodButton("Play Bgm2")]
        void PlayBgm2() => PlayBgm(bgm2);
        
        void PlayBgm(AudioSO audio)
        {
            bgmService.Play(audio, transition);
        }
    }
}
