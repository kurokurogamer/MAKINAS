using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelect : MenuSelect
{
	[SerializeField, Tooltip("テキストエリア"), TextArea(2, 5)]
	private string[] _str = new string[0];
	[SerializeField, Tooltip("テキスト")]
	private Text _text = null;

	[SerializeField, Tooltip("有効にするUIのリスト")]
	private List<GameObject> _uiList = null;

	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();
	}

	private void SetUI()
	{
		if (_text)
		{
			_text.text = _str[_id];
		}
	}


	protected override void Check()
	{
		// キャンセルボタンを押したときの処理
		if (Input.GetButtonDown("Fire3"))
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
        
    }
}
