using System.Collections;
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

    // Update is called once per frame
    void Update()
    {
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
