
using UnityEngine;

public sealed class Mushroom : ActiveInteractiveObject
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
            Interaction(other);
    }

    protected override void Interaction(Collider other) {
        // other.GetComponent<Character>()?.LevelUp();
        other.GetComponent<StateHandler>()?.LevelUp();
        Destroy(gameObject);
    }

    public override void Drop() {
        if(_patrol.isActiveAndEnabled)
            _patrol.DirectionChange();
    }


}
