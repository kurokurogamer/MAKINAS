using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLobot : MonoBehaviour
{
	[SerializeField, Tooltip("ロボット")]
	private GameObject _robot;
	[SerializeField]
	private float _rotateSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.RotateAround(_robot.transform.position, Vector3.up, _rotateSpeed * Time.deltaTime);
	}
}
