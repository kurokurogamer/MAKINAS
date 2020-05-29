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
	protected List<Weapon> _weaponList = new List<Weapon>();
	[SerializeField]
	private LayerMask _layerMask = 0;



	// Use this for initialization
	protected virtual void Start()
    {
        _rigid = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();
		_lastForce = Vector3.zero;
		foreach (Transform child in transform)
		{
			if (child.TryGetComponent(out Weapon weapon))
			{
				_weaponList.Add(weapon);
			}
		}
	}

	// プレイヤー、AIでも対応可能なように値をもらって判断
	// Animationで移動
	protected void Walk(Vector2 Axis)
	{
		// 入力がない場合は処理をスキップする
		if (Axis.x == 0 && Axis.y == 0)
		{
			return;
		}
		_animator.SetBool("Hover", false);
		_animator.SetBool("Walk", true);
		Vector3 forceX = Vector3.zero;
		Vector3 forceY = Vector3.zero;

		GroundCheck();
		if (Axis.x > 0.1f)
		{
			forceX = transform.right * _power;
		}
		else if (Axis.x < -0.1f)
		{
			forceX = -transform.right * _power;
		}
		if (Axis.y > 0.1f)
		{
			_animator.speed = 1;
			forceY = transform.forward * _power;
		}
		else if (Axis.y < -0.1f)
		{
			forceY = -transform.forward * _power;
		}
		_lastForce = forceY + forceX;
	}

	private void GroundCheck()
	{
		RaycastHit hit;
		//if(Physics.Raycast(transform.position,Vector3.down,hit,10,_layerMask,QueryTriggerInteraction.Ignore))
		//{
		//	transform.position = new Vector3(transform.position.x, hit.point.y + 4.5f, transform.position.z);
		//}
		if (Physics.BoxCast(transform.position, new Vector3(1, 1, 1), Vector3.down, out hit, transform.rotation, 10, _layerMask, QueryTriggerInteraction.Ignore))
		{
			transform.position = new Vector3(transform.position.x, hit.point.y + 4.5f, transform.position.z);
		}
	}

	protected void Boost(Vector2 Axis)
	{
		_slider.value -= 1 * Time.deltaTime * 0.2f;
	}

	// プレイヤー、AIでも対応可能なように値をもらって判断
	protected void Hover(Vector2 Axis)
	{
		if (Axis.x == 0 && Axis.y == 0)
		{
			return;
		}
		GroundCheck();

		_animator.SetBool("Hover", true);
		_animator.SetBool("Walk", false);
		Vector3 forceX = Vector3.zero;
		Vector3 forceY = Vector3.zero;


		if (Axis.x > 0)
		{
			forceX = transform.right * _power * 2;
		}
		else if(Axis.x < 0)
		{
			forceX = transform.right * _power * -2;
		}
		if (Axis.y > 0)
		{
			forceY = transform.forward * _power * 2;
		}
		else if (Axis.y < 0)
		{
			forceY = transform.forward * _power * -2;
		}

		_lastForce = forceY + forceX;
		_lastForce.y = 0;
	}

	protected void Jump()
    {
        _lastForce.y += _power * 5;
    }

	protected void ChangeMode()
	{
		Debug.Log("ログ");
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
		_lastForce = new Vector3(0, 0, 0);
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
		_rigid.velocity = new Vector3(_lastForce.x, _lastForce.y, _lastForce.z);
	}

	private void OnDrawGizmos()
	{
		RaycastHit rayCast;
		bool hit = Physics.BoxCast(transform.position, new Vector3(1, 1, 1), Vector3.down, out rayCast, transform.rotation, 10, _layerMask, QueryTriggerInteraction.Ignore);
		if(hit)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(rayCast.point, Vector3.one * 2);
		}
		else
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireCube(transform.position + Vector3.down * 10, Vector3.one * 2);
		}

	}
}
