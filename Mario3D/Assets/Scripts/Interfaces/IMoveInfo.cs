using UnityEngine;

namespace Interfaces
{
    public interface IMoveInfo
    {
        bool MovingUp { get; }
        bool MovingDown { get; }
        bool IsGrounded { get; }
    }
}