using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
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