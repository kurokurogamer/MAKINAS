using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotObj : MonoBehaviour
{
	public enum BULLET_TYPE
	{
		METAL,
		BEAM,
		MAX
	}
    [SerializeField, Tooltip("弾速")]
    private float _shotSpeed = 1f;
	[SerializeField, Tooltip("攻撃有効なタグ")]
	public string _tagName = "";
	[SerializeField, Tooltip("Effect")]
	private GameObject _particle = null;
	[SerializeField, Tooltip("タイプ")]
	private BULLET_TYPE _type = BULLET_TYPE.METAL;
	[SerializeField, Tooltip("衝突音")]
	private AudioClip _clip = null;
	[SerializeField, Tooltip("Damage")]
	private int _damage = 100;
	private bool isHit = false;
	[SerializeField]
	private bool _stopFlag = false;
	Vector3 _point;
	private Collider _collider;

    void Start()
    {
		_collider = GetComponent<Collider>();
		Destroy(gameObject, 5.0f);
    }

    void Update()
    {
		if(!isHit)
		{
			transform.position += transform.forward * _shotSpeed;
		}
    }

	private void SetEmit()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.root.tag == _tagName)
		{
			var hp = other.transform.root.GetComponent<HitPoint>();
			hp.Damage(_damage, _type);
			if (_particle)
			{
				Instantiate(_particle, transform.position, transform.rotation).GetComponent<ParticleSystem>().Play();
			}
			AudioManager.instance.PlayOneSE(_clip);
			if (_stopFlag)
			{
				Destroy(gameObject, 1f);
				_collider.enabled = false;
				isHit = true;
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}

}
