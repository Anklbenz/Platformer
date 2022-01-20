using UnityEngine;

namespace Interfaces
{
    public interface IStateSystemHandler
    {
        Transform SkinsParent { get; }
        CapsuleCollider MainCollider { get; }

    }
}
