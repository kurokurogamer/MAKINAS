using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUI : MonoBehaviour
{
#pragma warning disable 0649
	[System.Serializable]
	private struct MinMax
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
	private MinMax _gage = new MinMax();
		public float i;

	protected float Alpha
	{
		get { return _alpha; }
	}
	
    // Start is called before the first frame update
    protected virtual void Start()
    {
		_alpha = _gage.max;
		_fadeSwitch = true;
    }

	private void FadeIn()
	{
		_alpha += _fadeSpeed * Time.deltaTime;
		if(_alpha > _gage.max)
		{
			_fadeSwitch = false;
			_alpha = _gage.max;
		}
	}

	private void FadeOut()
	{
		_alpha -= _fadeSpeed * Time.deltaTime;
		if (_alpha < _gage.min)
		{
			_fadeSwitch = true;
			_alpha = _gage.min;
		}
	}

	protected void Fade()
	{
		if(_fadeSwitch)
		{
			FadeIn();
		}
		else
		{
			FadeOut();
		}
	}

}
