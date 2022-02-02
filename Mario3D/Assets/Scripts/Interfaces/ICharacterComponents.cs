using Character.States;
using UnityEngine;

namespace Interfaces
{
    public interface ICharacterComponents
    {
        Transform MainTransform{ get; }
        BoxCollider MainCollider{ get; }
        SphereCollider SecondaryCollider{ get; }
        Rigidbody MainRigidbody{ get; }
        Transform SkinsParent{ get; }
    }
}
