using UnityEngine;
using System;

public class ActiveEnemy : ActiveInteractiveObject, IJumpOn, IScoreMessage
{
    public event Action<IScoreMessage, int> ScoreEvent;

    [Header("ActiveEnemy")]
    [SerializeField] protected float _timeToDestroyAfterDrop = 0f;
    [SerializeField] protected GameManager gameManager;

    protected Rigidbody _rbody;
    public bool DoDamage { get; set; } = true;
    public bool DoBounce { get; set; } = true;
    public Vector3 Position { get => _collider.bounds.center; }

    protected override void Awake() {
        base.Awake();
        _rbody = GetComponent<Rigidbody>();
    }

    private void Start() {
        base.OnEnemyFrontCollisionEvent += OnEnemyFrontCollision;
        gameManager.ScoreManager.EventHandler.Subsribe(this);
    }
    private void OnDisable() {
        base.OnEnemyFrontCollisionEvent -= OnEnemyFrontCollision;
        gameManager.ScoreManager.EventHandler.Subsribe(this);
    }

    protected override void Interaction(Collider other) {
        if (DoDamage)
            other.GetComponent<StateHandler>()?.Hurt();
    }

    public virtual void JumpOn(Vector3 center, int InRowJumpCount) {
        DoDamage = false;
        DoBounce = false;
        _patrol.SetActive(false);

        SendScore(InRowJumpCount);
    }

    public void Drop() {
        _collider.enabled = false;
        _rbody.constraints = RigidbodyConstraints.None;
        _rbody.AddForceAtPosition(Vector3.up * 30, _rbody.position * UnityEngine.Random.Range(50, 100), ForceMode.Impulse);
        Destroy(gameObject, _timeToDestroyAfterDrop);
    }

    public override void DownHit() {
        SendScore(0);
        Drop();
    }

    protected virtual void OnEnemyFrontCollision(ActiveEnemy ae) {
        base._patrol.DirectionChange();
    }

    public void SendScore(int InRowCount) {
        ScoreEvent?.Invoke(this, InRowCount);
    }
}
