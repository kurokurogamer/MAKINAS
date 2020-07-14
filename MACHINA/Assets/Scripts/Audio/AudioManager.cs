using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public enum AUDIO_TYPE
	{
		SE,
		BGM,
		VOICE,
		MAX
	}
	public static AudioManager instance = null;

	[Header("MixerGroup")]
	[SerializeField]
	private List<AudioSource> _audioSourceList = new List<AudioSource>();

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
    }

	public void PlaySE()
	{
		_audioSourceList[0].Play();
	}

	public void PlaySE(AudioClip clip)
	{
		_audioSourceList[0].PlayOneShot(clip);
	}

	public void PlayOneSE(AudioClip clip)
	{
		if (!_audioSourceList[0].isPlaying)
		{
			_audioSourceList[0].PlayOneShot(clip);
		}
	}

	public void PlayBGM(AudioClip clip)
	{
		_audioSourceList[1].PlayOneShot(clip);
	}

	public void PlayVoice()
	{
		_audioSourceList[2].Play();
	}

	public void PlayVoice(AudioClip clip)
	{
		_audioSourceList[2].PlayOneShot(clip);
	}

	public void PlayOneVoice(AudioClip clip)
	{
		if (!_audioSourceList[2].isPlaying)
		{
			_audioSourceList[2].PlayOneShot(clip);
		}
	}

	public void Stop(AUDIO_TYPE type)
	{
		if (type != AUDIO_TYPE.MAX)
		{
			_audioSourceList[(int)type].Stop();
		}
	}

	public void StopSE()
	{
		_audioSourceList[0].Stop();
	}

	public void StopBGM()
	{
		StopAllCoroutines();
		_audioSourceList[1].Stop();

	}

	public void StopVoice()
	{
		_audioSourceList[2].Stop();

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
