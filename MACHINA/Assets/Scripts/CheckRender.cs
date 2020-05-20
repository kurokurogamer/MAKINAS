﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRender : MonoBehaviour
{

    [SerializeField, Tooltip("描画フラグ")]
    private bool _isRendFlag = false;
    public bool IsRendFlag
    {
        get { return _isRendFlag; }
    }
    // Use this for initialization
    void Start()
    {

    }

	private void LateUpdate()
	{
		_isRendFlag = false;
	}

	// 各カメラの描画範囲内に入っていたら呼ばれる
	private void OnWillRenderObject()
    {
        if (Camera.current.name == "Main Camera")
        {
			Debug.Log("カメラに写っている。");
            _isRendFlag = true;
        }
    }
}