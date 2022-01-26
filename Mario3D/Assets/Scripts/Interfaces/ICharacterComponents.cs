using UnityEngine;

namespace Interfaces
{
    public interface ICharacterComponents
    {
        Transform MainTransform{ get; }
        CapsuleCollider MainCollider{ get; }
        Rigidbody MainRigidbody{ get; }
        Transform SkinsParent{ get; }
    }
}
