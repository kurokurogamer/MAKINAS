using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtl : MonoBehaviour
{
	public static SceneCtl instance = null;

	private List<string> _sceneNameList = new List<string>();

	[SerializeField, Tooltip("黒背景")]
	private GameObject _fadeUI = null;
	[SerializeField, Tooltip("回転イメージ")]
	private List<GameObject> _ui = null;
	private FadeUI _back;
	private Coroutine _coroutine;

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
		_coroutine = null;
		for(int i =0; i < _ui.Count; i++)
		{
			_ui[i].SetActive(false);
		}
		_back = _fadeUI.GetComponent<FadeUI>();
    }

	public void LoadScene(string name)
	{
		if (name == "")
		{
			return;
		}
		if (_coroutine == null)
		{
			_coroutine = StartCoroutine(Load(name));
		}
	}

	private IEnumerator Load(string name)
	{
		_back.Mode = FadeUI.FADE_MODE.IN;	
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < _ui.Count; i++)
		{
			_ui[i].SetActive(true);
		}
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene(name, LoadSceneMode.Single);
		yield return new WaitForSeconds(2.0f);
		for (int i = 0; i < _ui.Count; i++)
		{
			_ui[i].SetActive(false);
		}
		_back.Mode = FadeUI.FADE_MODE.OUT;
		_coroutine = null;
		yield return null;
	}

	public void LoadSceneAsync(string name)
	{
		SceneManager.LoadSceneAsync(name);
	}

	public void AddScene(string name)
	{
		SceneManager.LoadScene(name, LoadSceneMode.Additive);
	}

	public void AddSceeAsync()
	{
		SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
	}

	public void UnLoadScene(string name)
	{
		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			if (name == SceneManager.GetSceneAt(i).name)
			{
				SceneManager.UnloadSceneAsync(name);
			}
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
