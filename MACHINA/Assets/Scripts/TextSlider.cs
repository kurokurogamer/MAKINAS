using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSlider : MonoBehaviour
{
	[SerializeField, Tooltip("秒数")]
	private float _secondTime;
	private Text _text;
	private Image _image;
    // Start is called before the first frame update
    void Start()
    {
		_image = GetComponent<Image>();
		foreach(Transform trans in transform)
		{
			if(trans.TryGetComponent(out Text text))
			{
				_text = text;
			}
		}
    }

	public void SetText(string text)
	{
		_text.text = text;
	}

	public void SliderReset()
	{
		_image.fillAmount = 0;
	}

	public void UpSlider()
	{
		_image.fillAmount += _secondTime * Time.deltaTime;
		if (_image.fillAmount > 1)
		{
			Debug.Log(_image.fillAmount + "超えている");
			_image.fillAmount = 1;
		}
	}

    // Update is called once per frame
    void Update()
    {
		UpSlider();
    }
}
