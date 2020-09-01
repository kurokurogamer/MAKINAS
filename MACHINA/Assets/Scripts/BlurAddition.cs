using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurAddition : MonoBehaviour
{
    [SerializeField, Tooltip("ブラーシェーダーを設定する")]
    private Shader _shader;
    // 自身のマテリアル
    private Material _material;

    [SerializeField, Tooltip("ゆがむ速度")]
    private float _speed = 10;
    // 現在の歪み度
    private float _force = 0;

    // Start is called before the first frame update
    void Start()
    {
        _material = new Material(_shader);
        GetComponent<MeshRenderer>().material = _material;
        _force = 0;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _force = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _force = Mathf.Lerp(_force, 1.5f, Time.unscaledDeltaTime * _speed);
        _material.SetFloat("_Factor", _force);
    }
}
