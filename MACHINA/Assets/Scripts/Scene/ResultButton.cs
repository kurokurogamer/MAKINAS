using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void SetInput()
	{
		if(Input.GetButtonDown("Fire2"))
		{
			SceneCtl.instance.SceneChange(SceneCtl.SceneList.MENU);
		}
		if (Input.GetButtonDown("Fire3"))
		{
			SceneCtl.instance.SceneChange(SceneCtl.SceneList.MENU);
		}
	}

    // Update is called once per frame
    void Update()
    {
		SetInput();
    }
}
