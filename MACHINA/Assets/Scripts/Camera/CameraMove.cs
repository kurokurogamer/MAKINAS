using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	[SerializeField]
	private float _speed = 1;
	private Vector2 _rightAxis;
	private Vector2 _leftAxis;

	[SerializeField]
	private float _moveSpeed = 1.0f;
	[SerializeField]
	private float _rotSpeed = 3.0f;

	[SerializeField]
	private GameObject _rotTarget = null;
	[SerializeField]
	private GameObject _posTarget = null;
	//[SerializeField]
	//private RectTransform _cursor = null;
	private Vector3 _firstPos = Vector3.zero;
	[SerializeField]
	private bool _LeftRightMode = false;
	[SerializeField]
	private Vector3 _offset = Vector3.zero;
	private void Awake()
	{
		//_firstPos = _cursor.position;
	}
	// Use this for initialization
	void Start()
	{
		_rightAxis = Vector2.zero;
		_leftAxis = Vector2.zero;
	}

	private void Rotate()
	{
		transform.RotateAround(_posTarget.transform.position, Vector3.up, _rightAxis.x * _rotSpeed);
		if(_rightAxis.x >= 0.1f && _leftAxis.x == 0&& _leftAxis.y == 0)
		{
			_LeftRightMode = false;
		}
		if (_rightAxis.x <= -0.1f && _leftAxis.x == 0 && _leftAxis.y == 0)
		{
			_LeftRightMode = true;
		}
	}

    private void Move()
    {
		if (_LeftRightMode)
		{
			_posTarget.transform.localPosition = new Vector3(-_offset.x, _offset.y, _offset.z);
			transform.position = Vector3.Lerp(transform.position, _posTarget.transform.position, Time.deltaTime * _speed);
		}
		else
		{
			_posTarget.transform.localPosition = _offset;
			transform.position = Vector3.Lerp(transform.position, _posTarget.transform.position, Time.deltaTime * _speed);
		}
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
		Debug.Log("処理中");
		_leftAxis.x = Input.GetAxis("Horizontal");
		_leftAxis.y = Input.GetAxis("Vertical");
		if (Input.GetKey(KeyCode.Alpha4))
		{
			_leftAxis.x = 1;
		}
		if (Input.GetKey(KeyCode.Alpha6))
		{
			_leftAxis.x = -1;
		}

		_rightAxis.x = Input.GetAxis("Horizontal2");
		_rightAxis.y = Input.GetAxis("Vertical2");
		if (Input.GetKey(KeyCode.Alpha3))
		{
			_rightAxis.x = 1;
		}
		if (Input.GetKey(KeyCode.Alpha1))
		{
			_rightAxis.x = -1;
		}
	}

	private void LateUpdate()
    {
        LockOn();
    }
	private void FixedUpdate()
	{
		Rotate();
		Move();
	}
}
