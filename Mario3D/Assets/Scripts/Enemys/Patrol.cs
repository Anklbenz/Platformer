using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEnums;

public sealed class Patrol : MonoBehaviour
{
    [Range(0, 15)] [SerializeField] private float _speed = 2f;
    [SerializeField] private Direction MoveDirection;
    private Rigidbody _rb;
    private bool _isActive = true;
    private Vector3 _direction = Vector3.back;

    private void Awake() {
        _direction = MoveDirection == Direction.left ? Vector3.back : Vector3.forward;
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (_isActive)
            Patrolling();
    }

    public void SetSpeed(float _spd) {
        _speed = _spd;
    }
    public float GetSpeed() {
        return _speed ;
    }

    public void SetActive(bool state) {
        _isActive = state;
    }

    public void SetDirection(Direction d) {
        _direction = d == Direction.left ? Vector3.back : Vector3.forward;
    }

    public Vector3 GetDirection() {
        return _direction;
    }

    private void Patrolling() {
        _rb.MovePosition(_rb.position + _direction * _speed * Time.fixedDeltaTime);
    }

    public void DirectionChange() {
        _direction = _direction == Vector3.back ? Vector3.forward : Vector3.back;
    }
}
