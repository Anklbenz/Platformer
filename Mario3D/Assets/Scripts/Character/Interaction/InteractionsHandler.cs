using Character.States;
using Character.States.Data;
using Enums;
using Interfaces;
using UnityEngine;

namespace Character.Interaction
{
    public class InteractionsHandler
    {
        private const float IS_GROUNDED_BOX_INDENT = 0.95f;
        public bool IsGrounded => _isGrounded.InteractionBoxcast(Vector3.down);
        
        private const float OVERHEAD_BOX_INDENT = 0.93f;
        private readonly LegsInteractionsHandler _legsInteractionsHandler;
        private readonly HeadInteractionsHandler _headInteractionsHandler;
        private readonly Interacting _isGrounded;


        public InteractionsHandler(IStateData data, Collider collider, IMoveInfo moveInfo, IBounce bounce, float topCheckLength,
            LayerMask topCheckLayer, float bottomCheckLength, LayerMask bottomCheckLayer, float isGroundLength, LayerMask isGroundLayer){

            _headInteractionsHandler = new HeadInteractionsHandler(data, collider, moveInfo, Vector3.up, topCheckLength, topCheckLayer,
                OVERHEAD_BOX_INDENT);
            
            _legsInteractionsHandler = new LegsInteractionsHandler(collider, moveInfo, bounce, Vector3.down, bottomCheckLength, bottomCheckLayer);

            _isGrounded = new Interacting(collider, Axis.Vertical, isGroundLength, isGroundLayer, IS_GROUNDED_BOX_INDENT, true);
        }

        public void HeadInteractionCheck(){
            _headInteractionsHandler.CollisionCheck();
        }

        public void LegsInteractionsCheck(){
            _legsInteractionsHandler.CollisionCheck();
        }

        public void OnDrawGizmos(Color color){
            _headInteractionsHandler?.OnDrawGizmos(color);
            _legsInteractionsHandler?.OnDrawGizmos(color);
        }
    }
}