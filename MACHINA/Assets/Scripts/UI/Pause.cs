using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MenuSelect
{
	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();
	}

	private void Point()
	{
		if (_id == 0)
		{
			Time.timeScale = 1.0f;
			gameObject.SetActive(false);
		}
		else if (_id == 1)
		{
			Time.timeScale = 1.0f;
			SceneCtl.instance.SceneChange(SceneCtl.SceneList.MENU);
		}
	}


	// Update is called once per frame
	void Update()
	{
		Seletct();
		if (Input.GetButtonDown("Fire2"))
		{
			Point();
		}
	}
}
