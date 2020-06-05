using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeUI : MonoBehaviour
{
	[System.Serializable]
	private struct MinMax
	{
		public float min;
		public float max;
	}
	// フェイドするイメージ
	private Image _image;
	[SerializeField]
	private float _fadeSpeed = 1.0f;
	// 自身のカラー
	private Color _myColor;
	// 現在の透明度
	private float _alpha;
	private float _min;
	// フェイド状態切り替えフラグ
	private bool _fadeTrigger;
	[SerializeField]
	private MinMax _gage;
	
    // Start is called before the first frame update
    void Start()
    {
		_image = GetComponent<Image>();
		_myColor = _image.color;
		_alpha = _gage.max;
		_fadeTrigger = true;
    }

	private void FadeIn()
	{
		_alpha += _fadeSpeed * Time.deltaTime;
		if(_alpha > _gage.max)
		{
			_fadeTrigger = false;
			_alpha = _gage.max;
		}
	}

	private void FadeOut()
	{
		_alpha -= _fadeSpeed * Time.deltaTime;
		if (_alpha < _gage.min)
		{
			_fadeTrigger = true;
			_alpha = _gage.min;
		}
	}

	private void Fade()
	{
		if(_fadeTrigger)
		{
			FadeIn();
		}
		else
		{
			FadeOut();
		}
		ChangeColor();
	}

	private void ChangeColor()
	{
		_image.color = new Color(_myColor.r, _myColor.b, _myColor.g, _alpha);
	}

    // Update is called once per frame
    void Update()
    {
		Fade();
    }
}
