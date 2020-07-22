using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtl : MonoBehaviour
{
	public enum SceneList
	{
		TITLE,
		MENU,
		GAME1,
		GAME2,
		GAME3,
		RESULT,
		MAX
	}

	public static SceneCtl instance = null;

	private string _sceneName;
	[SerializeField, Tooltip("追加するシーン")]
	private List<string> _sceneNameList = new List<string>();

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
			case SceneList.GAME1:
				_sceneName = "GameScene1";
				break;
			case SceneList.GAME2:
				_sceneName = "GameScene2";
				break;
			case SceneList.GAME3:
				_sceneName = "GameScene3";
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

	public void SceneLoad()
	{
		SceneManager.LoadSceneAsync("Stage1");
	}

	public void AddScene(string name)
	{
		SceneManager.LoadScene(name, LoadSceneMode.Additive);
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
