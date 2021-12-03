using UnityEngine;

public sealed class FireBall : MonoBehaviour
{
    private LayerMask _groundLayers;
    private float _bulletSpeed;
    private float _maxFlyHeight;
    private const  float MIN_FLY_HEIGHT  = 0.1f;
    private Vector3 _moveDirection;
    private Vector3 _ricocheteStartPoint;
    private bool _ricochetHappened = false;
  

    private Rigidbody _rb;
    private Collider _col;
    RaycastHit _rayHit;
    private float rayLength;

    private bool _rayVectorDown {
        get => Physics.Raycast(_col.bounds.center, Vector3.down, out  _rayHit, _col.bounds.size.y/2 + MIN_FLY_HEIGHT, _groundLayers);
    }

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
        
    }

    private void FixedUpdate() {

        if (!_ricochetHappened && _rayVectorDown) {
           // Debug.LogError(rayLength + MIN_FLY_HEIGHT);
            RicocheteState();
        }

        Move();
    }

    private void OnTriggerEnter(Collider other) {
        Hit(other);
    }

    public void Initialize(Vector3 position, float bulletSpeed, float ricochetHeight, Vector3 direction, LayerMask groundLayers) {
        _ricochetHappened = false;
        transform.position = position;
        _bulletSpeed = bulletSpeed;
        _maxFlyHeight = ricochetHeight;
        _moveDirection = direction + Vector3.down; // 45 degrees down
        _groundLayers = groundLayers;
    }

    private void Move() {
        if (_ricochetHappened) {
            if (_maxFlyHeight <= Vector3.Distance(_ricocheteStartPoint, transform.position)) {
                ChangeVerticalDirection();
                _ricocheteStartPoint = transform.position;
            }
        }
        _rb.velocity = _moveDirection * _bulletSpeed ;
       // _rb.MovePosition(_rb.position + _moveDirection * _bulletSpeed * Time.fixedDeltaTime);
    }

    private void RicocheteState() {
        ChangeVerticalDirection();
        _ricochetHappened = true;
        _ricocheteStartPoint = transform.position;
    }

    private void Hit(Collider other) {
        ActiveEnemy activeEnemy = other.GetComponentInParent<ActiveEnemy>();
        if (activeEnemy) {
            activeEnemy.Drop();
            Deactivate();
            return;
        }
        Deactivate();
    }

    private void ChangeVerticalDirection() {
        _moveDirection = new Vector3(_moveDirection.x, _moveDirection.y * -1, _moveDirection.z);
    }

    void Deactivate() {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos() {
        if (_col == null) return;
        Debug.DrawLine(transform.position, transform.position + Vector3.down * (_col.bounds.size.y / 2 + MIN_FLY_HEIGHT));
    }
}

