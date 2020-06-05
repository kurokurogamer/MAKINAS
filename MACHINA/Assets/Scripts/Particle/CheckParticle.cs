using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckParticle : MonoBehaviour
{
	private ParticleSystem _particle = null;
    // Start is called before the first frame update
    void Start()
    {
		_particle = GetComponent<ParticleSystem>();
    }

	private void Check()
	{
		if(!_particle.isPlaying)
		{
			gameObject.SetActive(false);
		}
	}

    // Update is called once per frame
    void Update()
    {
		Check();
    }
}
