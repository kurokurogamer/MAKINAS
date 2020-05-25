using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	private static ObjectPool _instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public void ReleaseGameObject(GameObject obj)
	{
		obj.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
