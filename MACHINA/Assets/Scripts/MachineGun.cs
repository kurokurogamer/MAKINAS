using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
	private ParticleSystem _effect;
	private UnitControl _unit;
    // Start is called before the first frame update
    void Start()
    {
		_effect = _bullet.GetComponent<ParticleSystem>();
    }

	public void Attack()
	{
		if(_nowWaitTime > _waitTime)
		{
			_effect.Emit(1);
			_ammo--;
			_nowWaitTime = 0;
		}
	}

	// Update is called once per frame
	void Update()
    {
		_nowWaitTime += Time.deltaTime;
		ReLoad();
    }
}
