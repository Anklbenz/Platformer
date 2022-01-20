using System;
using System.Collections.Generic;
using Interfaces;

namespace Game
{
    public class CoinSystem
    {
        public event Action CoinsCountChanged;
        public int TotalCoin => _coinCount; 
    
        private int _coinCount;

        private List<ICoinCollectNotify> _coinCollectNotifies;

        public CoinSystem(List<ICoinCollectNotify> coinCollectNotifies) {
            _coinCollectNotifies = coinCollectNotifies;
            SubscribeAllActiveCoinColletNotifies();
        }

        public void SubscribeColinCollectEvent(ICoinCollectNotify sender) {
            sender.CoinCollectNotify += AddCoin;
        }

        private void SubscribeAllActiveCoinColletNotifies() {
            foreach (var instance in _coinCollectNotifies) {
                SubscribeColinCollectEvent(instance);
            }
        }

        private void AddCoin() {
            _coinCount++;
            CoinsCountChanged?.Invoke();
        }
    }
}
