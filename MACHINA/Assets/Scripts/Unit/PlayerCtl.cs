using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーの入力系をまとめる場所
public class PlayerCtl : UnitControl
{
    private Vector2 _stickLeft;
	private Vector2 _stickRight;
	private float _triggerLR;
	private Vector3 _rot;
	[SerializeField, Tooltip("ロックオンUI")]
	private GameObject _lockOnUi = null;
	[SerializeField, Tooltip("スキャンUI")]
	private GameObject _scanUi = null;

	[SerializeField]
	private RectTransform _circle;
	private Vector2 _circleForce;
	[SerializeField, Tooltip("システムメッセージ")]
	private RectTransform _message;

    protected override void Start()
    {
		base.Start();
		_stickLeft = Vector2.zero;
		_stickRight = Vector2.zero;
		_triggerLR = 0;
		_circleForce = _circle.sizeDelta;

		_rot = transform.eulerAngles;
		AudioManager.instance.StopBGM();
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
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Alpha6))
		{
			_stickLeft.x = 1;
		}
		else if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.Alpha4))
		{
			_stickLeft.x = -1;
		}
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Alpha8))
		{
			_stickLeft.y = 1;
		}
		else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.Alpha2))
		{
			_stickLeft.y = -1;
		}

		if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}

		if (Input.GetButtonDown("RStickPush"))
		{
			_lockOnUi.SetActive(_scanUi.activeInHierarchy);
			_scanUi.SetActive(!_scanUi.activeInHierarchy);
		}

		if(Input.GetButtonDown("LStickPush"))
		{
			ChangeMode();
		}

		if (Input.GetButtonDown("LB") || Input.GetKeyDown(KeyCode.B))
		{
			ChangeMode();
		}

		if(!_lockOnUi.activeInHierarchy)
		{
			return;
		}

		if (_triggerLR <= -0.1f || Input.GetKey(KeyCode.X))
		{
			_weaponList[0].Attack();
		}

		if (_triggerLR >= 0.1f || Input.GetKey(KeyCode.Z))
		{
			_weaponList[1].Attack();
		}

		if (Input.GetButton("RB") || Input.GetKey(KeyCode.A))
		{
			_weaponList[2].Attack();
		}
	}

    // 入力系はUpdateで処理してください
    void Update()
    {
		transform.eulerAngles = new Vector3(_rot.x, Camera.main.transform.eulerAngles.y, _rot.z);
        InputSet();
	}

    private void LateUpdate()
    {
    }

	protected void FixedUpdate()
	{
		System(_stickLeft);
		// カーソルの縮小
		_circleForce = Vector2.MoveTowards(_circleForce, new Vector2(400, 400), 10);
		if (_stickLeft.x == 0 && _stickLeft.y == 0)
		{
			// カーソルの拡大
			_circleForce = Vector2.MoveTowards(_circleForce, new Vector2(500, 500), 20);
			Brake();
		}
		// 最終的なカーソルの大きさを反映させる
		_circle.sizeDelta = _circleForce;
	}
}
