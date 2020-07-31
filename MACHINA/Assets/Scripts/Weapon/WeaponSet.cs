using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSet : MonoBehaviour
{
	private List<Weapon> _weaponList;
	// Start is called before the first frame update
	void Start()
	{
		_weaponList = new List<Weapon>();

		foreach (Transform child in transform)
		{
			if (child.TryGetComponent(out Weapon weapon))
			{
				_weaponList.Add(weapon);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
	}
}
