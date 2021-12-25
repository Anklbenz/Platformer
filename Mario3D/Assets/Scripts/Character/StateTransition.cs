
using System.Collections;
using UnityEngine;

public class StateTransition : MonoBehaviour
{
    private const float FREQUENCY = 0.05f;
    [SerializeField] private MeshRenderer _meshRenderer;
    private int _flickCount;

    public void PlayFlick(float length) {       
        _flickCount = (int)(length / FREQUENCY);
        StartCoroutine(VisibleFlickerRoutine());
    }

    public void PlaySizeTransition(float length) {
        _flickCount = (int)(length / FREQUENCY);
        StartCoroutine(SizeFlickerRoutine());
    }

    private IEnumerator VisibleFlickerRoutine() {
        for (int i = 0; i < _flickCount; i++) {
            yield return new WaitForSeconds(FREQUENCY);
            _meshRenderer.enabled = _meshRenderer.enabled == false ? true : false;
        }
        _meshRenderer.enabled = true;
    }

    private IEnumerator SizeFlickerRoutine() {
        for (int i = 0; i < _flickCount; i++) {
            yield return new WaitForSeconds(FREQUENCY);
            _meshRenderer.enabled = true;
        }
    }


}
