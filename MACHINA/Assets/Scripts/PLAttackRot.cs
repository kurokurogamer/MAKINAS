using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAttackRot : MonoBehaviour
{
	private GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {
		player = Camera.main.GetComponent<LockOnSystem>().Player;   
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 rot = transform.eulerAngles;
		if(player)
		{
			transform.LookAt(player.transform);
			transform.rotation = Quaternion.Euler(rot.x, transform.eulerAngles.y, rot.z);
		}
    }
}
