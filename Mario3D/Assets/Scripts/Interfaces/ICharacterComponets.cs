using UnityEngine;

namespace Interfaces
{
    public interface ICharacterComponets
    {
        Transform SkinsParent{ get; }
        CapsuleCollider MainCollider{ get; }
        Rigidbody MainRigidbody{ get; }
    }
}
