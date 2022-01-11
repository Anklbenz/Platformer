using System.Collections;
using Character.States;
using UnityEngine;
using MyEnums;

public sealed class BrickBox : Brick
{
    private const float DROP_CHECK_DISTANCE = 0.1f;

    [SerializeField] private ParticleSystem _crushParticales;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _timeToDestroyBrickAfterHit;
    [SerializeField] private bool _brickCanBeCrushed = false;
    [SerializeField] private LayerMask _dropLayer;
    private Interactor _interactor;

    private void Awake() {
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

    public override void BrickHit(StateSystem state) {
        if (!_isActive) return;        

         this.DownHit();
        _animator.SetTrigger("hit");

        if (_bonusesCount > 0) {
            base.BonusShow(state);
            _bonusesCount--;

            if (_bonusesCount == 0) {
                _isActive = false;
                _primaryMesh.SetActive(false);
                _secondaryMesh.SetActive(true);
            }

        } else if (state.Data.CanCrush && _brickCanBeCrushed) {
            _crushParticales.gameObject.SetActive(true);
            Destroy(gameObject, _timeToDestroyBrickAfterHit);
        }
    }

    public void DownHit() {
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

