using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEnums;

public sealed class Bonus : MonoBehaviour
{
    [SerializeField] private Transform bonusCreationPoint;
    [Range(0, 10)][SerializeField] public int numberOfBonuses;
    [SerializeField] private float _coinDestroyTime = 0.5f;

    public BonusType bonusType;
    private GameObject bonus;

    private void Awake() {
        if (bonusType == BonusType.none)
            numberOfBonuses = 0;

        if (numberOfBonuses > 1 && bonusType != BonusType.coin)
            numberOfBonuses = 1;
    }

    public void Show(Character character) {
        if (numberOfBonuses < 1) {
            Destroy(this);
        } else {
            switch (bonusType) {
                case BonusType.none:
                    break;
                case BonusType.coin:
                    bonus = Resources.Load<GameObject>("Prefabs/Bonuses/Coin");
                    bonusCreationPoint = transform;
                    bonus = Instantiate(bonus,
                                        bonusCreationPoint.position,
                                        transform.rotation);
                    bonus.transform.parent = gameObject.transform;
                    Destroy(bonus, _coinDestroyTime);
                    break;

                case BonusType.levelUp:
                    if (character.CompareCurrentStateWith<JuniorState>()) {
                        bonus = Resources.Load<GameObject>("Prefabs/Bonuses/Mushroom");
                        bonus = Instantiate(bonus, bonusCreationPoint.position, transform.rotation);
                    } else {
                        bonus = Resources.Load<GameObject>("Prefabs/Bonuses/Flower");
                        bonus = Instantiate(bonus, bonusCreationPoint.position, transform.rotation);
                    }
                    break;
            }
        }
    }

    public void Decrease() {
        numberOfBonuses--;
    }


}
