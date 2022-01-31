using Enemy;

namespace Interfaces
{
    public interface IStateMethods
    {
        void BonusTake();
        void UnstopBonusTake();
        void EnemyTouch(ActiveEnemy sender);
    }
}
