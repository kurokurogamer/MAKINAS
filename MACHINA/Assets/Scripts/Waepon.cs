using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 全武器共通の親クラス
public class Waepon : MonoBehaviour
{
	[SerializeField, Tooltip("使用する弾")]
	private GameObject _bullet = null;
	[SerializeField, Tooltip("消滅時エフェクト")]
	private GameObject _effect = null;
	[SerializeField, Tooltip("発射感覚")]
	private float _waitTime = 0;

	public float WaitTime
	{
		get { return _waitTime; }
		set { _waitTime = value; }
	}

	private void OnDisable()
	{
		Instantiate(_effect, transform.position, transform.rotation);
	}

	private void WaitTimer()
	{
		_waitTime += Time.deltaTime;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
