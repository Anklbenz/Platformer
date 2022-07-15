using System;

namespace Interfaces
{
    public interface ICoinCollectNotify
    {
        event Action CoinCollectNotify;
    }
}
