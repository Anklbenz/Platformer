using UnityEngine;
public interface IJumpOn
{
    bool DoBounce { get; set; }
    void JumpOn(Vector3 senderCenter = default(Vector3));
}