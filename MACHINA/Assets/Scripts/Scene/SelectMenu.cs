using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenu : MonoBehaviour
{
	[SerializeField, Tooltip("カーソル")]
	private RectTransform _cursor;
	[SerializeField, Tooltip("UIリスト")]
	private List<RectTransform> _uiList = new List<RectTransform>();
	private int _ID;
	[SerializeField, Tooltip("UI1")]
	private GameObject _ui;
	[SerializeField, Tooltip("UI2")]
	private GameObject _ui2;
	// 入力情報""
	private Vector2 _axis;
	// 前回の入力情報
	private Vector2 _oldAxis;
    // Start is called before the first frame update
    void Start()
    {
		_cursor = GetComponent<RectTransform>();
		_axis = Vector2.zero;
		_oldAxis = Vector2.zero;
		
    }

	private void Seletct()
	{
		_oldAxis = _axis;
		_axis.x = Input.GetAxis("Horizontal");
		_axis.y = Input.GetAxis("Vertical");

		if (_axis.y < 0 && _oldAxis.y == 0)
		{
			if (_ID < _uiList.Count - 1)
			{
				_ID++;
			}
		}
		else if (_axis.y > 0 && _oldAxis.y == 0)
		{
			if (_ID > 0)
			{
				_ID--;
			}
		}
		_cursor.position = _uiList[_ID].position;
	}

	private void Check()
	{
		if(_ID != 0)
		{
			return;
		}
		if(Input.GetButtonDown("Fire2"))
		{
			_ui.SetActive(false);
			_ui2.SetActive(true);
		}
	}

	// Update is called once per frame
	void Update()
    {
		Seletct();
		Check();
    }
}
