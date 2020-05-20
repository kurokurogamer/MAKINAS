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
	[SerializeField, Tooltip("弾速")]
	protected float _shotSpeed = 1.0f;
	[SerializeField, Tooltip("使用する弾")]
	protected GameObject _bullet = null;
	[SerializeField, Tooltip("最大弾数")]
	private int _maxAmmo = 100;
	// 現在弾数
	protected int _ammo = 0;
	// 現在のリロード時間
	protected float _nowReloadTime = 0.0f;
	// 現在の発射感覚時間
	protected float _nowWaitTime = 0.0f;

	protected virtual void Start()
	{
		_ammo = _maxAmmo;
		_nowReloadTime = 0.0f;
		_nowWaitTime = _waitTime;
	}

	private void OnDisable()
	{
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

	public void Attack()
	{
		if (_ammo > 0)
		{
			if (_nowWaitTime > _waitTime)
			{
				_ammo--;
				_nowWaitTime = 0;
				Instantiate(_bullet, transform.position, transform.rotation);
			}
		}
	}
	public void Attack(int count)
	{
		if (_ammo > 0)
		{
			_ammo -= count;
			StartCoroutine(RapidFire(count));
		}
	}

	public IEnumerator RapidFire(int count)
	{
		int i = 0;
		while(i < count)
		{
			_nowWaitTime += Time.deltaTime;
			if (_nowWaitTime > _waitTime)
			{
				Instantiate(_bullet, transform.position, transform.rotation);
				i++;
				_nowWaitTime = 0;
			}
			yield return null;
		}
	}
}
