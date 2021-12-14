using UnityEngine;

public sealed class Mushroom : ActiveInteractiveObject
{
    protected override void Interaction(Collider other) {
        other.GetComponent<StateHandler>()?.LevelUp();
        Destroy(gameObject);
    }

    public override void DownHit() {
        if(_patrol.isActiveAndEnabled)
            _patrol.DirectionChange();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<Character>(out Character character))
            Interaction(other);
    }
}
