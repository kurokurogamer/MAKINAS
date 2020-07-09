using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : FadeUI
{
	// フェイドするイメージ
	private Image _image;
	// Start is called before the first frame update
	protected override void Start()
    {
		base.Start();
		_image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
		Fade();
		if(!_image.enabled)
		{
			_alpha = _gage.max;
		}
		_image.color = new Color(_image.color.r, _image.color.g, _image.color.b, Alpha);
	}

}
