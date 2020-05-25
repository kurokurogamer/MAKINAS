using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
	public static ParticleManager instance = null;

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

	private void CreateEffect()
	{
	}

	private void CreateEffect(ParticleSystem particle)
	{
		Instantiate(particle);

	}

	private void Play(ParticleSystem particle)
	{
		particle.Play();
	}

	private void Stop(ParticleSystem particle)
	{
		particle.Stop();
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
