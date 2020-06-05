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
	private string _tagName = "";
	[SerializeField, Tooltip("Effect")]
	private GameObject _particle = null;
	[SerializeField, Tooltip("タイプ")]
	private BULLET_TYPE _type = BULLET_TYPE.METAL;
	[SerializeField, Tooltip("衝突音")]
	private AudioClip _clip = null;

    void Start()
    {
		Destroy(gameObject, 5.0f);
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
			hp.Damage(10, _type);
			if (_particle)
			{
				Instantiate(_particle, transform.position, transform.rotation).GetComponent<ParticleSystem>().Play();
			}
			AudioManager.instance.PlayOneSE(_clip);
			Destroy(gameObject);
		}
	}

}
