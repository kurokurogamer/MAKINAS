using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLobot : MonoBehaviour
{
	[SerializeField, Tooltip("ロボット")]
	private GameObject _robot;
	[SerializeField]
	private float _rotateSpeed = 1.0f;
    [SerializeField]
    private int _pointID = 0;
    [SerializeField]
    private List<GameObject> _pointList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Rotate()
    {
        transform.RotateAround(_robot.transform.position, Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    private void Point()
    {
        if(_pointID >= 3)
        {
            return;
        }
        // カメラ座標の切り替え
        transform.position = Vector3.MoveTowards(transform.position, _pointList[_pointID].transform.position, Time.deltaTime * 5f);
        // カメラ角度の切り替え
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _pointList[_pointID].transform.rotation, Time.deltaTime * 20);

    }

    // Update is called once per frame
    void Update()
    {
        Point();
    }
}
