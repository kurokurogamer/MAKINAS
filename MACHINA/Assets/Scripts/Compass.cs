using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
	private RectTransform _rectTransform;
	private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
		_rectTransform = GetComponent<RectTransform>();
		_camera = Camera.main;

	}

	private void Rotate()
	{
		_rectTransform.rotation = Quaternion.Euler(0, 0, -_camera.transform.eulerAngles.y - 10);
	}

    // Update is called once per frame
    void Update()
    {
		Rotate();
    }
}
