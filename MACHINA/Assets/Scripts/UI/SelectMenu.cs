using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMenu : MonoBehaviour
{
	private enum INPUT_TYPE
	{
		X,
		Y,
		MAX
	}

	[SerializeField, Tooltip("入力タイプ")]
	private INPUT_TYPE _type = INPUT_TYPE.Y;
	[SerializeField, Tooltip("機能だけ消すか、オブジェクトごと消すかのフラグ")]
	private bool _typeEnabled = false;

	[SerializeField, Tooltip("テキストエリア"), TextArea(2, 5)]
	private string[] _str = new string[0];
	[SerializeField, Tooltip("テキスト")]
	private Text _text = null;

	[SerializeField, Tooltip("カーソル")]
	protected RectTransform _cursor = null;
	[SerializeField, Tooltip("有効にするUIのリスト")]
	private List<GameObject> _uiList = null;
	private List<RectTransform> _menuList;

	[SerializeField, Tooltip("ひとつ前のUI")]
	protected GameObject _backUI = null;
	protected SelectMenu _startUI = null;


	[SerializeField, Tooltip("長押しの遅れ")]
	private float _delay = 1.0f;
	[SerializeField, Tooltip("間隔")]
	private float _interval = 0.2f;
	protected float _nowTime;
	protected float _nowTimeDelay;
	// UI用音声
	private UIAudio _uiAudio;
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
		_uiAudio = transform.root.GetComponent<UIAudio>();
		//_clip = transform.root.GetComponent<UIAudio>().Clip[0];
		_startUI = this;
		_menuList = new List<RectTransform>();
		foreach (RectTransform child in transform)
		{
			if (child.tag == "Button" && child.TryGetComponent(out RectTransform rect))
			{
				_menuList.Add(rect);
			}
		}
		_nowTime = 0;
		_nowTimeDelay = 0;
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
			//AudioManager.instance.PlaySE(_uiAudio.MoveSE);
			if (_cursor)
			{
				//_cursor.GetComponent<TextSlider>().SliderReset();
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
				//_cursor.position = _menuList[_id].position;
			}
			if(_text)
			{
				_text.text = _str[_id];
			}
			_nowTime = 0;
		}
	}

	protected virtual void Check()
	{
		// キャンセルボタンを押したときの処理
		if(Input.GetButtonDown("Fire1"))
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
			AudioManager.instance.PlaySE(_uiAudio.PushSE);
			if (_id < _uiList.Count)
			{
				if (_typeEnabled)
				{
					_startUI.enabled = false;
				}
				else
				{
					_startUI.gameObject.SetActive(false);
				}
				_uiList[_id].SetActive(true);
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
