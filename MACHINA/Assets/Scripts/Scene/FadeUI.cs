using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeUI : MonoBehaviour
{
	// フェイドするイメージ
	private Image _image;
	[SerializeField]
	private float _fadeSpeed = 1.0f;
	// 自身のカラー
	private Color _myColor;
	// 現在の透明度
	private float _alpha;
	// フェイド状態切り替えフラグ
	private bool _fadeTrigger;
    // Start is called before the first frame update
    void Start()
    {
		_image = GetComponent<Image>();
		_myColor = _image.color;
		_alpha = 1;
		_fadeTrigger = true;
    }

	private void FadeIn()
	{
		_alpha += _fadeSpeed * Time.deltaTime;
		if(_alpha > 1)
		{
			_fadeTrigger = false;
			_alpha = 1;
		}
	}

	private void FadeOut()
	{
		_alpha -= _fadeSpeed * Time.deltaTime;
		if (_alpha < 0)
		{
			_fadeTrigger = true;
			_alpha = 0;
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
