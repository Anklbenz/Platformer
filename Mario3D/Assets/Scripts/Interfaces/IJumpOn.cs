using UnityEngine;
interface IJumpOn
{
    bool DoBounce { get; set; }
    void JumpOn(Vector3 senderCenter);
}