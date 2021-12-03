//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FireballMove : MonoBehaviour
//{
//    private float _time = 0;
//    private float _amplitude = 1f; // высота
//    private float _frecuncy = 8; // ширина волны
//    private float _offcet = 0;
//    private float magnitude = 0.5f;
//    private Vector3 _startPos;
//    private float speed = 6;



//    private void Start()
//    {
//        _startPos = transform.position;

//    }

//    private void FixedUpdate()
//    {

//        _time += Time.deltaTime;
//        _offcet = _amplitude * Mathf.Sin(_time * _frecuncy);
//        _startPos += Vector3.forward *Time.deltaTime * speed;
//        transform.position = _startPos + new Vector3(0, _offcet, 0);
//    }
//}
