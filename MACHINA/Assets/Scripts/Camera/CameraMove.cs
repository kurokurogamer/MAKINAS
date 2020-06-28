using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	[SerializeField]
	private float _speed = 1;
	private Vector2 Axis;

	[SerializeField]
	private float _moveSpeed = 1.0f;
	[SerializeField]
	private float _rotSpeed = 3.0f;

	[SerializeField]
	private GameObject _rotTarget = null;
	[SerializeField]
	private GameObject _posTarget = null;
	[SerializeField]
	private RectTransform _cursor = null;
	private Vector3 _firstPos = Vector3.zero;
	[SerializeField]
	private Vector3 _point = Vector3.zero;
	private void Awake()
	{
		_firstPos = _cursor.position;
	}
	// Use this for initialization
	void Start()
	{
		Axis = Vector2.zero;
	}

	private void Rotate()
	{
		Axis.x = Input.GetAxis("Horizontal2");
		Axis.y = Input.GetAxis("Vertical2");
		transform.RotateAround(_posTarget.transform.position, Vector3.up, Axis.x * _rotSpeed);
		Vector3 pos = Vector3.zero;
		if(Axis.x > 0.1f)
		{
			pos += new Vector3(50, 0, 0);
		}
		else if (Axis.x < -0.1f)
		{
			pos += new Vector3(-50, 0, 0);
		}
		if (Axis.y > 0.5f)
		{
			pos += new Vector3(0, 50, 0);
		}
		else if (Axis.y < -0.5f)
		{
			pos += new Vector3(0, -50, 0);
		}

		_cursor.position = Vector3.Lerp(_cursor.position, _firstPos + pos, Time.deltaTime * 10);
	}

    private void Move()
    {
		//transform.position = _posTarget.transform.position + _point;
		transform.position = Vector3.Lerp(transform.position, _posTarget.transform.position, Time.deltaTime * _moveSpeed);
		//transform.position = Vector3.MoveTowards(transform.position, _posTarget.transform.position, Time.deltaTime * _moveSpeed);
	}

	private void LockOn()
    {
        Vector3 vec = _rotTarget.transform.position - transform.position;
        var targetRotate = Quaternion.LookRotation(vec);
		//var newRotate = Quaternion.Lerp(transform.rotation, targetRotate, Time.deltaTime * _rotSpeed).eulerAngles;
		//transform.eulerAngles = new Vector3(newRotate.x, newRotate.y, newRotate.z);
	}

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        //Move();
        LockOn();
    }
	private void FixedUpdate()
	{
		Rotate();

		Move();
	}
}
