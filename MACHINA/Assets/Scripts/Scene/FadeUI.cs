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

	private enum FADE_MODE
	{
		IN,		// フェイドイン
		OUT,	// フェイドアウト
		NON
	}

	[SerializeField]
	private float _fadeSpeed = 1.0f;
	// フェイド状態
	[SerializeField, Tooltip("フェイド状態:IN=不透明に,OUT:透過に")]
	private FADE_MODE _mode = FADE_MODE.IN;
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

	// Start is called before the first frame update
	protected virtual void Start()
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

	protected virtual void OnEnable()
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
