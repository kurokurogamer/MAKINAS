using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRotate : MonoBehaviour
{
	[SerializeField, Tooltip("ポイント")]
	private GameObject _point = null;
	private GameObject _center;

	private Vector3 _firstRotate = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
		_firstRotate = _point.transform.eulerAngles;
		_center = Camera.main.GetComponent<LockOnSystem>()._centerPoint;
    }

	private void RotateX()
	{
		if (_center == null)
		{
			return;
		}

		Vector3 dt = _center.transform.position - _point.transform.position;

		float rad = Mathf.Atan2(dt.y, dt.z);
		_point.transform.LookAt(_center.transform);
		_point.transform.localRotation = Quaternion.Euler(new Vector3(-_point.transform.localEulerAngles.x, 0, 0));
	}

	// Update is called once per frame
	void Update()
    {
		RotateX();
    }
}
