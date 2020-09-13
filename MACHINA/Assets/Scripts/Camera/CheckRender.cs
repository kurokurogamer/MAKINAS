using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRender : MonoBehaviour
{
	private LockOnSystem _lockOnSystem;
    private bool _isRendFlag = false;
    public bool IsRendFlag
    {
        get { return _isRendFlag; }
    }
    // Use this for initialization
    void Start()
    {
		_lockOnSystem = Camera.main.GetComponent<LockOnSystem>();
	}

	// カーソル内のターゲット追加処理
	private void AddTarget()
	{
		if(_lockOnSystem.TargetList.Count > 0 && _isRendFlag)
		{
			GameObject obj = null;
			foreach(GameObject target in _lockOnSystem.TargetList)
			{
				if(target == gameObject)
				{
					obj = target;
				}
			}
			if(obj == null)
			{
				// 同一のターゲットがいないのでリストに追加
				_lockOnSystem.TargetList.Add(gameObject);
			}
		}
		else if(_isRendFlag)
		{
			// ターゲットリストが空の場合の初期追加
			_lockOnSystem.TargetList.Add(gameObject);
		}
		else
		{
			for(int i = _lockOnSystem.TargetList.Count; i > 0; i--)
			{
				if(_lockOnSystem.TargetList[i - 1] == gameObject)
				{
					_lockOnSystem.TargetList.Remove(gameObject);
				}
			}
		}
	}

	private void LateUpdate()
	{
		AddTarget();
		_isRendFlag = false;
	}

	// 各カメラの描画範囲内に入っていたら呼ばれる
	private void OnWillRenderObject()
    {
        if (Camera.current.name == "Main Camera")
        {
            _isRendFlag = true;
        }
    }
}
