using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotObj : MonoBehaviour
{
    [SerializeField, Tooltip("弾速")]
    private float _shotSpeed = 1f;
	[SerializeField, Tooltip("攻撃有効なタグ")]
	private string _tagName = "";
	[SerializeField, Tooltip("Effect")]
	private GameObject _particle = null;

    void Start()
    {

    }

    void Update()
    {
        transform.position += transform.forward * _shotSpeed;
    }

	private void SetEmit()
	{

	}

	private void OnTriggerEnter(Collider other)
	{

		if (other.tag == _tagName)
		{
			var hp = other.transform.GetComponent<HitPoint>();
			hp.Damage(10);
			Instantiate(_particle, transform.position, transform.rotation).GetComponent<ParticleSystem>().Play();
			Destroy(gameObject);
		}
	}

}
