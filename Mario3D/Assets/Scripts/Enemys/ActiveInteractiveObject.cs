using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveInteractiveObject : InteractiveObject
{
    protected Patrol _patrol;
    protected float patrolSpeed;
    protected BoxCollider _collider;
  
   protected virtual void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _patrol = GetComponent<Patrol>();
        patrolSpeed = _patrol._patrolSpeed;
   }
    public abstract void Drop();
}
