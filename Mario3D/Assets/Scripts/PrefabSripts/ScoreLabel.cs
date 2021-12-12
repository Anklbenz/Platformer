using UnityEngine;
using UnityEngine.UI;

public sealed class ScoreLabel : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _aliveTime;
    private float timer;
    private Text _scoreTextLabel;

    private void Awake() {
        _scoreTextLabel = GetComponent<Text>();
    }

    private void OnEnable() {
        this.ResetTimer();
    }

    private void FixedUpdate() {
        this.Move();
        this.DisableTimer();
    }

    private void Move() {
        transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);
    }

    public void Text(string str) {
        _scoreTextLabel.text = str;
    }

    private void DisableTimer() {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
            this.Disable();
    }

    private void Disable() {
        gameObject.SetActive(false);
    }

    private void ResetTimer() {
        timer = _aliveTime;
    }
}

