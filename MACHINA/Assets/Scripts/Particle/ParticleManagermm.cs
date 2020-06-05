using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
	public static ParticleManager instance = null;
	private List<GameObject> _effectList = new List<GameObject>();
	private Dictionary<string, GameObject> _effectDictionary = new Dictionary<string, GameObject>();

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

	private void CreateEffect(ParticleSystem particle)
	{
		Instantiate(particle);
	}

	public void CreateEffect(ParticleSystem particle, Vector3 point)
	{
		Instantiate(particle, point, transform.rotation);
	}

	private void CreateEffect(ParticleSystem particle,float lifetime ,float size)
	{
		Instantiate(particle);
		var main = particle.main;
		main.startLifetime = lifetime;
		main.startSize = size;
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
