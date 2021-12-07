using UnityEngine;
using MyEnums;

public sealed class BonusSpawner : MonoBehaviour
{
    [SerializeField] private Transform bonusCreationPoint;
    [Range(0, 10)] [SerializeField] public int numberOfBonuses;

    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _mushroomPrefab;
    [SerializeField] private GameObject _flowerPrefab;

    [SerializeField] private BonusType bonusType;

    private void Awake() {
        if (bonusType == BonusType.none)
            numberOfBonuses = 0;

        if (numberOfBonuses > 1 && bonusType != BonusType.coin)
            numberOfBonuses = 1;
    }

    public void Show(Character character) {
        if (numberOfBonuses < 1) return;

        switch (bonusType) {
            case BonusType.coin:
                bonusCreationPoint = transform;
                Instantiate(_coinPrefab, bonusCreationPoint.position, Quaternion.identity, transform);
                break;

            case BonusType.levelUp:
                if (character.CompareCurrentStateWith<JuniorState>())
                    Instantiate(_mushroomPrefab, bonusCreationPoint.position, Quaternion.identity);
                else
                    Instantiate(_flowerPrefab, bonusCreationPoint.position, Quaternion.identity);
                break;
        }
    }

    public void Decrease() {
        numberOfBonuses--;
    }
}
