﻿using UnityEngine;

public sealed class BrickBox : EmtpyBrick
{
    [SerializeField] private ParticleSystem _crushParticales;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _timeToDestroyBrickAfterHit;
    [SerializeField] private bool _brickCanBeCrushed = false;    

    private RaycastHit _rayHit;
    private Vector3 _enemyOnBrickCheckPlatformSize;
    private float _dropPlatformSizeWidth = 0.02f;
    private float _collisionCheckDistance;

    private bool EnemyOnTheBrick { //Overlap
        get => Physics.BoxCast(transform.position, _enemyOnBrickCheckPlatformSize / 2, Vector3.up, out _rayHit, transform.rotation, 
            _collisionCheckDistance) && (_rayHit.transform.GetComponent<ActiveInteractiveObject>() != null);        
    }

    protected override void Awake() {
        base.Awake();
        _collisionCheckDistance = _brickCollider.size.y / 2;
        _enemyOnBrickCheckPlatformSize = new Vector3(_brickCollider.size.x, _dropPlatformSizeWidth, _brickCollider.size.z);

        ParticleInitialize();  
    }

    private void ParticleInitialize() {
        if (_crushParticales) {
            _crushParticales = Instantiate(_crushParticales, this.transform);
            _crushParticales.transform.position = transform.position;
            _crushParticales.gameObject.SetActive(false);
        }
    }

    public override void BrickHit(Character character) {
        if (!_isActive) 
            return;
        else
            this.Drop();

        if (_bonusSpawner.numberOfBonuses > 0) {
            _animator.SetTrigger("hit");
            _bonusSpawner.Show(character);
            _bonusSpawner.Decrease();

            if (_bonusSpawner.numberOfBonuses == 0) {
                _isActive = false;
                _primaryMesh.SetActive(false);
                _secondaryMesh.SetActive(true);
            }

        } else if (character.CanCrush && _brickCanBeCrushed) {
            _animator.SetTrigger("hit");
            _crushParticales.gameObject.SetActive(true);
           
            Destroy(gameObject, _timeToDestroyBrickAfterHit);
        } else {
            _animator.SetTrigger("hit");
        }
    }

    public void Drop() {
        if (EnemyOnTheBrick)
            _rayHit.transform.gameObject.GetComponent<ActiveInteractiveObject>().Drop();
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * _collisionCheckDistance);
        Gizmos.DrawWireCube(transform.position + transform.up * _collisionCheckDistance, _enemyOnBrickCheckPlatformSize);
    }
}

