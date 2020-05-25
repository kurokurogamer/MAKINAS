using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーの入力系をまとめる場所
public class PlayerCtl : UnitControl
{
    private Vector2 _Axis;
	private Vector2 _Axis2;
	private float _stick;
	private float _nowTime;
	private bool _isJamp;
	private List<Weapon> _weaponList = new List<Weapon>();
	[SerializeField]
	private AudioClip _clip = null;

    protected override void Start()
    {
		base.Start();
		_Axis = Vector2.zero;
		_Axis2 = Vector2.zero;
		_stick = 0;
		_nowTime = 0;
		_isJamp = true;
		foreach(Transform child in transform)
		{
			if (child.TryGetComponent(out Weapon weapon))
			{
				Debug.Log("処理中");
				_weaponList.Add(weapon);
			}
		}
	}

	// ファイルからボタン情報を取得
	private void SetButton()
	{
	}

	private void InputSet()
    {
        // コントローラーの入力を受け取る
        _Axis.x = Input.GetAxis("Horizontal");
        _Axis.y = Input.GetAxis("Vertical");
		_Axis2.x = Input.GetAxis("Horizontal2");
		_Axis2.y = Input.GetAxis("Vertical2");
		_stick = Input.GetAxis("TriggerLR");
        // キー入力を受け取る

		if(Input.GetKey(KeyCode.RightArrow))
        {
            _Axis.x = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _Axis.x = -1;
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            _Axis.y = 1;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            _Axis.y = -1;
        }
		if (Input.GetButton("Jump") || Input.GetKey(KeyCode.Space))
		{
			if (_isJamp)
			{
				//Jump();
				_isJamp = false;
			}
		}
		_nowTime += Time.deltaTime;
		if (Input.GetButton("Fire2"))
		{
			_weaponList[0].Attack();
			AudioManager.instance.PlayOneSE(_clip);
		}
		else
		{
			AudioManager.instance.StopSE();
		}
		if (Input.GetButton("RT"))
		{
			_weaponList[1].Attack(3);
		}
		if (Input.GetButton("LT"))
		{
			_weaponList[2].Attack();
		}

		if (Input.GetButtonDown("LT"))
		{
			Debug.Log("センサーシステム起動");
		}
	}

    // 入力系はUpdateで処理してください
    void Update()
    {
        InputSet();
		Vector3 rot = transform.eulerAngles;
		transform.eulerAngles = new Vector3(rot.x, Camera.main.transform.eulerAngles.y, rot.z);
	}

    private void LateUpdate()
    {
    }

	//  private void FixedUpdate()
	//  {
	////MoveForce();
	////Walk(_Axis);
	////Boost(_stick, _Axis);
	//  }

	//private void OnCollisionStay(Collision collision)
	//{
	//	_isJamp = true;
	//}

}
