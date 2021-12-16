
using System;
using UnityEngine;

public sealed class Flower : MonoBehaviour, IScoreNotify
{
    private const int SCORE_LIST_ELEMENT = 5;
    public event Action<IScoreNotify, int> ScoreNotifyEvent;

    [SerializeField] private GameManager _gameManager;

    public Vector3 Position => transform.position;

    private void OnEnable() {
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
}
