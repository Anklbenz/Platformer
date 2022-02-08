using System;
using System.Collections.Generic;
using Interfaces;

namespace Game
{
    public class CoinSystem
    {
        public event Action CoinsCountChanged;
        public int TotalCoin{ get; private set; }

        private readonly List<ICoinCollectNotify> _coinCollectNotifies;

        public CoinSystem(List<ICoinCollectNotify> coinCollectNotifies) {
            _coinCollectNotifies = coinCollectNotifies;
            SubscribeAllActiveCoinCollectNotifies();
        }

        public void SubscribeColinCollectEvent(ICoinCollectNotify sender) {
            sender.CoinCollectNotify += AddCoin;
        }

        private void SubscribeAllActiveCoinCollectNotifies() {
            foreach (var instance in _coinCollectNotifies) {
                SubscribeColinCollectEvent(instance);
            }
        }

        private void AddCoin() {
            TotalCoin++;
            CoinsCountChanged?.Invoke();
        }
    }
}
