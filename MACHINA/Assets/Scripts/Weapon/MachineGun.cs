using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
	//private ParticleSystem _effect;
	private UnitControl _unit;
    // Start is called before the first frame update
    void Start()
    {
		//_effect = _bullet.GetComponent<ParticleSystem>();
    }

	// Update is called once per frame
	void Update()
    {
		_nowWaitTime += Time.deltaTime;
		ReLoad();
    }
}
