using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRotate : MonoBehaviour
{
	private GameObject _player;
	[SerializeField, Tooltip("ポイント")]
	private GameObject _point = null;
	private float _degree;
	private float _degree2;

	private Vector3 _firstRotate = Vector3.zero;
	[SerializeField, Tooltip("ポイント2")]
	private GameObject _secondPoint = null;
    // Start is called before the first frame update
    void Start()
    {
		_player = null;
		_degree = _point.transform.eulerAngles.y;
		_firstRotate = _point.transform.eulerAngles;
    }

	private void RotateY()
	{
		if(_player == null)
		{
			return;
		}

		Vector3 dt = _player.transform.position - transform.position;

		float rad = Mathf.Atan2(dt.x, dt.z);
		_degree = rad * Mathf.Rad2Deg;
	}

	private void RotateX()
	{
		if (_player == null)
		{
			return;
		}

		Vector3 dt = _player.transform.position - transform.position;

		float rad = Mathf.Atan2(dt.y, dt.z);
		_degree2 = rad * Mathf.Rad2Deg;
	}


	// Update is called once per frame
	void Update()
    {
		RotateY();
		RotateX();
		_point.transform.localRotation = Quaternion.Euler(0, _degree - 90, 0);
		if(_degree2 > 90)
		{
			_degree2 -= 180;
		}
		_secondPoint.transform.localRotation = Quaternion.Euler(0, 0, _degree2);

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
