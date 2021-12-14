﻿using System.Collections;
using UnityEngine;
using MyEnums;

public sealed class BrickBox : EmtpyBrick
{
    private const float DROP_CHECK_DISTANCE = 0.1f;

    [SerializeField] private ParticleSystem _crushParticales;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _timeToDestroyBrickAfterHit;
    [SerializeField] private bool _brickCanBeCrushed = false;
    [SerializeField] private LayerMask _dropLayer;
    private Interactor _interactor;

    protected override void Awake() {
        base.Awake();
        ParticleInitialize();      
        _interactor = new Interactor(_brickCollider, Axis.vertical, DROP_CHECK_DISTANCE, _dropLayer);
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

        _animator.SetTrigger("hit");   

        if (_bonusSpawner.numberOfBonuses > 0) {
            _bonusSpawner.Show(character);
            _bonusSpawner.Decrease();

            if (_bonusSpawner.numberOfBonuses == 0) {
                _isActive = false;
                _primaryMesh.SetActive(false);
                _secondaryMesh.SetActive(true);
            }

        } else if (character.CanCrush && _brickCanBeCrushed) {
            _crushParticales.gameObject.SetActive(true);
            Destroy(gameObject, _timeToDestroyBrickAfterHit);
        }
    }

    public void Drop() {
        var interactions = _interactor.InteractionOverlap(Vector3.up);
        if (interactions.Length > 0) {
            foreach (var obj in interactions) {
                ActiveInteractiveObject activeObject = obj.transform.GetComponentInParent<ActiveInteractiveObject>();
                if (activeObject)
                    activeObject.DownHit();
            }
        }
    }

    void OnDrawGizmos() {
        if (_interactor==null) return;
        _interactor.OnDrawGizmos(Color.red);
    }
}

