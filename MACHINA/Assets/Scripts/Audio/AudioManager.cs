using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance = null;

	private AudioSource _source;

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
    }

	public void PlaySE(AudioClip clip)
	{
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

			}
			yield return null;
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
