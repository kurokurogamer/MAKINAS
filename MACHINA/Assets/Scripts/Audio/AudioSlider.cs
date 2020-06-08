using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
	[SerializeField, Tooltip("mixer")]
	private AudioMixer _mixer = null;
	[SerializeField, Tooltip("SEグループ")]
	private AudioMixerGroup _mixerSEGroup = null;
	[SerializeField, Tooltip("BGM")]
	private AudioMixerGroup _mixerBGMGroup = null;
	[SerializeField, Tooltip("Voiceグループ")]
	private AudioMixerGroup _mixerVoiceGroup = null;
	private List<Slider> _sliders;
	// Start is called before the first frame update
	void Start()
    {
		_sliders = new List<Slider>();
        foreach(Transform trans in transform)
		{
			if(trans.TryGetComponent(out Slider slider))
			{
				_sliders.Add(slider);
			}
		}
    }

	private void SetVolume()
	{
		_mixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, _sliders[0].value));
		_mixer.SetFloat("SEVolume", Mathf.Lerp(-80, 0, _sliders[1].value));
		_mixer.SetFloat("BGMVolume", Mathf.Lerp(-80, 0, _sliders[2].value));
		_mixer.SetFloat("VoiceVolume", Mathf.Lerp(-80, 0, _sliders[3].value));
	}

	// Update is called once per frame
	void Update()
    {
		SetVolume();
    }
}
