using System.Collections;
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

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        ReLoad();
    }
}
