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
        Vector3 rot = transform.eulerAngles;
        switch (_type)
        {
            case ROTATE_TYPE.X:
                transform.rotation = Quaternion.Euler(_speed * Time.time, rot.y, rot.z);
                break;
            case ROTATE_TYPE.Y:
                transform.rotation = Quaternion.Euler(rot.x, _speed * Time.time, rot.z);
                break;
            case ROTATE_TYPE.Z:
                transform.rotation = Quaternion.Euler(rot.x, rot.y, _speed * Time.time);
                break;
            case ROTATE_TYPE.MAX:
                break;
            default:
                break;
        }
    }

}