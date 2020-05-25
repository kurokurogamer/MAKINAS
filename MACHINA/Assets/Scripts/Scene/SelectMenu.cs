using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenu : MonoBehaviour
{
	// メニュー一覧
	private enum Menu
	{
		SINGLE,
		MULTI,
		GARAGE,
		OPTION,
		MAX
	}
	[SerializeField, Tooltip("選択時SE")]
	private AudioClip _clip = null;
	[SerializeField, Tooltip("決定時SE")]
	private AudioClip _clip2 = null;
	[SerializeField, Tooltip("カーソル")]
	private RectTransform _cursor;
	[SerializeField, Tooltip("メニューUI")]
	private GameObject _ui = null;
	[SerializeField, Tooltip("ミッションUI")]
	private GameObject _ui2 = null;

	private List<RectTransform> _uiList;
	// 長押し時のメニュー
	[SerializeField, Tooltip("長押しの遅れ")]
	private float _delay = 1.0f;
	[SerializeField, Tooltip("間隔")]
	private float _interval = 0.2f;
	private float _nowTime;
	private float _nowTimeDelay;
	// 入力情報""
	private Vector2 _axis;
	// 前回の入力情報
	private Vector2 _oldAxis;

	private int _menuType;

    // Start is called before the first frame update
    void Start()
    {
		_uiList = new List<RectTransform>();
		foreach(RectTransform child in transform)
		{
			if(child.TryGetComponent(out RectTransform rect))
			{
				_uiList.Add(rect);
			}
		}
		_menuType = 0;
		_nowTime = 0;
		_nowTimeDelay = 0;
		_axis = Vector2.zero;
		_oldAxis = Vector2.zero;
    }

	private void Seletct()
	{
		_oldAxis = _axis;
		_axis.x = Input.GetAxis("Horizontal");
		_axis.y = Input.GetAxis("Vertical");
		// 入力があった場合時間を計測する
		if(_axis.y > 0 || _axis.y < 0)
		{
			if(_nowTimeDelay <= 0)
			{
				_nowTime = _interval;
			}
			_nowTimeDelay += Time.deltaTime;
		}
		else
		{
			_nowTimeDelay = 0;
		}

		if(_nowTimeDelay > _delay)
		{
			_nowTime += Time.deltaTime;
		}

		if (_nowTime >= _interval)
		{
			Debug.Log("処理");
			AudioManager.instance.PlaySE(_clip);
			if (_axis.y < 0)
			{
				_menuType++;
				if(_menuType > _uiList.Count - 1)
				{
					_menuType = 0;
				}
			}
			else if (_axis.y > 0)
			{
				_menuType--;
				if (_menuType < 0)
				{
					_menuType = _uiList.Count - 1;
				}
			}
			_nowTime = 0;
		}
		_cursor.position = _uiList[_menuType].position;
	}

	private void Check()
	{
		if (Input.GetButtonDown("Fire2"))
		{
			AudioManager.instance.PlaySE(_clip2);
			switch ((Menu)_menuType)
			{
				case Menu.SINGLE:
					_ui.SetActive(false);
					_ui2.SetActive(true);
					break;
				case Menu.MULTI:
					break;
				case Menu.GARAGE:
					break;
				case Menu.OPTION:
					break;
				case Menu.MAX:
				default:
					Debug.Log("存在しないメニュー");
					break;
			}
		}
	}


	// Update is called once per frame
	void Update()
    {
		Seletct();
		Check();
    }
}
