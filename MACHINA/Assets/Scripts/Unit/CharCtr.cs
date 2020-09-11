using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCtr : MonoBehaviour
{
	[SerializeField]
	private float _speed = 5f;
	private Rigidbody _rigid = null;

	private Animator _animator = null;

	private Vector2 _stickRight = Vector2.zero;
	private Vector2 _stickLeft = Vector2.zero;

	// Start is called before the first frame update
	void Start()
	{
		_rigid = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();
	}

	private void InputSet()
	{
		Vector3 velocity = Vector3.zero;

		// コントローラーの入力を受け取る
		_stickRight.x = Input.GetAxis("Horizontal2");
		_stickRight.y = Input.GetAxis("Vertical2");

		if (_stickRight.x > 0)
		{
			velocity += Vector3.right * _speed;
		}
		else if (_stickRight.x < 0)
		{
			velocity -= Vector3.right * _speed;
		}
		if (_stickRight.y > 0)
		{
			velocity += Vector3.forward * _speed;
		}
		else if (_stickRight.y < 0)
		{
			velocity -= Vector3.forward * _speed;
		}

		if (_stickRight.x != 0 || _stickRight.y != 0)
		{
			_animator.SetFloat("Speed", 1.0f);

			Vector2 dt = new Vector2(velocity.z, velocity.x) - Vector2.zero;
			float rad = Mathf.Atan2(dt.y, dt.x);
			float degree = rad * Mathf.Rad2Deg;

			transform.rotation = Quaternion.Euler(0, degree, 0);
			Debug.Log(degree);
		}
		else
		{
			_animator.SetFloat("Speed", 0.0f);
		}

		_rigid.velocity = velocity;
	}

	// Update is called once per frame
	void Update()
	{
		InputSet();
	}
}
