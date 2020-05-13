using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControl : MonoBehaviour
{
	private Rigidbody _rigid;

	private Vector3 _forceX;
	private Vector3 _forceY;
    // 加わる最終的な力
    private Vector3 _lastForce;
    [SerializeField]
    private float _power = 1f;
    [SerializeField]
    private GameObject[] _boost;

    // Use this for initialization
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
		_lastForce = Vector3.zero;
    }

	// プレイヤー、AIでも対応可能なように値をもらって判断
	public void Walk(Vector2 Axis)
	{
		if(Axis.x > 0.1f)
		{
			_forceX = transform.right * _power;
		}
		else if(Axis.x < -0.1f)
		{
			_forceX = -transform.right * _power;
		}
		else
		{
			_forceX = Vector3.zero;
		}
		if (Axis.y > 0.1f)
		{
			_forceY = transform.forward * _power;
		}
		else if (Axis.y < -0.1f)
		{
			_forceY = -transform.forward * _power;
		}
		else
		{
			_forceY = Vector3.zero;
		}

		_lastForce = _forceX + _forceY;

	}

	// プレイヤー、AIでも対応可能なように値をもらって判断
	public void Boost(Vector2 Axis)
    {
        foreach (var boost in _boost)
        {
            boost.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Axis.x > 0.1f)
        {
            _lastForce += transform.right / 2;
        }
        else if (Axis.x < -0.1f)
        {
            _lastForce -= transform.right / 2;
        }
        if (Axis.y > 0.1f)
        {
            _lastForce += transform.forward / 2;
            foreach(var boost in _boost)
            {
                boost.transform.rotation = Quaternion.Euler(90, 0, 0);
            }
        }
        else if (Axis.y < -0.1f)
        {
            _lastForce -= transform.forward / 2;
        }
        if (Axis.x > -0.1f && Axis.x < 0.1f)
        {
            _lastForce.x = Vector3.Lerp(_lastForce, Vector3.zero, Time.deltaTime).x;
        }
        if (Axis.y > -0.1f && Axis.y < 0.1f)
        {
            _lastForce.z = Vector3.Lerp(_lastForce, Vector3.zero, Time.deltaTime).z;
        }
    }

    public void Jump()
    {
        _lastForce.y += _power;
    }

    public void MoveForce()
    {
        if (_lastForce.y > 0)
        {
            _lastForce.y -= 0.5f;
        }
        _rigid.velocity = new Vector3(_lastForce.x, _lastForce.y + _rigid.velocity.y, _lastForce.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        MoveForce();
    }
}
