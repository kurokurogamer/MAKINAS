using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance = null;

	private AudioSource _source;
	private Coroutine _coroutine = null;
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
		_source = GetComponent<AudioSource>();
	}

	// Start is called before the first frame update
	void Start()
    {
		_coroutine = null;
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
		if (!_source.isPlaying)
		{
			_audioSourceList[0].PlayOneShot(clip);
		}
	}

	public void PlayBGM(AudioClip clip)
	{
		StartCoroutine(BGMLoop(clip));
	}

	public void PlayVoice(AudioClip clip)
	{
		_audioSourceList[2].Play();
	}

	public void PlayOneVoice(AudioClip clip)
	{
		_audioSourceList[2].PlayOneShot(clip);
	}

	public void StopSE()
	{
		_audioSourceList[0].Stop();
	}

	public void StopBGM()
	{

		//_source.Stop();
		_audioSourceList[1].Stop();

		StopAllCoroutines();
	}

	public void StopVoice()
	{
		_audioSourceList[2].Stop();

	}

	private IEnumerator BGMLoop(AudioClip clip)
	{
		while(true)
		{
			if(!_audioSourceList[1].isPlaying)
			{
				_audioSourceList[1].PlayOneShot(clip);
			}
			yield return null;
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
