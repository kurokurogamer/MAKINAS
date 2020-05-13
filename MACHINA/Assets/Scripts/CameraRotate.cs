using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField, Tooltip("追従するべきオブジェクト")]
    private GameObject _followTarget = null;
    [SerializeField]
    private float _clampAngle = 60f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newRot = transform.eulerAngles;

        newRot.x -= newRot.x > 180 ? 360 : 0;
        newRot.x = Mathf.Abs(newRot.x) > _clampAngle ? _clampAngle * Mathf.Sign(newRot.x) : newRot.x;
        transform.eulerAngles = new Vector3(newRot.x, _followTarget.transform.eulerAngles.y, 0);
    }
}
