using UnityEngine;
using System;

public sealed class Character : MonoBehaviour
{
    [SerializeField] private Vector3 smallSize = Vector3.one;
    [SerializeField] private Vector3 bigSize = new Vector3(1.3f, 1.6f, 1.3f);
    [SerializeField] private MeshRenderer mesh;
    public bool CanCrush { get; set; }

    public delegate void ScaleSchanged();
    public event ScaleSchanged OnScaleChangedEvent;

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
        if (isBig) {
            transform.localScale = bigSize;
            OnScaleChangedEvent?.Invoke();
        } else {
            transform.localScale = smallSize;
            OnScaleChangedEvent?.Invoke();
        }
    }

    public bool CompareCurrentStateWith<T>() where T : State {
        return _stateHandler.CompareCurrentStateWith<T>();
    }

    public void PlayFlick() {
        _flicker.Play();
    }
}
