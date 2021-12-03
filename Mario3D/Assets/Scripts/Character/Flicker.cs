using UnityEngine;
//InvisibleForEnemy
public class Flicker : MonoBehaviour {

    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private float _playTime;
    [SerializeField] private string _defaultLayer;
    [SerializeField] private string _invisibeForEnemyLayer;
    private bool _isActive = false;
    
    private float i = 0;
    private float _timeRemaning;


    public void Play() {
        _isActive = true;
        _timeRemaning = _playTime;
        SetLayer(LayerMask.NameToLayer(_invisibeForEnemyLayer));
    }

    private void Flick() {
        i++; // итератор для метода PingPong 
        float f = Mathf.PingPong(i, 2f);

        if (f == 0)
            _mesh.enabled = false;
        else if (f == 1)
            _mesh.enabled = true;

        if (_timeRemaning <= 0) {
            _isActive = false;
            _mesh.enabled = true;            
            SetLayer(LayerMask.NameToLayer(_defaultLayer));
        }
        _timeRemaning -= Time.deltaTime;
    }

    private void FixedUpdate() {
        if (_isActive)
            Flick();
    }

    private void SetLayer(int layer) {
        gameObject.layer = layer;
        Transform[] gObjects = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform trn in gObjects)
            trn.gameObject.layer = layer;
    }
}
