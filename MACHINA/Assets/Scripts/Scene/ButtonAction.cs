using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
	[SerializeField]
	private string _sceneName = "";
	[SerializeField]
	private List<string> _sceneList = new List<string>();
	private bool _active = false;
	// Start is called before the first frame update
    void Start()
    {
    }

	public void Active()
	{
		if(_active)
		{
			return;
		}
		_active = true;
		for (int i = 0; i < _sceneList.Count; i++)
		{
			Debug.Log("scene名の追加" + _sceneList[i]);
			SceneCtl.instance.SceneNameList.Add(_sceneList[i]);
		}

		SceneCtl.instance.LoadScene(_sceneName);
	}

}
