using UnityEngine;

public interface IMoveData
{
    BoxCollider InteractCollider { get; }
    bool MovingUp { get; }
    bool MovingDown { get; }
    bool IsGrounded { get; }
    void Bounce();
}