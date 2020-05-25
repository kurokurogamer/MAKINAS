using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeButton : MonoBehaviour
{
	[SerializeField]
	private SceneCtl.SceneList _nextScene = SceneCtl.SceneList.TITLE;
	[SerializeField, Tooltip("SE")]
	private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {
    }

	private void Change()
	{
		if(Input.GetButtonDown("Fire2"))
		{
			AudioManager.instance.PlaySE(_clip);
			SceneCtl.instance.SceneChange(_nextScene);
		}
	}

    // Update is called once per frame
    void Update()
    {
		Change();
    }
}
