using UnityEngine;

public class EmtpyBrick : MonoBehaviour, IBrickHit
{
    [SerializeField] protected GameObject _primaryMesh;
    [SerializeField] protected GameObject _secondaryMesh;
    [SerializeField] protected BoxCollider _brickCollider;

    public bool BrickInHitState { get; set; } = false;
    protected bool _isActive = true;
    protected BonusSpawner _bonusSpawner;

    protected virtual void Awake() {
        _bonusSpawner = GetComponent<BonusSpawner>();
    }

    public virtual void BrickHit(Character character) {
        if (!_isActive) return; 

        if (_bonusSpawner.numberOfBonuses > 0) {
            _bonusSpawner.Show(character);
            _bonusSpawner.Decrease();
            _brickCollider.enabled = true;
            _isActive = false;
            _secondaryMesh.SetActive(true);
        }
    }
}
