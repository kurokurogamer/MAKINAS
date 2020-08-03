using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButton : MonoBehaviour
{
	//private SceneCtl.SceneList _nextScene = SceneCtl.SceneList.TITLE;
	[SerializeField, Tooltip("SE")]
	private AudioClip _clip = null;
    // Start is called before the first frame update
    void Start()
    {
    }

	public void Active()
	{

	}

	private void Change()
	{
		if(Input.GetButtonDown("Fire2"))
		{
			AudioManager.instance.PlaySE(_clip);
			//SceneCtl.instance.SceneChange(_nextScene);
		}
	}

	public void Change(int i)
	{
		//SceneCtl.instance.SceneChange((SceneCtl.SceneList)i);
	}

	// Update is called once per frame
	void Update()
    {
		Change();
    }
}
