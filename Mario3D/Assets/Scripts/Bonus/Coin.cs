using UnityEngine;

public sealed class Coin : MonoBehaviour
{
    [SerializeField] private float _destroyTime;

    private void Awake() {
        Destroy(this.gameObject, _destroyTime);
    }
}
