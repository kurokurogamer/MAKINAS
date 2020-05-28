using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーの入力系をまとめる場所
public class PlayerCtl : UnitControl
{
	[SerializeField]
	private AudioClip _clip = null;

	private List<Weapon> _weaponList = new List<Weapon>();

    private Vector2 _stickLeft;
	private Vector2 _stickRight;
	private float _triggerLR;
	private float _nowTime;
	private bool _isJamp;
	private bool _boost;
	private Vector3 _rot;

    protected override void Start()
    {
		base.Start();
		_stickLeft = Vector2.zero;
		_stickRight = Vector2.zero;
		_triggerLR = 0;
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
		_rot = transform.eulerAngles;

	}

	// ファイルからボタン情報を取得
	private void SetButton()
	{
	}

	private void InputSet()
    {
        // コントローラーの入力を受け取る
        _stickLeft.x = Input.GetAxis("Horizontal");
        _stickLeft.y = Input.GetAxis("Vertical");
		_stickRight.x = Input.GetAxis("Horizontal2");
		_stickRight.y = Input.GetAxis("Vertical2");
		_triggerLR = Input.GetAxis("TriggerLR");
        // キー入力を受け取る

		if(Input.GetKey(KeyCode.RightArrow))
        {
            _stickLeft.x = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _stickLeft.x = -1;
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            _stickLeft.y = 1;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            _stickLeft.y = -1;
        }

		if (Input.GetButtonDown("Jump") || Input.GetKey(KeyCode.Space))
		{
			Jump();
		}

		if(Input.GetButtonDown("LB"))
		{
			ChangeMode();
		}
		_nowTime += Time.deltaTime;
		if (Input.GetButton("Fire2"))
		{
			_weaponList[0].Attack();
		}
		if (Input.GetButton("RB"))
		{
			_weaponList[1].Attack(3);
		}
		if (_triggerLR <= -0.1f)
		{
			_weaponList[2].Attack();
		}

		if (_triggerLR >= 0.1f)
		{
			_weaponList[2].Attack();
		}

		if (Input.GetButtonDown("RStickPush"))
		{
			Sensor();
		}
	}

    // 入力系はUpdateで処理してください
    void Update()
    {
        InputSet();
		transform.eulerAngles = new Vector3(_rot.x, Camera.main.transform.eulerAngles.y, _rot.z);
	}

    private void LateUpdate()
    {
    }

	protected void FixedUpdate()
	{
		System(_stickLeft);
		if (_stickLeft.x == 0 && _stickLeft.y == 0)
		{
			Brake();
		}
	}
}
