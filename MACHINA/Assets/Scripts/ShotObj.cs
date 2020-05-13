using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotObj : MonoBehaviour
{
    [SerializeField]
    private float _shotSpeed = 1f;
	[SerializeField]
	private string _tagName = "";

    void Start()
    {
        Destroy(gameObject, 3);
    }

    void Update()
    {
        transform.position += transform.forward * _shotSpeed;
    }

	private void OnTriggerEnter(Collider other)
	{

		if (other.tag == _tagName)
		{
			var hp = other.transform.GetComponent<HitPoint>();
			Debug.Log(hp.gameObject);
			hp.HP = hp.HP - 10;
			Debug.Log("衝突した");
			Destroy(gameObject);
		}
	}

}
