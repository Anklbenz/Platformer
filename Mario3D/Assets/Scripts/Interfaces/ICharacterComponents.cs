using UnityEngine;

namespace Interfaces
{
    public interface ICharacterComponents
    {
        Transform MainTransform{ get; }
        Rigidbody MainRigidbody{ get; }
        CapsuleCollider MainCollider{ get; }
        Transform SkinsParent{ get; }
    }
}
