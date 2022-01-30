using Character.States;
using Interfaces;

namespace Bonus
{
    public sealed class Flower : Bonus
    {
        private const int SCORE_LIST_ELEMENT = 5;

        protected override void BonusTake(IStateHandlerInteraction character) {
            character.BonusTake();
            base.SendScore(SCORE_LIST_ELEMENT);
        }
    }
}
