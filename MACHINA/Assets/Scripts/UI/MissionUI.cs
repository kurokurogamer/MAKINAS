using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionUI : MonoBehaviour
{
	[System.Serializable]
	private struct Explanation
	{
		public AudioClip voice;
		public Sprite sprite;
		[SerializeField, TextArea(5,10),Tooltip("ミッション内容")]
		public string text;
	}

	[SerializeField, Tooltip("差し替えるイメージ")]
	private Image _image = null;
	[SerializeField, Tooltip("差し替えるテキスト")]
	private Text _text = null;
	[SerializeField, Tooltip("ミッション一覧")]
	private List<Explanation> _explanationsList = new List<Explanation>();
	[SerializeField, Tooltip("選択時SE")]
	private AudioClip _clip = null;
	[SerializeField, Tooltip("決定時SE")]
	private AudioClip _clip2 = null;

	// 長押し時のメニュー
	[SerializeField, Tooltip("長押しの遅れ")]
	private float _delay = 1.0f;
	[SerializeField, Tooltip("間隔")]
	private float _interval = 0.2f;
	private float _nowTime;
	private float _nowTimeDelay;
	// 入力情報""
	private Vector2 _axis;
	private int _menuNum;

	// Start is called before the first frame update
	void Start()
    {
		_menuNum = 0;
		_nowTime = 0;
		_nowTimeDelay = 0;
		_axis = Vector2.zero;
	}

	protected void Seletct()
	{
		_axis.x = Input.GetAxis("Horizontal");
		_axis.y = Input.GetAxis("Vertical");
		// 入力があった場合時間を計測する
		if (_axis.x > 0 || _axis.x < 0)
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

		if (_nowTimeDelay > _delay)
		{
			_nowTime += Time.unscaledDeltaTime;
		}

		// インターバル時間を超えていたら処理を行う
		if (_nowTime >= _interval)
		{
			AudioManager.instance.StopSE();
			Debug.Log(_nowTime);
			AudioManager.instance.PlaySE(_clip);
			if (_axis.x < 0)
			{
				_menuNum++;
				if (_menuNum > _explanationsList.Count - 1)
				{
					_menuNum = 0;
				}
			}
			else if (_axis.x > 0)
			{
				_menuNum--;
				if (_menuNum < 0)
				{
					_menuNum = _explanationsList.Count - 1;
				}
			}
			_nowTime = 0;
			AudioManager.instance.PlaySE(_explanationsList[_menuNum].voice);
		}
	}


	private void Change()
	{
		_image.sprite = _explanationsList[_menuNum].sprite;
		_text.text = _explanationsList[_menuNum].text;

	}

	// Update is called once per frame
	void Update()
    {
		Seletct();
		Change();
    }
}
