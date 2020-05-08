using System.Threading;
using UniRx.Async;

namespace Unice.Util
{
    public static class Delay
    {
        public static UniTask Time(int ms, CancellationToken ct = default(CancellationToken))
        {
            return UniTask.Delay(ms, cancellationToken: ct);
        }
        
        public static UniTask<int> NextFrame(CancellationToken ct = default(CancellationToken))
        {
            return UniTask.DelayFrame(1, cancellationToken: ct);
        }

        public static UniTask None()
        {
            return UniTask.Delay(0);
        }
    }
}