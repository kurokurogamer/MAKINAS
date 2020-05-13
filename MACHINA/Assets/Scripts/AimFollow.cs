using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimFollow : MonoBehaviour
{
    [SerializeField, Tooltip("追従するべき原点")]
    private GameObject _followTarget = null;
    // 相対位置
    private Vector3 _offset;
    // Use this for initialization
    void Start()
    {
        _offset = transform.position - _followTarget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _followTarget.transform.position + _offset;
    }
}
