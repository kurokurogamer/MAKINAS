using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICtl : UnitControl
{
	private Vector2 Axis;
	private float _changeTime;
    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
		Axis = Vector2.zero;
		_changeTime = 0;
    }

	public void SetAICtl()
	{
		_changeTime += Time.deltaTime;
		if (_changeTime > 2.0f)
		{
			Axis.x = Random.Range(-1.0f, 1.0f);
			Axis.y = Random.Range(-1.0f, 1.0f);
			_changeTime = 0;
		}
		_mode = UNIT_MODE.WAIK;
	}

    // Update is called once per frame
    void Update()
    {
		SetAICtl();
    }

	private void FixedUpdate()
	{
		System(Axis);
		transform.rotation = Quaternion.Euler(0, 180, 0);
	}

}
