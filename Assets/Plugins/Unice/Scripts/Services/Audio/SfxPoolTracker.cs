using System.Collections.Generic;
using System.Threading;
using Unice.Util;
using Unice.ViewHelpers.Audio;
using UniRx.Async;
using UnityEngine;

namespace Unice.Services.Audio
{
    /// <summary>
    /// SFX play component pool tracker.
    /// </summary>
    public class SfxPoolTracker
    {
        public int ActiveCount => activeSfx.Count;
        public int PoolSize => activeSfx.Count + pool.ItemsAvailable;
        
        readonly Pool<SfxPlayComponent> pool;
        readonly List<SfxPlayComponent> activeSfx;
        CancellationTokenSource cts;
        SfxPlayComponent lowestPrioritySfx = null;
        
        
        public SfxPoolTracker(int poolSize)
        {
            var sfxGameObject = new GameObject("Sfx Instance", typeof(AudioSource), typeof(SfxPlayComponent));

            pool = new Pool<SfxPlayComponent>(poolSize, sfxGameObject);
            activeSfx = new List<SfxPlayComponent>();
        }

        /// <summary>
        /// Borrow SFX play instance.
        /// </summary>
        public SfxPlayComponent Borrow(SfxAudio audio, Transform followTarget)
        {
            if (pool.ItemsAvailable == 0)
            {
                // obtain ranks of the requested sfx and compare it with the lowest ranked sfx.
                int rank = audio.Priority.GetRank(followTarget);
                int lowestRank = lowestPrioritySfx.GetRank();

                // don't play sfx if it has the lowest rank.
                if (rank == lowestRank) return null;
            
                // stop lowest priority sfx and return it to pool.
                Stop(lowestPrioritySfx);
            }

            try
            {
                SfxPlayComponent sfx = pool.Borrow();
                
                activeSfx.Add(sfx);
            
                // start updating when list is no longer empty.
                if (activeSfx.Count == 1)
                {
                    UpdateInstancesAsync().Forget();
                }
                
                return sfx;
            }
            catch (ErrPoolExhausted e)
            {
                Debug.LogError(e.Message);

                return null;
            }
        }

        /// <summary>
        /// Update all active SFX instances.
        /// </summary>
        /// <param name="ct">Cancellation token to stop update</param>
        async UniTask UpdateInstancesAsync()
        {
            while (true)
            {
                await Delay.NextFrame();
                
                for (int i = activeSfx.Count - 1; i >= 0; i--)
                {
                    SfxPlayComponent sfx = activeSfx[i];

                    // stopped
                    if (sfx.AudioSource == null || !sfx.AudioSource.isPlaying)
                    {
                        Return(sfx);
                        
                        if(activeSfx.Count == 0) return;
                    }
                    // canceled
                    else if (sfx.Ct.IsCancellationRequested || sfx.FollowTarget == null)
                    {
                        Stop(sfx);

                        if (activeSfx.Count == 0) return;
                    }
                    // playing
                    else
                    {
                        // update position
                        sfx.AudioSource.transform.position = sfx.FollowTarget.position;
                    }
                }

                lowestPrioritySfx = FindLowestSfxRank();
            }
        }

        void Stop(SfxPlayComponent sfx)
        {
            sfx.AudioSource.Stop();
            Return(sfx);
        }

        void Return(SfxPlayComponent sfx)
        {
            pool.Return(sfx);
            activeSfx.Remove(sfx);

            lowestPrioritySfx = FindLowestSfxRank();
        }
        
        SfxPlayComponent FindLowestSfxRank()
        {
            if (activeSfx.Count == 0) return null;
            
            var lowPrioritySfx = activeSfx[0];

            for (int i = 1; i < activeSfx.Count; i++)
            {
                int rank = activeSfx[i].GetRank();
                
                if (rank < lowPrioritySfx.GetRank())
                {
                    lowPrioritySfx = activeSfx[i];
                }
            }

            return lowPrioritySfx;
        }
    }
}
