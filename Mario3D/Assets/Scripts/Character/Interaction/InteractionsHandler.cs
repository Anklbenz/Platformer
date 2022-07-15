using Enums;
using Interfaces;
using UnityEngine;

namespace Character.Interaction
{
    public class InteractionsHandler
    {
        private const float IS_GROUNDED_BOX_INDENT = 0.999f;
        private const float IS_WALL_BOX_INDENT = 0.99f;
        private const float OVERHEAD_BOX_INDENT = 0.93f;
        public bool IsGrounded => _isGrounded.InteractionBoxcast(Vector3.down);
        public bool IsWall => _isWall.InteractionBoxcast(_moveData.MoveDirection);
        
        private readonly LegsInteractionsHandler _legsInteractionsHandler;
        private readonly HeadInteractionsHandler _headInteractionsHandler;
        private readonly Interacting _isGrounded, _isWall;
        private readonly IMoveData _moveData;

        public InteractionsHandler(IStateData data, Collider collider, IMoveData moveData, IBounce bounce, float topCheckLength,
            LayerMask topCheckLayer, float bottomCheckLength, LayerMask bottomCheckLayer, float isGroundLength, LayerMask isGroundLayer){

            _headInteractionsHandler = new HeadInteractionsHandler(data, collider, moveData, Vector3.up, topCheckLength, topCheckLayer,
                OVERHEAD_BOX_INDENT);

            _legsInteractionsHandler =
                new LegsInteractionsHandler(collider, moveData, bounce, Vector3.down, bottomCheckLength, bottomCheckLayer);
            _isGrounded = new Interacting(collider, Axis.Vertical, isGroundLength, isGroundLayer, IS_GROUNDED_BOX_INDENT, true);
            _isWall = new Interacting(collider, Axis.Horizontal, isGroundLength, isGroundLayer, IS_WALL_BOX_INDENT, true);

            _moveData = moveData;
        }

        public void HeadInteractionCheck(){
            _headInteractionsHandler.CollisionCheck();
        }

        public void LegsInteractionsCheck(){
            _legsInteractionsHandler.CollisionCheck();
        }

        public void OnDrawGizmos(Color color){
            _isWall?.OnDrawGizmos(color);
            _isGrounded?.OnDrawGizmos(color);
            _headInteractionsHandler?.OnDrawGizmos(color);
            _legsInteractionsHandler?.OnDrawGizmos(color);
        }
    }
}