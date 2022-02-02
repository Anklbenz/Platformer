using UnityEngine;

namespace Interfaces
{
    public interface IMoveData
    {
        bool MovingUp{ get; }
        bool MovingDown{ get; }
        bool IsGrounded{ get; }
        bool IsWallContact{ get; }
        bool IsSittingState{ get; }
        Vector3 MoveDirection{ get; }
    }
}