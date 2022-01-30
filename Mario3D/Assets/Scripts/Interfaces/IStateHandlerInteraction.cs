using Enemy;

namespace Interfaces
{
    public interface IStateHandlerInteraction
    {
        void BonusTake();
        void UnstopBonusTake();
        void EnemyTouch(ActiveEnemy sender);
    }
}
