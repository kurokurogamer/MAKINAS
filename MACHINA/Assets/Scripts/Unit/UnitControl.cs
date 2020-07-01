using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	//[SerializeField, Tooltip("ブーストゲージ")]
	//private Slider _slider;
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
	private bool isAir;
	[SerializeField]
	private AudioClip _clip2;
	[SerializeField]
	UnityEngine.UI.Image _image = null;
	[SerializeField]
	UnityEngine.UI.Text _text = null;

	[SerializeField]
	private BoostEffect _boost;

	// エネルギー総量
	private float _energy;
	// 現在のエネルギー量
	private float _nowEnergy;

	private float Energy
	{
		set { _energy = value; }
		get { return _energy; }
	}

	float gage = 1;

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
		isAir = false;
		//AudioManager.instance.PlayBGM(_clip2);
	}

	// プレイヤー、AIでも対応可能なように値をもらって判断
	// Animationで移動
	protected void Walk(Vector2 Axis)
	{
		_boost.StopEffect();

		if (isAir)
		{
			return;
		}
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
		_rigid.velocity = _lastForce;
	}

	private void GroundCheck()
	{
		RaycastHit hit;
		if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out hit, 10, _layerMask, QueryTriggerInteraction.Ignore))
		{
			transform.position = new Vector3(transform.position.x, hit.point.y + 5, transform.position.z);
		}
		else
		{
			_lastForce.y = _rigid.velocity.y;
		}
	}

	protected void Boost(Vector2 Axis)
	{
		_animator.SetBool("Hover", true);
		_animator.SetBool("Walk", false);

		//_slider.value -= 1 * Time.deltaTime * 0.2f;
	}

	// プレイヤー、AIでも対応可能なように値をもらって判断
	protected void Hover(Vector2 Axis)
	{
		_boost.PlayEffect();
		if (isAir)
		{
			return;
		}

		if (Axis.x == 0 && Axis.y == 0)
		{
			return;
		}
		if (_image)
		{
			gage -= Time.deltaTime * 1.2f;
			if (gage < 0.0f)
			{
				gage = 0;
				_image.fillAmount = gage;
			}
			else
			{
				_image.fillAmount = gage / 4;
			}
		}
		GroundCheck();
		AudioManager.instance.PlayOneSE(_clip2);

		_animator.SetBool("Hover", true);
		_animator.SetBool("Walk", false);
		Vector3 forceX = Vector3.zero;
		Vector3 forceY = Vector3.zero;


		if (Axis.x > 0)
		{
			forceX = transform.right * _power * 3;
		}
		else if(Axis.x < 0)
		{
			forceX = transform.right * _power * -3;
		}
		if (Axis.y > 0)
		{
			forceY = transform.forward * _power * 3;
		}
		else if (Axis.y < 0)
		{
			forceY = transform.forward * _power * -3;
		}

		_lastForce = forceY + forceX;
		_rigid.velocity = _lastForce;
	}

	protected void Jump()
	{
		Debug.Log("ジャンプが呼ばれている");
		if (isAir)
		{
			return;
		}
		isAir = true;
		_rigid.AddForce(Vector3.up * 20, ForceMode.VelocityChange);
	}

	protected void ChangeMode()
	{
		if (_mode == UNIT_MODE.WAIK)
		{
			Debug.Log("ホバーモード");
			_mode = UNIT_MODE.HOVER;
			_boost.SetAnimation(true);
		}
		else if(_mode == UNIT_MODE.HOVER)
		{
			Debug.Log("歩行モード");
			_mode = UNIT_MODE.BOOST;
		}
		else
		{
			_animator.SetBool("Hover", true);
			_animator.SetBool("Walk", false);
			Debug.Log("歩行モード");
			_mode = UNIT_MODE.WAIK;
			_boost.SetAnimation(false);
		}
	}

	private void HiBoost(Vector2 axis)
	{
		float moveSpeed = axis.x;
		if(axis.x == 0)
		{
			moveSpeed = 0;
		}
		if (axis.x > 0 || axis.x < 0)
		{
			_rigid.useGravity = false;
			_rigid.velocity = transform.forward * _power * 7 + (transform.right * moveSpeed * _power);
		}
	}

	protected void Brake()
	{
		_rigid.useGravity = true;
		_boost.StopEffect();
		_animator.SetBool("Walk", false);
		_animator.SetBool("Hover", false);
		//_slider.value += 1 * Time.deltaTime;
		_lastForce = Vector3.zero;
		//if(_rigid.velocity.magnitude > 1)
		//{
		//	_rigid.velocity = Vector3.zero;
		//}
		//_lastForce.y = _rigid.velocity.y;
		//_rigid.velocity = new Vector3(_lastForce.x, _lastForce.y + _rigid.velocity.y, _lastForce.z);
	}

	protected void System(Vector2 Axis)
	{
		gage += Time.deltaTime * 1;
		switch (_mode)
		{
			case UNIT_MODE.WAIK:
				Walk(Axis);
				if (_image)
				{
					if (gage > 1.0f)
					{
						gage = 1;
					}
					_image.fillAmount = gage / 4;
				}
				break;
			case UNIT_MODE.BOOST:
				HiBoost(Axis);
				break;
			case UNIT_MODE.HOVER:
				if (_image)
				{
					if (gage > 1.0f)
					{
						gage = 1;
					}
					_image.fillAmount = gage / 4;
				}
				Hover(Axis);
				break;
			case UNIT_MODE.MAX:
			default:
				break;
		}
		float TextName = gage * 100;
		if (_text)
		{
			_text.text = TextName.ToString("000.00");
		}
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Ground")
		{
			Debug.Log("地面");
			isAir = false;
		}
	}
	private void OnCollisionStay(Collision collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			isAir = false;
		}
	}

	private void OnDrawGizmos()
	{
		RaycastHit hit;

		if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out hit, 100, _layerMask, QueryTriggerInteraction.Ignore))
		{
			Gizmos.color = Color.red;
			Gizmos.DrawRay(transform.position, Vector3.down * hit.distance);
			Gizmos.DrawWireSphere(transform.position + (-transform.up * hit.distance), 0.5f);

		}
		else
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawRay(transform.position, Vector3.down * 10);
			Gizmos.DrawWireSphere(transform.position + (-transform.up * 10), 0.5f);
		}
	}
}
