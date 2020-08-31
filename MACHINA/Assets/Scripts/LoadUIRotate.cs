using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUIRotate : MonoBehaviour
{
    private enum ROTATE_TYPE
    {
        X,
        Y,
        Z,
        MAX
    }
    [SerializeField]
    private ROTATE_TYPE _type = ROTATE_TYPE.X;
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private float _secondTime = 1.0f;
    private float _nowTime = 0;

    private void OnEnable()
    {
        _nowTime = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _nowTime += Time.deltaTime;
        if(_nowTime < _secondTime)
        {
            return;
        }
        _nowTime = 0;
        switch (_type)
        {
            case ROTATE_TYPE.X:
                transform.Rotate(_speed, 0, 0);
                break;
            case ROTATE_TYPE.Y:
                transform.Rotate(0, _speed, 0);
                break;
            case ROTATE_TYPE.Z:
                transform.Rotate(0, 0, _speed);
                break;
            case ROTATE_TYPE.MAX:
                break;
            default:
                break;
        }
    }
}
