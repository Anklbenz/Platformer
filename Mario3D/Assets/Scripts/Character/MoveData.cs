using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MoveData : ScriptableObject
{
    [Header("Jump")]
    [SerializeField] private float _maxJumpForceDuration = 0.15f;

    [SerializeField] private float _addForceStep = 3.67f;
    [SerializeField] private float _bouncePower = 5f;
    [SerializeField] private LayerMask _isGroundedLayer;
    [SerializeField] private LayerMask _wallCheckLayer;

    public float MaxJumpForceDuration => _maxJumpForceDuration;

    public float AddForceStep => _addForceStep;

    public float BouncePower => _bouncePower;


    [Header("Walk")]
    [SerializeField] private float _maxWalkSpeed = 9;

    [SerializeField] private float _walkForceStep = 1.1f;
    [SerializeField] public float _minActionSpeed = 0.3f;

    public float MaxWalkSpeed => _maxWalkSpeed;

    public float WalkForceStep => _walkForceStep;

    [Header("ExtraWalk")]
    [SerializeField] private float _maxExtraWalkSpeed = 9;
    [SerializeField] private float _extraWalkForceStep = 1.1f;

    public float MaxExtraWalkSpeed => _maxExtraWalkSpeed;

    public float ExtraWalkForceStep => _extraWalkForceStep;
}