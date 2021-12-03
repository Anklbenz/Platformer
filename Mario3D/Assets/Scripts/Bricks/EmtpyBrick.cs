using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEnums;

public class EmtpyBrick : MonoBehaviour
{
    [SerializeField] protected GameObject _primaryMesh;
    [SerializeField] protected GameObject _secondaryMesh;
    [SerializeField] protected BoxCollider _brickCollider;
    protected bool _isActive = true;
    protected Bonus _bonus;

    private void Start() {
        _bonus = GetComponent<Bonus>();
    }

    public virtual void BrickHit(Character character) {
        if (!_isActive) return; 

        if (_bonus.numberOfBonuses > 0) {
            _bonus.Show(character);
            _bonus.Decrease();
            _brickCollider.enabled = true;
            _isActive = false;
            _secondaryMesh.SetActive(true);
        }
    }
}
