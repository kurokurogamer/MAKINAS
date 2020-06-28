using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePoint : MonoBehaviour
{
	[SerializeField, Tooltip("占拠時カラー")]
	private Color _color = Color.white;
	private ParticleSystem _particle;
	private float _nowTime;
	private bool _test = false;
	private bool _check = false;
	public bool Check
	{
		get { return _check; }
	}
    // Start is called before the first frame update
    void Start()
    {
		foreach (Transform trans in transform)
		{
			_particle = trans.GetComponent<ParticleSystem>();
		}
		_nowTime = 0;
    }

	private void TrigeerTest()
	{
		if(!_test)
		{
			return;
		}
		_nowTime += Time.deltaTime;
		if (_nowTime > 3)
		{
			var main = _particle.main;
			main.startColor = _color;
			_check = true;
		}
	}

	// Update is called once per frame
	void Update()
    {
		TrigeerTest();
    }

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			_test = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			_test = false;
			_nowTime = 0;
		}
	}
}
