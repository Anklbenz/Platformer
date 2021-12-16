using System;
using UnityEngine;

public sealed class Mushroom : ActiveInteractiveObject, IScoreNotify
{
    private const int SCORE_LIST_ELEMENT = 5;

    public event Action<IScoreNotify, int> ScoreNotifyEvent;
    public Vector3 Position => _collider.bounds.center;   

    [SerializeField] GameManager _gameManager;

    private void Start() {
        _gameManager.ScoreSystem.EventHandler.Subsribe(this);
    }
    private void OnDisable() {
        _gameManager.ScoreSystem.EventHandler.UnSubsribe(this);
    }

    private void OnTriggerEnter(Collider other) {
        var character = other.GetComponent<StateHandler>();
        if (character) {
            this.BounsTake(character);
            Destroy(gameObject);
        }
    }

    private void BounsTake(StateHandler character) {
        character.LevelUp();
        ScoreNotifyEvent?.Invoke(this, SCORE_LIST_ELEMENT);
    }

    public override void DownHit() {
        if (_patrol.isActiveAndEnabled)
            _patrol.DirectionChange();
    }

    protected override void Interaction(Collider other) { }
}
