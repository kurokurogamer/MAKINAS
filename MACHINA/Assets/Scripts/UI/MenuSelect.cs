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

	[SerializeField, Tooltip("入力タイプ")]
	private INPUT_TYPE _type = INPUT_TYPE.Y;

	[SerializeField, Tooltip("長押しの遅れ")]
	private float _delay = 1.0f;
	[SerializeField, Tooltip("間隔")]
	private float _interval = 0.2f;
	// 経過時間を図る
	private float _nowTime;
	private float _nowTimeDelay;

	[SerializeField, Tooltip("一つ前のメニュー項目")]
	protected GameObject _backUI = null;
	protected MenuSelect _startUI = null;

	protected List<RectTransform> _menuList;
	protected List<Text> _menuTextList;

	[SerializeField, Tooltip("カーソル")]
	protected RectTransform _cursor = null;
	private Text _cursorText = null;

	// UI用サウンド
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
		_menuTextList = new List<Text>();
		foreach (RectTransform menu in transform)
		{
			// TagがButtonの子オブジェクトをすべて取得する
			if (menu.tag == "Button" && menu.TryGetComponent(out RectTransform rect))
			{
				_menuList.Add(rect);
				// メニュー項目のテキストを取得
				foreach(Transform child in menu)
				{
					if(child.TryGetComponent(out Text text))
					{
						_menuTextList.Add(text);
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

		// 入力情報の初期化
		_axis = 0;
		_id = 0;
	}

	protected void SetInput()
	{
		// 入力タイプによって
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

	protected bool Select()
	{
		// 動かすメニューがなければ処理しない
		if(_menuList.Count < 0)
		{
			return false;
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
			return false;
		}

		// 反応時間を超えていたら
		if (_nowTimeDelay > _delay)
		{
			_nowTime += Time.unscaledDeltaTime;
		}

		// インターバル時間を超えていたら処理を行う
		if (_nowTime >= _interval)
		{
			if (_uiAudio)
			{
				// 操作音を鳴らす
				AudioManager.instance.PlaySE(_uiAudio.MoveSE);
			}
			if (_cursor)
			{
				if (_cursor.TryGetComponent(out TextSlider _slider))
				{
					_slider.SliderReset();
				}
			}
			// メニューIDの変更
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
				_cursorText.text = _menuTextList[_id].text;
				_cursor.sizeDelta = _menuList[_id].sizeDelta;
			}
			// インターバル分戻す
			_nowTime -= _interval;
			_nowTime = 0;
			return true;
		}
		return false;
	}

	protected virtual void Check()
	{
		// キャンセルボタンを押したときの処理
		if (Input.GetButtonDown("Fire1"))
		{
			// キャンセルサウンドを鳴らす
			AudioManager.instance.PlaySE(_uiAudio.CancelSE);
			// 戻った際のUIを表示しておく
			if (_backUI != null)
			{
				_backUI.SetActive(true);
			}
			// 自身のUIに対して非表示処理を行う
			_startUI.gameObject.SetActive(false);
		}
		else if (Input.GetButtonDown("Fire2"))
		{
			// 決定ボタンを押したときの処理
			this.enabled = false;
		}
	}


	// Update is called once per frame
	void Update()
    {
		SetInput();
		Select();
		Check();
    }
}
