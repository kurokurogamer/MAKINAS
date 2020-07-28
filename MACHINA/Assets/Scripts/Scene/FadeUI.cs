using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUI : MonoBehaviour
{
#pragma warning disable 0649
	[System.Serializable]
	protected struct MinMax
	{
		public float min;
		public float max;
	}

	public enum FADE_MODE
	{
		IN,		// フェイドイン
		OUT,	// フェイドアウト
		NON
	}
	[SerializeField]
	private bool _onAwake = true;
	[SerializeField]
	private float _fadeSpeed = 1.0f;
	private float _nowTime;
	// フェイド状態
	[SerializeField, Tooltip("フェイド状態:IN=不透明に,OUT:透過に")]
	private FADE_MODE _mode = FADE_MODE.IN;
	public FADE_MODE Mode
	{
		get { return _mode; }
		set { _mode = value; }
	}
	[SerializeField, Tooltip("ループフラグ")]
	private bool _loop = true;

	[SerializeField, Tooltip("透過値の最小値と最大値")]
	protected MinMax _gage = new MinMax();

	// 現在の透明度
	protected float _alpha;

	public float Alpha
	{
		get { return _alpha; }
	}

	protected virtual void Awake()
	{
		if(!_onAwake)
		{
			gameObject.SetActive(false);
		}
		_nowTime =1;
		switch (_mode)
		{
			case FADE_MODE.IN:
				_alpha = _gage.min;
				break;
			case FADE_MODE.OUT:
				_alpha = _gage.max;
				break;
			case FADE_MODE.NON:
			default:
				break;
		}
	}

	protected virtual void OnDisable()
	{
		switch (_mode)
		{
			case FADE_MODE.IN:
				_alpha = _gage.min;
				break;
			case FADE_MODE.OUT:
				_alpha = _gage.max;
				break;
			case FADE_MODE.NON:
			default:
				break;
		}
	}

	private void FadeIn()
	{
		_alpha += _fadeSpeed * Time.deltaTime;
		if(_alpha > _gage.max)
		{
			_alpha = _gage.max;
			if (_loop)
			{
				_mode = FADE_MODE.OUT;
			}
		}
		AudioManager.instance.PlaySE();
	}

	private void FadeOut()
	{
		_alpha -= _fadeSpeed * Time.deltaTime;
		if (_alpha < _gage.min)
		{
			_alpha = _gage.min;
			if (_loop)
			{
				_mode = FADE_MODE.IN;
			}
		}
	}

	private void Flash()
	{
		_nowTime += Time.deltaTime;
		if(_nowTime < _fadeSpeed)
		{
			return;
		}
		switch (_mode)
		{
			case FADE_MODE.IN:
				_alpha = _gage.max;
				_mode = FADE_MODE.OUT;
				break;
			case FADE_MODE.OUT:
				_alpha = _gage.min;
				_mode = FADE_MODE.IN;
				break;
			case FADE_MODE.NON:
				break;
			default:
				break;
		}
		_nowTime = 0;
	}

	protected void Fade()
	{
		switch (_mode)
		{
			case FADE_MODE.IN:
				FadeIn();
				break;
			case FADE_MODE.OUT:
				FadeOut();
				break;
			case FADE_MODE.NON:
			default:
				break;
		}
	}

	public void FadeSkip()
	{
		switch (_mode)
		{
			case FADE_MODE.IN:
				_alpha = _gage.max;
				break;
			case FADE_MODE.OUT:
				_alpha = _gage.min;
				break;
			case FADE_MODE.NON:
				break;
			default:
				break;
		}
	}

}
