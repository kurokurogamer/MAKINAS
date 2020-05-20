﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtl : MonoBehaviour
{
	public enum SceneList
	{
		TITLE,
		MENU,
		GAME,
		RESULT,
		MAX
	}

	public static SceneCtl instance = null;

	private string _sceneName;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		_sceneName = "";
    }

	public void SceneChange(SceneList sceneList)
	{
		switch (sceneList)
		{
			case SceneList.TITLE:
				_sceneName = "TitleScene";
				break;
			case SceneList.MENU:
				_sceneName = "MenuScene";
				break;
			case SceneList.GAME:
				_sceneName = "GameScene";
				break;
			case SceneList.RESULT:
				_sceneName = "ResultScene";
				break;
			case SceneList.MAX:
			default:
				break;
		}
		SceneChange(_sceneName);
	}

	public void SceneChange(string name)
	{
		if (name != "")
		{
			SceneManager.LoadScene(name);
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}