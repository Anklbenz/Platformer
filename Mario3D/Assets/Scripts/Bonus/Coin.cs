using System;
using UnityEngine;

public sealed class Coin : MonoBehaviour, IScoreNotify
{
    private const int SCORE_LIST_ELEMENT = 0;
    public event Action<IScoreNotify, int> ScoreNotifyEvent;

    [SerializeField] private float _destroyTime;
    [SerializeField] private GameManager _gameManager;

    public Vector3 Position => transform.position;
   
    private void Awake() {
        Destroy(this.gameObject, _destroyTime);
    }

    private void OnEnable() {
        _gameManager.ScoreSystem.EventHandler.Subsribe(this);
    }
    private void OnDisable() {
        this.BonusTake();
        _gameManager.ScoreSystem.EventHandler.UnSubsribe(this);
    }

    private void BonusTake() {
        ScoreNotifyEvent?.Invoke(this, SCORE_LIST_ELEMENT);
    }
}