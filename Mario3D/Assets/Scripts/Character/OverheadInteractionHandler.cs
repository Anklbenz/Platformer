using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadInteractionHandler 
{
    private Character _character;

    //private void BrickHitTest() {
    //    if (_isBrickHit) return;

    //    BrickBox brick = _rayHit.transform.GetComponentInParent<BrickBox>();
    //    if (brick != null) {
    //        _isBrickHit = true;
    //        brick.BrickHit(_character);
    //    }
    //}

    public void Interaction(Collider[] overheadColliders, Vector3 patrentCenter) {
 
        float minValue = 0;
Debug.Log(overheadColliders.Length);
        foreach (var collider in overheadColliders) {
            //if minValue = collider.ClosestPoint(patrentCenter).z;
            
            Debug.Log($"ближайшая точка к центру {collider.ClosestPoint(patrentCenter)} имя {collider.name} --- центр {patrentCenter}");
            //IJumpOn instance = collider.transform.GetComponentInParent<IJumpOn>();

            //if (instance != null) {
            //    if (instance.DoBounce == true)
            //        bounce = true;
            //    instance.JumpOn(patrentCenter);
            //}
        }
     
    }



}
