using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScene : MonoBehaviour
{
	private HitPoint _hitpoint;

    // Start is called before the first frame update
    void Start()
    {
		_hitpoint = GetComponent<HitPoint>();
    }

	private void ChangeScene()
	{
		SceneCtl.instance.SceneChange(SceneCtl.SceneList.RESULT);
	}

	// Update is called once per frame
	void Update()
    {
		if(_hitpoint)
		{
			if(_hitpoint.HP <= 0)
			{
				ChangeScene();
			}
		}
    }
}
