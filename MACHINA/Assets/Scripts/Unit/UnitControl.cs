﻿using System.Collections;
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
	private AudioClip _clip2 = null;
	[SerializeField]
	UnityEngine.UI.Image _image = null;

	[SerializeField]
	private BoostEffect _boost;
	[SerializeField]
	private Vector3 _gravity;

	// エネルギー総量
	private float _energy;
	// 現在のエネルギー量
	private float _nowEnergy;

	private float Energy
	{
		set { _energy = value; }
		get { return _energy; }
	}


	// Use this for initialization
	protected virtual void Start()
	{
		_rigid = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();
		_lastForce = Vector3.zero;
		_energy = 50000;
		_nowEnergy = _energy;
		foreach (Transform child in transform)
		{
			if (child.TryGetComponent(out Weapon weapon))
			{
				_weaponList.Add(weapon);
			}
		}
		isAir = false;
	}

	// プレイヤー、AIでも対応可能なように値をもらって判断
	// Animationで移動
	protected void Walk(Vector2 Axis)
	{
		_boost.StopEffect();

		// 入力がない場合は処理をスキップする
		if (Axis.x == 0 && Axis.y == 0)
		{
			return;
		}
		_animator.SetBool("Hover", false);
		_animator.SetBool("Walk", true);
		Vector3 forceX = Vector3.zero;
		Vector3 forceY = Vector3.zero;

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
		//_rigid.velocity = _lastForce;
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
			//_lastForce.y = _rigid.velocity.y;
		}
	}

	protected void Boost(Vector2 Axis)
	{
		_animator.SetBool("Hover", true);
		_animator.SetBool("Walk", false);
	}

	// プレイヤー、AIでも対応可能なように値をもらって判断
	protected void Hover(Vector2 Axis)
	{

		if (Axis.x == 0 && Axis.y == 0)
		{
			return;
		}
		_hobaEffect.Emit(1);
		_boost.PlayEffect();

		Camera.main.GetComponent<Radial>().Strength = Mathf.Lerp(Camera.main.GetComponent<Radial>().Strength, 0.05f, Time.deltaTime * 5);

		if (_image)
		{
			_nowEnergy -= 200;
			if (_nowEnergy < 0)
			{
				_nowEnergy = 0;
				_mode = UNIT_MODE.WAIK;
			}
		}
		AudioManager.instance.PlayOneSE(_clip2);

		_animator.SetBool("Hover", true);
		_animator.SetBool("Walk", false);
		Vector3 forceX = Vector3.zero;
		Vector3 forceY = Vector3.zero;


		if (Axis.x > 0)
		{
			forceX = transform.right * _power * 10;
		}
		else if (Axis.x < 0)
		{
			forceX = transform.right * _power * -10;
		}
		if (Axis.y > 0)
		{
			forceY = transform.forward * _power * 10;
		}
		else if (Axis.y < 0)
		{
			forceY = transform.forward * _power * -10;
		}

		_lastForce = forceY + forceX;
		//_rigid.velocity = _lastForce;
	}

	protected void Jump()
	{
		Debug.Log("ジャンプが呼ばれている");
		isAir = true;
		_rigid.AddForce(Vector3.up * _power * 300, ForceMode.Acceleration);
	}

	protected void ChangeMode()
	{
		if (_mode == UNIT_MODE.WAIK)
		{
			Debug.Log("ホバーモード");
			_mode = UNIT_MODE.HOVER;
			_boost.SetAnimation(true);
		}
		else if (_mode == UNIT_MODE.HOVER)
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
		_boost.PlayEffect();

		float moveSpeed = axis.x;
		if (axis.x == 0)
		{
			moveSpeed = 0;
		}
		if (axis.x > 0 || axis.x < 0)
		{
			_rigid.velocity = transform.forward * _power * 10 + (transform.right * moveSpeed * _power);
		}
	}

	protected void Brake()
	{

		if (Camera.main.GetComponent<Radial>().Strength < 0.001f)
		{
			Camera.main.GetComponent<Radial>().Strength = 0;
		}
		else
		{
			Camera.main.GetComponent<Radial>().Strength = Mathf.Lerp(Camera.main.GetComponent<Radial>().Strength, 0, Time.deltaTime * 5);
		}

		_boost.StopEffect();
		_animator.SetBool("Walk", false);
		_animator.SetBool("Hover", false);
		_lastForce = Vector3.zero;
	}

	protected void System(Vector2 Axis)
	{
		_nowEnergy += 100;
		if (_nowEnergy > _energy)
		{
			_nowEnergy = _energy;
		}
		switch (_mode)
		{
			case UNIT_MODE.WAIK:
				Walk(Axis);
				break;
			case UNIT_MODE.BOOST:
				HiBoost(Axis);
				break;
			case UNIT_MODE.HOVER:
				Hover(Axis);
				break;
			case UNIT_MODE.MAX:
			default:
				break;
		}
		if (_nowEnergy != 0)
		{
			_image.fillAmount = _nowEnergy / _energy / 4;
		}
		else
		{
			_image.fillAmount = 0;
		}

		_rigid.AddForce(_gravity + _lastForce * 10, ForceMode.Acceleration);

		Vector3 velocitySpeed = _rigid.velocity;
		if (_rigid.velocity.x > 30)
		{
			velocitySpeed.x = 30;
		}
		else if (_rigid.velocity.x < -30)
		{
			velocitySpeed.x = -30;
		}
		if (_rigid.velocity.z > 30)
		{
			velocitySpeed.z = 30;
		}
		else if (_rigid.velocity.z < -30)
		{
			velocitySpeed.z = -30;
		}
		if (_mode != UNIT_MODE.BOOST)
		{
			_rigid.velocity = velocitySpeed;
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
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			if (isAir)
			{
				_boostEffect.Play();
			}

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
