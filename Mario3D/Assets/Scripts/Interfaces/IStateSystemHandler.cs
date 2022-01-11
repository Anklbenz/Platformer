using UnityEngine;
public interface IStateSystemHandler
{
    Transform SkinsParent { get; }
    BoxCollider MainCollider { get; }
}
