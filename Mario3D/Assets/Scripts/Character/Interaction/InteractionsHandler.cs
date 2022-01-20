using Character.States;
using Interfaces;
using UnityEngine;

namespace Character.Interaction
{
    public class InteractionsHandler
    {
        const float OVERHEAD_BOX_INDENT = 0.93f;
        private readonly LegsInteractionsHandler _legsInteractionsHandler;
        private readonly HeadInteractionsHandler _headInteractionsHandler;

        public InteractionsHandler(StateSystem stateSystem, IMove move, IBounce bounce, float topCheckLength,
            LayerMask topCheckLayer, float bottomCheckLength, LayerMask bottomCheckLayer){

            var moveData = move;
            _headInteractionsHandler = new HeadInteractionsHandler(stateSystem, moveData, Vector3.up, topCheckLength,
                topCheckLayer, OVERHEAD_BOX_INDENT);

            _legsInteractionsHandler =
                new LegsInteractionsHandler(moveData, bounce, Vector3.down, bottomCheckLength, bottomCheckLayer);
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