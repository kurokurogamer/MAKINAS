﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionUI : MenuSelect
{
	[SerializeField, Tooltip("差し替えるイメージ")]
	private Image _image = null;
	[SerializeField, Tooltip("差し替えるテキスト")]
	private Text _missionText = null;
	[SerializeField, Tooltip("ミッション一覧")]
	private List<ExplanationText> _explantion = new List<ExplanationText>();
	[SerializeField]
	private Text _missionUI = null;
	// Start is called before the first frame update
	protected override void Start()
    {
		base.Start();
		//_scene = GetComponent<SceneChangeButton>();
	}

	private void Change()
	{
		_image.sprite = _explantion[_id].SpriteImage;
		_missionText.text = _explantion[_id].Explantion;
	}

	private void MissionCode()
	{
		_missionUI.text = "M i s s i o n C o d e " + (_id + 1).ToString("0 0 : ") + _explantion[_id].MissionName;
	}

	protected override void Check()
	{
		if (Input.GetButtonDown("Fire1"))
		{
		}
		if (Input.GetButtonDown("Fire2"))
		{
		}
	}

	// Update is called once per frame
	void Update()
    {
		SetInput();
		Seletct();
		MissionCode();

		Change();
		Check();
	}
}
