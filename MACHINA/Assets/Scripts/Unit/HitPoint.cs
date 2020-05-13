using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 自機、敵変わらずに使用するヒットポイントスクリプト
public class HitPoint : MonoBehaviour
{
	[SerializeField, Tooltip("耐久値")]
	private int _hitPoint = 100;

	public int HP
	{
		get { return _hitPoint; }
		set { _hitPoint = value; }
	}

    // Update is called once per frame
    void Update()
    {
        if(_hitPoint <= 0)
		{
			Destroy(gameObject);
		}
    }
}
