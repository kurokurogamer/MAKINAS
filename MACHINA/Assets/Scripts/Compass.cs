using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
	private RectTransform _rectTransform;
	private Camera _camera;
	private float _first;
    // Start is called before the first frame update
    void Start()
    {
		_rectTransform = GetComponent<RectTransform>();
		_camera = Camera.main;
		_first = transform.eulerAngles.z;
	}

	private void Rotate()
	{
		_rectTransform.rotation = Quaternion.Euler(0, 0, -_camera.transform.eulerAngles.y + _first);
	}

    // Update is called once per frame
    void Update()
    {
		Rotate();
    }
}
