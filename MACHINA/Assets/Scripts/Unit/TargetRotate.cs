using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRotate : MonoBehaviour
{
	private GameObject _player;
	[SerializeField, Tooltip("ポイント")]
	private GameObject _point = null;
	[SerializeField, Tooltip("ポイント2")]
	private GameObject _secondPoint = null;
	private GameObject _center;
	private float _degree;
	private float _degree2;

	private Vector3 _firstRotate = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
		_player = null;
		_degree = _point.transform.eulerAngles.y;
		_firstRotate = _point.transform.eulerAngles;
		_center = Camera.main.GetComponent<LockOnSystem>()._centerPoint;
    }

	private void RotateY()
	{
		if(_player == null)
		{
			return;
		}

		Vector3 dt = _center.transform.position - transform.position;

		float rad = Mathf.Atan2(dt.x, dt.z);
		_degree = rad * Mathf.Rad2Deg;
		_point.transform.LookAt(_center.transform);
		_point.transform.localRotation = Quaternion.Euler(new Vector3(0, _point.transform.localEulerAngles.y - 90, 0));
		//_point.transform.localRotation = Quaternion.Euler(0, _degree - 90, 0);
	}

	private void RotateX()
	{
		if (_player == null)
		{
			return;
		}

		Vector3 dt = _center.transform.position - _secondPoint.transform.position;

		float rad = Mathf.Atan2(dt.y, dt.z);
		_degree2 = rad * Mathf.Rad2Deg;
		_secondPoint.transform.LookAt(_center.transform);
		_secondPoint.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -_secondPoint.transform.localEulerAngles.x));
		//_secondPoint.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, _degree2) - transform.eulerAngles);
	}


	// Update is called once per frame
	void Update()
    {
		RotateY();
		RotateX();
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			_player = other.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			_player = null;
		}
	}
}
