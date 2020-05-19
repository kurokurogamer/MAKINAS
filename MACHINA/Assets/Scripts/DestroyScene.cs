using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void ChangeScene()
	{
		SceneCtl.instance.SceneChange(SceneCtl.SceneList.RESULT);
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
