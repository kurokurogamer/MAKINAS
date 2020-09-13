using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSt : MonoBehaviour
{
	[SerializeField]
	HitPoint _hitPoint = null;
	[SerializeField]
	private List<Weapon> _weaponList = new List<Weapon>();

	[SerializeField]
	private UnitControl _unit = null;

	private float hp = 0;
	private float[] _weaponTam = new float[3];

	private float _boost = 0;

	public float HP
	{
		get { return hp; }
	}

	private float[] WeaponTam
	{
		get { return _weaponTam; }
	}

	private float Boost
	{
		get { return _boost; }
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

	private void GetStates()
	{
		hp = _hitPoint.HP;
		_weaponTam[0] = _weaponList[0]._ammo;
		_weaponTam[1] = _weaponList[1]._ammo;

		_weaponTam[2] = _weaponList[2]._ammo;
		//_boost = _unit.boox

	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
