using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;
    private Vector2 Axis;

    [SerializeField]
    private float _moveSpeed = 3.0f;
    [SerializeField]
    private float _rotSpeed = 3.0f;

    [SerializeField]
    private GameObject _rotTarget = null;
    [SerializeField]
    private GameObject _posTarget = null;
    private 
    // Use this for initialization
    void Start()
    {
        Axis = Vector2.zero;
    }

    private void Rotate()
    {
        Axis.x = Input.GetAxis("Horizontal2");
        Axis.y = Input.GetAxis("Vertical2");
        if (Axis.x > 0.1f)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + _speed, transform.eulerAngles.z);
        }
        else if (Axis.x < -0.1f)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - _speed, transform.eulerAngles.z);
        }
        if (Axis.y > 0.1f)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x - _speed, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        else if(Axis.y < -0.1f)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x + _speed, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, _posTarget.transform.position, Time.deltaTime * _moveSpeed);
    }

    private void LockOn()
    {
        Vector3 vec = _rotTarget.transform.position - transform.position;
        var targetRotate = Quaternion.LookRotation(vec);
        var newRotate = Quaternion.Lerp(transform.rotation, targetRotate, Time.deltaTime * _rotSpeed).eulerAngles;
        transform.eulerAngles = new Vector3(newRotate.x, newRotate.y, newRotate.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        Rotate();
        Move();
        LockOn();
    }
}
