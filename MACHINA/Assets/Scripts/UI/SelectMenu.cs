using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenu : MonoBehaviour
{
	[SerializeField, Tooltip("テキストエリア"), TextArea(2, 5)]
	private string[] _str;

	// 操作時音声
	[SerializeField, Tooltip("操作時SE")]
	private AudioClip _clip = null;
	[SerializeField, Tooltip("決定時SE")]
	private AudioClip _clip2 = null;
	[SerializeField, Tooltip("カーソル")]
	protected RectTransform _cursor = null;
	[SerializeField]
	protected GameObject _backUI;
	protected GameObject _startUI = null;

	[SerializeField, Tooltip("有効にするUIのリスト")]
	private List<GameObject> _uiList = null;
	private List<RectTransform> _menuList;

	[SerializeField, Tooltip("長押しの遅れ")]
	private float _delay = 1.0f;
	[SerializeField, Tooltip("間隔")]
	private float _interval = 0.2f;
	protected float _nowTime;
	protected float _nowTimeDelay;
	// 入力情報""
	protected Vector2 _axis;

	protected int _menuType;


	// Start is called before the first frame update
	protected virtual void Start()
	{
		_startUI = this.gameObject;
		_menuList = new List<RectTransform>();
		foreach (RectTransform child in transform)
		{
			if (child.tag == "Button" && child.TryGetComponent(out RectTransform rect))
			{
				_menuList.Add(rect);
			}
		}
		_menuType = 0;
		_nowTime = 0;
		_nowTimeDelay = 0;
		_axis = Vector2.zero;
	}

	protected void SetInput()
	{
		_axis.x = Input.GetAxis("Horizontal");
		_axis.y = Input.GetAxis("Vertical");
	}

	private void AxisX()
	{
		if (_axis.x > 0 || _axis.x < 0)
		{

		}
	}

	protected void Seletct()
	{
		if(_menuList.Count < 0)
		{
			return;
		}
		// 入力があった場合時間を計測する
		if(_axis.y > 0 || _axis.y < 0)
		{
			if(_nowTimeDelay <= 0)
			{
				_nowTime = _interval;
			}
			_nowTimeDelay += Time.unscaledDeltaTime;
		}
		else
		{
			_nowTimeDelay = 0;
		}

		if(_nowTimeDelay > _delay)
		{
			_nowTime += Time.unscaledDeltaTime;
		}

		// インターバル時間を超えていたら処理を行う
		if (_nowTime >= _interval)
		{
			AudioManager.instance.PlaySE(_clip);
			_cursor.GetComponent<TextSlider>().SliderReset();
			if (_axis.y < 0)
			{
				_menuType++;
				if(_menuType > _menuList.Count - 1)
				{
					_menuType = 0;
				}
			}
			else if (_axis.y > 0)
			{
				_menuType--;
				if (_menuType < 0)
				{
					_menuType = _menuList.Count - 1;
				}
			}
			_nowTime = 0;
		}
		if (_cursor != null)
		{
			_cursor.position = _menuList[_menuType].position;
		}
	}

	private void Move()
	{
		// インターバル時間を超えていたら処理を行う
		if (_nowTime >= _interval)
		{
			AudioManager.instance.PlaySE(_clip);
			_cursor.GetComponent<TextSlider>().SliderReset();
			if (_axis.y < 0)
			{
				_menuType++;
				if (_menuType > _menuList.Count - 1)
				{
					_menuType = 0;
				}
			}
			else if (_axis.y > 0)
			{
				_menuType--;
				if (_menuType < 0)
				{
					_menuType = _menuList.Count - 1;
				}
			}
			if (_cursor != null)
			{
				_cursor.position = _menuList[_menuType].position;
			}
			_nowTime = 0;
		}
	}

	public void Check()
	{
		if(Input.GetKeyDown("Fire1"))
		{
			_startUI.SetActive(false);
			_backUI.SetActive(true);
		}
		if (Input.GetButtonDown("Fire2"))
		{
			AudioManager.instance.PlaySE(_clip2);
			if (_menuType < _uiList.Count)
			{
				_startUI.SetActive(false);
				_uiList[_menuType].SetActive(true);
			}
		}
	}


	// Update is called once per frame
	void Update()
    {
		SetInput();
		Seletct();
		Check();
    }
}
