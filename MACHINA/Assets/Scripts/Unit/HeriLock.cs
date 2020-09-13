using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeriLock : MonoBehaviour
{
	private GameObject _player = null;
    // Start is called before the first frame update
    void Start()
    {
		_player = Camera.main.GetComponent<LockOnSystem>().Player;
    }

    // Update is called once per frame
    void Update()
    {
		transform.LookAt(_player.transform);
	}
}
