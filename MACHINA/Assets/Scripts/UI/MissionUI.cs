using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionUI : SelectMenu
{
	[System.Serializable]
	public struct Explanation
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

	// Start is called before the first frame update
	protected override void Start()
    {
		base.Start();
		//_scene = GetComponent<SceneChangeButton>();
	}

	private void Change()
	{
		_image.sprite = _explanationsList[_menuType].sprite;
		_text.text = _explanationsList[_menuType].text;
		//AudioManager.instance.PlaySE(_explanationsList[_menuType].voice);
	}

	private  void Check()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			_startUI.SetActive(false);
			_backUI.SetActive(true);
		}
		if (Input.GetButtonDown("Fire2"))
		{
			//_scene.Change(3);
		}
	}

	// Update is called once per frame
	void Update()
    {
		SetInput();
		Seletct();
		Change();
		Check();
    }
}
