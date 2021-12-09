using UnityEngine;

public sealed class Character : MonoBehaviour
{
    private readonly Vector3 SMALL_SIZE = Vector3.one;
    private readonly Vector3 BIG_SIZE = new Vector3(1.3f, 1.6f, 1.3f);
   
    public bool CanCrush { get; set; }

    private FireBallSpawner _ballSpawner;
    private StateHandler _stateHandler;
    private Flicker _flicker;

    private void Awake() {
        _ballSpawner = GetComponent<FireBallSpawner>();
        _stateHandler = GetComponent<StateHandler>();
        _flicker = GetComponent<Flicker>();
    }

    public void BallSpawnerSetActive(bool state) {
        _ballSpawner.isActive = state;
    }

    public void SetTrasformSize(bool isBig) {
        transform.localScale = isBig ? BIG_SIZE : SMALL_SIZE;
    }

    public bool CompareCurrentStateWith<T>() where T : State {
        return _stateHandler.CompareCurrentStateWith<T>();
    }

    public void PlayFlick() {
        _flicker.Play();
    }
}