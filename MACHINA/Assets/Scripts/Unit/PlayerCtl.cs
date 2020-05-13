using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtl : MonoBehaviour
{
    private UnitControl _unit;
    [SerializeField, Tooltip("ショットオブジェクト")]
    private GameObject _shotObj;
    [SerializeField]
    private GameObject _weapon;
    private Vector2 _Axis;

	private bool _isJamp;

    void Start()
    {
        _unit = GetComponent<UnitControl>();
        _Axis = Vector2.zero;
		_isJamp = true;
    }

    private void InputSet()
    {
        // コントローラーの入力を受け取る
        _Axis.x = Input.GetAxis("Horizontal");
        _Axis.y = Input.GetAxis("Vertical");
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
        if(Input.GetButton("Jump") || Input.GetKey(KeyCode.Space))
        {
            if(_unit != null)
            {
				if (_isJamp)
				{
					_unit.Jump();
					_isJamp = false;
				}
            }
        }
		if (Input.GetButtonDown("Fire2"))
		{
			Shot();
		}
		if(Input.GetButtonDown(""))
		{
			Shot();
		}
	}

	public void Shot()
    {
        Instantiate(_shotObj, _weapon.transform.position, _weapon.transform.rotation);
    }

    // 入力系はUpdateでまわしてください
    void Update()
    {
        InputSet();
	}

    private void LateUpdate()
    {
    }

    private void FixedUpdate()
    {
        
        if(_unit == null)
        {
            return;
        }
        _unit.Walk(_Axis);
    }

	private void OnCollisionStay(Collision collision)
	{
		_isJamp = true;
	}

}
