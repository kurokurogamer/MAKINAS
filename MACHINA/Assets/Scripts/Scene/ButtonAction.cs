using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
	[SerializeField]
	private string _scnenName = "";
	// Start is called before the first frame update
    void Start()
    {
    }

	public void Active()
	{
		SceneCtl.instance.LoadScene(_scnenName);

	}

}
