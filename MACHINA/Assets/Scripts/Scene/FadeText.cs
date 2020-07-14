using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : FadeUI
{
	private Text _text;
    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
		_text = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update()
    {
		if (!_text.enabled)
		{
			_alpha = _gage.max;
		}

		Fade();
		if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3"))
		{
			FadeSkip();
		}
		_text.color = new Color(_text.color.r, _text.color.g, _text.color.b, Alpha);
	}
}
