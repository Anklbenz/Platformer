using MyEnums;
using System;
using UnityEngine;

public class EmtpyBrick : MonoBehaviour, IBrickHit, IBonus
{
    public event Action<IBonus> BonusSpawnEvent;

    [Range(0, 10)]
    [SerializeField] protected int _bonusesCount;
    [SerializeField] protected BonusType _bonusType;
    [SerializeField] protected GameObject _primaryMesh;
    [SerializeField] protected GameObject _secondaryMesh;
    [SerializeField] protected BoxCollider _brickCollider;
    [SerializeField] protected Transform _bonusCreationPoint;
    [SerializeField] private GameManager _gameManager;
    protected bool _isActive = true;
    
    public Vector3 BonusCreatePoint =>_bonusCreationPoint.position;
    public BonusType BonusType => _bonusType;


    private void Start() {
        if (_gameManager)
            _gameManager.BonusSpawner.Subscribe(this);
    }

    private void OnDisable() {
        if (_gameManager)
            _gameManager.BonusSpawner.UnSubscribe(this);
    }

    public virtual void BrickHit(Character character) {
        if (!_isActive) return; 

        if (_bonusesCount > 0) {
            this.BonusShow(character);
            _bonusesCount--;
            _brickCollider.enabled = true;
            _isActive = false;
            _secondaryMesh.SetActive(true);
        }
    }

    protected void BonusShow(Character character) {
        if (_bonusType == BonusType.none) return;

        if (_bonusType == BonusType.mushroom || _bonusType == BonusType.flower) {
            if (character.CompareCurrentStateWith<JuniorState>()) {
                _bonusType = BonusType.mushroom;
            } else {
                _bonusType = BonusType.flower;
            }
        }

        BonusSpawnEvent?.Invoke(this);
    }
}
