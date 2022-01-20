using UnityEngine;

namespace Interfaces
{
    public interface IMove
    {
        CapsuleCollider MainCollider { get; }
        bool MovingUp { get; }
        bool MovingDown { get; }
        bool IsGrounded { get; }
       // void Bounce();
    }
}