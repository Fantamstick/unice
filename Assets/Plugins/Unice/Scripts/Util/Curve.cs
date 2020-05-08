using System;
using System.Threading;
using Unice.Services.Time;
using UniRx.Async;
using UnityEngine;

namespace Unice.Util
{
    public static class Curve
    {
        /// <summary>
        /// Evaluate animation curve.
        /// </summary>
        /// <param name="curve">Animation curve</param>
        /// <param name="onUpdate">Evaluated value</param>
        /// <param name="timeService">Optional time service</param>
        /// <param name="ct">Optional cancellation token</param>
        public static async UniTask EvaluateAsync(AnimationCurve curve, Action<float> onUpdate, TimeServiceSO timeService = null, CancellationToken ct = default(CancellationToken))
        {
            float curveDuration = curve[curve.length - 1].time;
            float t = 0f;

            while (t < curveDuration)
            {
                var val = curve.Evaluate(t);
                
                onUpdate?.Invoke(val);
                
                // wait.
                await Delay.NextFrame(ct);

                // stop evaluation if cancelled.
                if (ct.IsCancellationRequested) break;
                
                // increment time.
                t += timeService != null ? timeService.DeltaTime : Time.deltaTime;
            }
        }
    }
}
