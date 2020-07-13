using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 全武器共通の親クラス
public class Weapon : MonoBehaviour
{
	[SerializeField, Tooltip("リロード速度")]
	protected float _reloadTime = 1.0f;
	[SerializeField, Tooltip("発射感覚時間")]
	protected float _waitTime = 1.0f;
	[SerializeField, Tooltip("連射速度")]
	protected float _rapidSpeed = 0.1f;
	[SerializeField, Tooltip("弾速")]
	protected float _shotSpeed = 1.0f;
	[SerializeField, Tooltip("使用する弾")]
	protected GameObject _bullet = null;
	[SerializeField, Tooltip("最大弾数")]
	protected int _maxAmmo = 100;
	[SerializeField, Tooltip("同時に発射される弾数")]
	protected int _multiple = 1;
	// 武器のアニメーション
	protected Animator _animator;
	// 現在弾数
	protected int _ammo = 0;
	// 現在のリロード時間
	protected float _nowReloadTime = 0.0f;
	// 現在の発射感覚時間
	protected float _nowWaitTime = 0.0f;
	// 現在の再連射間隔
	protected float _rapidTime = 0.1f;
	[SerializeField, Tooltip("発射音")]
	protected AudioClip _clip = null;
	[SerializeField]
	private List<GameObject> _shotPoint = new List<GameObject>();

	protected virtual void Start()
	{
		_animator = GetComponent<Animator>();
		_ammo = _maxAmmo;
		_nowReloadTime = 0.0f;
		_nowWaitTime = _waitTime;
		if (_shotPoint.Count == 0)
		{
			foreach (Transform child in transform)
			{
				if (child.tag == "ShotPoint")
				{
					_shotPoint.Add(child.gameObject);
				}
			}
		}
	}

	protected void ReLoad()
	{
		if(_ammo <= 0)
		{
			_nowReloadTime += Time.deltaTime;
			if(_nowReloadTime > _reloadTime)
			{
				_ammo = _maxAmmo;
				_nowReloadTime = 0.0f;
			}
		}
	}

	public virtual void Attack()
	{
		if (_ammo > 0)
		{
			if (_nowWaitTime >= _waitTime)
			{
				_ammo--;
				_nowWaitTime -= _waitTime;
				StartCoroutine(RapidFire(_multiple));
			}
		}
	}
	public void Attack(int count)
	{
		if (_ammo > 0)
		{
			_ammo--;
			StartCoroutine(RapidFire(_multiple));
		}
	}

	public IEnumerator RapidFire(int count)
	{
		int i = 0;
		while(i < count)
		{
			_rapidTime += Time.deltaTime;
			if (_rapidTime >= _rapidSpeed)
			{
				GameObject obj;
				for(int j = 0; j < _shotPoint.Count; j++)
				{
					obj = Instantiate(_bullet, _shotPoint[j].transform.position, transform.rotation);
					if (transform.root.tag == "Player")
					{
						obj.GetComponent<ShotObj>()._tagName = "Enemy";
					}
				}
				if (_shotPoint.Count == 0)
				{
					obj = Instantiate(_bullet, transform.position, transform.rotation);
					if (transform.root.tag == "Player")
					{
						obj.GetComponent<ShotObj>()._tagName = "Enemy";
					}
				}
				if(_clip != null)
				{
					AudioManager.instance.PlaySE(_clip);
				}
				i++;
				_rapidTime -= _rapidSpeed;
			}
			yield return null;
		}
	}
}
