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

	[SerializeField]
	private float _fadeSpeed = 1.0f;
	// 現在の透明度
	protected float _alpha;
	// フェイド状態切り替えフラグ
	private bool _fadeSwitch;
	[SerializeField]
	protected MinMax _gage = new MinMax();
	[SerializeField, Tooltip("開始時フラグ")]
	private bool _onAwake = true;
	[SerializeField, Tooltip("ループフラグ")]
	private bool _loop = true;
	[SerializeField, Tooltip("スキップフラグ")]
	private bool _skip = false;

	protected float Alpha
	{
		get { return _alpha; }
	}
	
    // Start is called before the first frame update
    protected virtual void Start()
    {
		_alpha = _gage.max;
		_fadeSwitch = false;
    }

	private void FadeIn()
	{
		_alpha += _fadeSpeed * Time.deltaTime;
		if(_alpha > _gage.max)
		{
			_fadeSwitch = false;
			_alpha = _gage.max;
			if(!_loop)
			{
				_onAwake = false;
			}
		}
	}

	private void FadeOut()
	{
		_alpha -= _fadeSpeed * Time.deltaTime;
		if (_alpha < _gage.min)
		{
			_fadeSwitch = true;
			_alpha = _gage.min;
			if (!_loop)
			{
				_onAwake = false;
			}
		}
	}

	protected void Fade()
	{
		if(_fadeSwitch && _onAwake)
		{
			FadeIn();
		}
		else
		{
			FadeOut();
		}
	}

	protected void FadeSkip()
	{
		if (_skip)
		{
			_alpha = _gage.min;
			_fadeSwitch = true;
			if (!_loop)
			{
				_onAwake = false;
			}
		}
	}

}
