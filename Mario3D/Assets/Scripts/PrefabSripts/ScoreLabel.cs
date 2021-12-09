using UnityEngine;
using UnityEngine.UI;

public sealed class ScoreLabel : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _aliveTime;
    private float timer;
    private Text _scoreTextLabel;
    private Camera cam;
    private RectTransform _rect;


    private void Awake() {
        _scoreTextLabel = GetComponent<Text>();
       
        cam = Camera.main;
    }

    private void OnEnable() {
        timer = _aliveTime;
    }

    private void FixedUpdate() {
        //transform.position = cam.WorldToScreenPoint(transform.position);
     
        transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
            gameObject.SetActive(false);
    }

    public void Text(string str) {
        _scoreTextLabel.text = str;
    }
}


