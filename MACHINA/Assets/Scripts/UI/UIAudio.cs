using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
	[SerializeField, Tooltip("音")]
	private AudioClip _clip = null;
	public AudioClip Clip
	{
		get { return _clip; }
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }
	// Update is called once per frame
	void Update()
    {
        
    }
}
