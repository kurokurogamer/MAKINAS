using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject[] _boost = null;
	[SerializeField, Tooltip("ブーストゲージ")]
	private Slider _slider;
	[SerializeField, Tooltip("移動エフェクト")]
	private ParticleSystem _walkEffect = null;
	[SerializeField]
	private ParticleSystem _boostEffect = null;
	[SerializeField, Tooltip("")]
	private ParticleSystem _hobaEffect = null;
	// Use this for initialization
	protected virtual void Start()
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

		if (_lastForce != Vector3.zero)
		{
			_walkEffect.Emit(1);
		}
	}

	// プレイヤー、AIでも対応可能なように値をもらって判断
	public void Boost(float stickR, Vector2 Axis)
	{
		Debug.Log(stickR);
		if (stickR >= 0)
		{
			_slider.value += 1 * Time.deltaTime;
			return;
		}
		if(_slider.value <= 0)
		{
			_slider.value = 0;
			return;
		}
		foreach (var boost in _boost)
		{
			boost.transform.rotation = Quaternion.Euler(0, 0, 0);
		}

		_slider.value -= 1 * Time.deltaTime * 0.2f;

		_hobaEffect.Emit(1);

		if (Axis.x > 0)
		{
			_forceX = transform.right * _power * 3;
		}
		else if(Axis.x < 0)
		{
			_forceX = transform.right * _power * -3;
		}
		if (Axis.y > 0)
		{
			_forceY = transform.forward * _power * 3;
		}
		else if (Axis.y < 0)
		{
			_forceY = transform.forward * _power * -3;
		}

		_lastForce = _forceY + _forceX;
	}

	private void NewMethod(float stickR)
	{
		//if (stickR > -0.1f && stickR < 0.1f)
		//{
		//	_lastForce.x = Vector3.Lerp(_lastForce, Vector3.zero, Time.deltaTime).x;
		//}
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
}
