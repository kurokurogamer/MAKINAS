using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitControl : MonoBehaviour
{
	protected enum UNIT_MODE
	{
		WAIK,
		BOOST,
		HOVER,
		MAX,
	}
	private Rigidbody _rigid;
	private Animator _animator = null;

	private Vector3 _forceX;
	private Vector3 _forceY;
    // 加わる最終的な力
    private Vector3 _lastForce;
    [SerializeField]
    protected float _power = 1f;
	[SerializeField]
	protected float _wait = 100.0f;
	[SerializeField, Tooltip("ブーストゲージ")]
	private Slider _slider;
	[SerializeField, Tooltip("移動エフェクト")]
	private ParticleSystem _walkEffect = null;
	[SerializeField, Tooltip("ホバーエフェクト")]
	private ParticleSystem _boostEffect = null;
	[SerializeField, Tooltip("移動エフェクト")]
	private ParticleSystem _hobaEffect = null;
	protected UNIT_MODE _mode = UNIT_MODE.WAIK;
	[SerializeField]
	private GameObject _testUIImage = null;


	// Use this for initialization
	protected virtual void Start()
    {
        _rigid = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();
		_lastForce = Vector3.zero;
	}

	// プレイヤー、AIでも対応可能なように値をもらって判断
	// Animationで移動
	protected void Walk(Vector2 Axis)
	{
		_animator.SetBool("Hover", false);
		_animator.SetBool("Walk", true);
		transform.position = _animator.rootPosition;
		// 入力がない場合は処理をスキップする
		if (Axis.x == 0 && Axis.y == 0)
		{
			return;
		}
		if (Axis.x > 0.1f)
		{
			_forceX = transform.right * _power;
		}
		else if (Axis.x < -0.1f)
		{
			_forceX = -transform.right * _power;
		}
		if (Axis.y > 0.1f)
		{
			_animator.speed = 1;
			_forceY = transform.forward * _power;
		}
		else if (Axis.y < -0.1f)
		{
			if (_animator.GetCurrentAnimatorStateInfo(0).speed <= 0.0f)
			{
				Debug.Log("再生位置を取得");
				_animator.Play(Animator.StringToHash("Walk"), 0, 0.5f);
			}
			_forceY = -transform.forward * _power;
		}

		_lastForce = _forceY + _forceX;
	}

	protected void Boost(Vector2 Axis)
	{
		_slider.value -= 1 * Time.deltaTime * 0.2f;
	}

	// プレイヤー、AIでも対応可能なように値をもらって判断
	protected void Hover(Vector2 Axis)
	{
		_animator.SetBool("Hover", true);

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

	protected void Jump()
    {
        _lastForce.y += _power;
    }

	protected void Sensor()
	{
		Debug.Log("センサー起動");
		bool active = _testUIImage.activeInHierarchy ? false : true;
		_testUIImage.SetActive(active);
	}

	protected void ChangeMode()
	{
		if (_mode == UNIT_MODE.WAIK)
		{
			Debug.Log("ホバーモード");
			_mode = UNIT_MODE.HOVER;
		}
		else
		{
			Debug.Log("歩行モード");
			_mode = UNIT_MODE.WAIK;
		}
	}

	protected void Brake()
	{
		_forceX = transform.forward;
		_forceY = Vector3.zero;
		_animator.SetBool("Walk", false);
		_animator.SetBool("Hover", false);
		//_lastForce = new Vector3(_rigid.velocity.x / 2, 0, _rigid.velocity.z / 2);
		_slider.value += 1 * Time.deltaTime;
	}

	protected void System(Vector2 Axis)
	{
		switch (_mode)
		{
			case UNIT_MODE.WAIK:
				Walk(Axis);
				Debug.Log("Brake");
				break;
			case UNIT_MODE.BOOST:
				Boost(Axis);
				Debug.Log("Boost");
				break;
			case UNIT_MODE.HOVER:
				Hover(Axis);
				Debug.Log("Hover");
				break;
			case UNIT_MODE.MAX:
			default:
				break;
		}
		_lastForce.y -= _lastForce.y / 2;
		//_rigid.velocity = new Vector3(_lastForce.x, _lastForce.y + _rigid.velocity.y, _lastForce.z);
	}
}
