using MyEnums;
using UnityEngine;

public class Motor : MonoBehaviour
{
    [Range(0, 15)]
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Direction MoveDirection;
    [SerializeField] private float _bouncePower = 20f;
    protected Rigidbody _rb;
    private bool _isActive = true;
    private Vector3 _direction = Vector3.back;

    private void Awake() {
        _direction = MoveDirection == Direction.left ? Vector3.back : Vector3.forward;
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (_isActive)
            Patrolling();
    }

    public void SetSpeed(float _spd) {
        _speed = _spd;
    }

    public float GetSpeed() {
        return _speed;
    }

    public void SetActive(bool state) {
        _isActive = state;
    }

    public void DirectionChange() {
        _direction = _direction == Vector3.back ? Vector3.forward : Vector3.back;
    }

    public void SetDirection(Direction dir) {
        _direction = dir == Direction.left ? Vector3.back : Vector3.forward;
    }

    public Vector3 GetDirection() {
        return _direction;
    }

    private void Patrolling() {
        _rb.MovePosition(_rb.position + _direction * _speed * Time.fixedDeltaTime);
    }

    public void Bounce() {
        StopVerticalMove();
        _rb.AddForce(Vector3.up * _bouncePower, ForceMode.Impulse);
    }

    protected void StopVerticalMove() {
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
    }
}