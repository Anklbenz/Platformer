using Character.States;
using UnityEngine;

namespace Character.Interaction
{
    public class InteractorHandler
    {
        const float OVERHEAD_BOX_INDENT = 0.93f;

        private readonly BottomInteractionHandler _bottomInteractionHandler;
        private readonly OverheadInteractionHandler _topInteractionHandler;
        private readonly IMoveData _moveData;

        public InteractorHandler(StateSystem stateSystem, IMoveData moveData, float topCheckLength, LayerMask topCheckLayer,
            float bottomCheckLength, LayerMask bottomCheckLayer){
            _moveData = moveData;
            _topInteractionHandler = new OverheadInteractionHandler(stateSystem, _moveData, Vector3.up, topCheckLength,
                topCheckLayer, OVERHEAD_BOX_INDENT);
            _bottomInteractionHandler =
                new BottomInteractionHandler(_moveData, Vector3.down, bottomCheckLength, bottomCheckLayer);
        }

        public void Interaction(){
            _topInteractionHandler.CollisionCheck();
            _bottomInteractionHandler.CollisionCheck();
            _bottomInteractionHandler.IsGoundedReport(_moveData.IsGrounded);
        }

        public void OnDrawGizmos(){
            _topInteractionHandler?.OnDrawGizmos(Color.white);
            _bottomInteractionHandler?.OnDrawGizmos(Color.black);
        }
    }
}