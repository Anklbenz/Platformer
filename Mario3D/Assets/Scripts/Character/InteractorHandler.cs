using UnityEngine;

public class InteractorHandler : MonoBehaviour
{
    const float OVERHEAD_BOX_INDENT = 0.93f;
    [SerializeField] private float _topInteractionLength;
    [SerializeField] private LayerMask _topInteractionLayer;

    [SerializeField] private float _bottomInteractionLength;
    [SerializeField] private LayerMask _bottomInteractionLayer;

    private OverheadInteractionHandler _topInteractionHandler;
    public BottomInteractionHandler _bottomInteractionHandler;

    private Character _character;
    private IMoveData _moveData;  

    private void Awake() {
        _character = GetComponent<Character>();
        _moveData = GetComponent<IMoveData>();

        _topInteractionHandler = new OverheadInteractionHandler(_character, _moveData, Vector3.up, _topInteractionLength, _topInteractionLayer, OVERHEAD_BOX_INDENT);
        _bottomInteractionHandler = new BottomInteractionHandler(_moveData, Vector3.down, _bottomInteractionLength, _bottomInteractionLayer);         
    }

    private void FixedUpdate() {
        _topInteractionHandler.CollisionCheck();
        _bottomInteractionHandler.CollisionCheck();
        _bottomInteractionHandler.IsGoundedReport(_moveData.IsGrounded);
    }

    private void OnDrawGizmos() {
         _topInteractionHandler?.OnDrawGizmos( Color.white);
        _bottomInteractionHandler?.OnDrawGizmos( Color.black);      
    }
}



