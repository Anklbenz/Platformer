using Character.States;

namespace Bonus
{
    public sealed class Flower : Bonus
    {
        private const int SCORE_LIST_ELEMENT = 5;

        protected override void BonusTake(StateSystem character) {
            character.LevelUp();
            base.SendScore(SCORE_LIST_ELEMENT);
        }
    }
}
