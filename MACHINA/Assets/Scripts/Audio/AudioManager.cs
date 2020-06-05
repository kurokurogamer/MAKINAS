using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance = null;

	private AudioSource _source;
	private Coroutine _coroutine;
	[Header("MixerGroup")]
	[SerializeField, Tooltip("mixer")]
	private AudioMixer _mixer = null;
	[SerializeField, Tooltip("SEグループ")]
	private AudioMixerGroup _mixerSEGroup = null;
	[SerializeField, Tooltip("BGM")]
	private AudioMixerGroup _mixerBGMGroup = null;
	[SerializeField, Tooltip("Voiceグループ")]
	private AudioMixerGroup _mixerVoiceGroup = null;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		_source = GetComponent<AudioSource>();
		_coroutine = null;
    }

	public void PlaySE(AudioClip clip)
	{
		_source.outputAudioMixerGroup = _mixerSEGroup;
		_source.PlayOneShot(clip);
	}

	public void PlayOneSE(AudioClip clip)
	{
		if(!_source.isPlaying)
		{
			_source.PlayOneShot(clip);
		}
	}

	public void PlayBGM(AudioClip clip)
	{
		_source.loop = true;
		StartCoroutine(BGMLoop(clip));
	}

	public void StopSE()
	{
		_source.Stop();
	}

	public void StopBGM()
	{

		_source.Stop();
	}

	private IEnumerator BGMLoop(AudioClip clip)
	{
		while(true)
		{
			if(!_source.isPlaying)
			{
				_source.PlayOneShot(clip);
			}
			yield return null;
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
