﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
	//private ParticleSystem _effect;
	private UnitControl _unit;
    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
    }

	public override void Attack()
	{
		base.Attack();
		//_animator.SetBool("Attack", true);
	}

	// Update is called once per frame
	void Update()
    {
		_nowWaitTime += Time.deltaTime;
		ReLoad();
    }
}
