using UnityEngine;

public sealed class FireBall : MonoBehaviour
{
    private const  float MIN_FLY_HEIGHT  = 0.1f;
    private LayerMask _groundLayers;
    private float _bulletSpeed;
    private float _maxFlyHeight;
    private Vector3 _moveDirection;
    private Vector3 _ricocheteStartPoint;
    private bool _ricochetHappened = false;  

    private Rigidbody _rb;
    private Collider _col;

    private bool _rayVectorDown {
        get => Physics.Raycast(_col.bounds.center, Vector3.down, out RaycastHit _rayHit, _col.bounds.size.y / 2 + MIN_FLY_HEIGHT, _groundLayers);
    }

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();        
    }

    public void Initialize(Vector3 position, float bulletSpeed, float ricochetHeight, Vector3 moveDirection, LayerMask groundLayers) {
        gameObject.SetActive(true);
        transform.position = position;
        _bulletSpeed = bulletSpeed;
        _maxFlyHeight = ricochetHeight;
        _moveDirection = moveDirection + Vector3.down; // 45 degrees down
        _groundLayers = groundLayers;
        _ricochetHappened = false;
    }
    private void FixedUpdate() {
        if (!_ricochetHappened && _rayVectorDown)
            RicocheteState();

        Move();
    }

    private void Move() {
        if (_ricochetHappened) {
            if (_maxFlyHeight <= Vector3.Distance(_ricocheteStartPoint, transform.position)) {
                ChangeVerticalDirection();
                _ricocheteStartPoint = transform.position;
            }
        }
        _rb.velocity = _moveDirection * _bulletSpeed;
    }

    private void RicocheteState() {
        ChangeVerticalDirection();
        _ricochetHappened = true;
        _ricocheteStartPoint = transform.position;
    }

    private void ChangeVerticalDirection() {
        _moveDirection = new Vector3(_moveDirection.x, _moveDirection.y * -1, _moveDirection.z);
    }

    private void OnTriggerEnter(Collider other) {
        Hit(other);
    }

    private void Hit(Collider other) {
        ActiveEnemy activeEnemy = other.GetComponentInParent<ActiveEnemy>();
        if (activeEnemy) 
            activeEnemy.DownHit();
  
        Deactivate();
    }

    void Deactivate() {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos() {
        if (_col == null) return;
        Debug.DrawLine(transform.position, transform.position + Vector3.down * (_col.bounds.size.y / 2 + MIN_FLY_HEIGHT));
    }
}

