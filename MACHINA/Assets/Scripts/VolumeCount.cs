using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeCount : MonoBehaviour
{
	[SerializeField]
	private Slider _slider = null;
	private Text _text;
    // Start is called before the first frame update
    void Start()
    {
		_text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
		float value = _slider.value * 100;
		value = (int)value;
		_text.text = value.ToString();
    }
}
