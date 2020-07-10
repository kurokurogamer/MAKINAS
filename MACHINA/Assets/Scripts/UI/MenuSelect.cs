using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour
{
	private enum INPUT_TYPE
	{
		X,
		Y,
		MAX
	}

	[SerializeField, Tooltip("長押しの遅れ")]
	private float _delay = 1.0f;
	[SerializeField, Tooltip("間隔")]
	private float _interval = 0.2f;
	private float _nowTime;
	private float _nowTimeDelay;

	[SerializeField, Tooltip("入力タイプ")]
	private INPUT_TYPE _type = INPUT_TYPE.Y;
	[SerializeField, Tooltip("機能だけ消すか、オブジェクトごと消すかのフラグ")]
	protected bool _typeEnabled = false;

	[SerializeField, Tooltip("ひとつ前のUI")]
	protected GameObject _backUI = null;
	protected MenuSelect _startUI = null;

	private Text _cursorText = null;
	private List<RectTransform> _menuList;
	private List<Text> _textList;

	[SerializeField, Tooltip("カーソル")]
	protected RectTransform _cursor = null;

	// UI用音声
	protected UIAudio _uiAudio;
	// 入力情報
	protected float _axis;
	// 選択中のメニューID
	protected int _id;
	public int ID
	{
		get { return _id; }
	}

	// Start is called before the first frame update
	protected virtual void Start()
	{
		_nowTime = 0;
		_nowTimeDelay = 0;

		_startUI = this;
		_uiAudio = transform.root.GetComponent<UIAudio>();
		_menuList = new List<RectTransform>();
		_textList = new List<Text>();
		foreach (RectTransform button in transform)
		{
			if (button.tag == "Button" && button.TryGetComponent(out RectTransform rect))
			{
				_menuList.Add(rect);
				foreach(Transform child in button)
				{
					if(child.TryGetComponent(out Text text))
					{
						_textList.Add(text);
					}
				}
			}
		}
		if(_cursor)
		{
			foreach(RectTransform child in _cursor)
			{
				if(child.TryGetComponent(out Text text))
				{
					_cursorText = text;
				}
			}
		}
		_axis = 0;
		_id = 0;
	}

	protected void SetInput()
	{
		switch (_type)
		{
			case INPUT_TYPE.X:
				_axis = Input.GetAxis("Horizontal");
				break;
			case INPUT_TYPE.Y:
				_axis = Input.GetAxis("Vertical");
				break;
			case INPUT_TYPE.MAX:
			default:
				break;
		};
	}

	protected void AxisY()
	{
		// 入力があった場合時間を計測する
		if (_axis > 0 || _axis < 0)
		{
			if (_nowTimeDelay <= 0)
			{
				_nowTime = _interval;
			}
			_nowTimeDelay += Time.unscaledDeltaTime;
		}
		else
		{
			_nowTimeDelay = 0;
		}
	}

	protected void Seletct()
	{
		// 動かすメニューがなければ処理しない
		if(_menuList.Count < 0)
		{
			return;
		}
		// ボタンを押しているか
		if (_axis > 0 || _axis < 0)
		{
			if (_nowTimeDelay <= 0)
			{
				_nowTime = _interval;
			}
			_nowTimeDelay += Time.unscaledDeltaTime;
		}
		else
		{
			_nowTimeDelay = 0;
		}

		// 反応時間を超えていたら
		if (_nowTimeDelay > _delay)
		{
			_nowTime += Time.unscaledDeltaTime;
		}

		// インターバル時間を超えていたら処理を行う
		if (_nowTime >= _interval)
		{
			AudioManager.instance.PlaySE(_uiAudio.MoveSE);
			if (_cursor)
			{
				if (_cursor.TryGetComponent(out TextSlider _slider))
				{
					_slider.SliderReset();
				}
			}

			if (_axis < 0)
			{
				_id++;
				if (_id > _menuList.Count - 1)
				{
					_id = 0;
				}
			}
			else if (_axis > 0)
			{
				_id--;
				if (_id < 0)
				{
					_id = _menuList.Count - 1;
				}
			}
			// カーソルが設定されているなら使用する
			if (_cursor != null)
			{
				_cursor.position = _menuList[_id].position;
				_cursorText.text = _textList[_id].text;
			}
			_nowTime = 0;
			Debug.Log("ムーブ");
		}
	}

	protected virtual void Check()
	{
		// キャンセルボタンを押したときの処理
		if(Input.GetButtonDown("Fire3"))
		{
			AudioManager.instance.PlaySE(_uiAudio.CancelSE);
			if (_typeEnabled)
			{
				_startUI.enabled = false;
			}
			else
			{
				_startUI.gameObject.SetActive(false);
			}
			_backUI.SetActive(true);
		}
		// 決定ボタンを押したときの処理
		else if (Input.GetButtonDown("Fire2"))
		{
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
