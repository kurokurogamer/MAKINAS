using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーの入力系をまとめる場所
public class PlayerCtl : UnitControl
{
    [SerializeField, Tooltip("ショットオブジェクト")]
    private GameObject _bullet = null;
	// エフェクト弾
	//private ParticleSystem _particle;
    [SerializeField, Tooltip("武器")]
    private GameObject _weapon = null;
    private Vector2 _Axis;
	private Vector2 _Axis2;
	private float _stick;
	private float _nowTime;
	[SerializeField, Tooltip("発射感覚")]
	private float _waitTime = 0.1f;
	private bool _isJamp;
	private Weapon _waepon;

    protected override void Start()
    {
		base.Start();
		_Axis = Vector2.zero;
		_Axis2 = Vector2.zero;
		_stick = 0;
		_nowTime = 0;
		_isJamp = true;
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
				Jump();
				_isJamp = false;
			}
		}
		_nowTime += Time.deltaTime;
		if ((Input.GetButton("Fire2")) && _nowTime >= _waitTime)
		{
			Shot();
			_nowTime = 0;
		}
		if (Input.GetButton("RT") && _nowTime >= _waitTime)
		{
			Shot();
			_nowTime = 0;
		}

		if (Input.GetButtonDown("LT"))
		{
			Debug.Log("センサーシステム起動");
		}
	}

	public void Shot()
    {
		Instantiate(_bullet, _weapon.transform.position, _weapon.transform.rotation);
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

    private void FixedUpdate()
    {
		MoveForce();
		Walk(_Axis);
		Boost(_stick, _Axis);
    }

	private void OnCollisionStay(Collision collision)
	{
		_isJamp = true;
	}

}
