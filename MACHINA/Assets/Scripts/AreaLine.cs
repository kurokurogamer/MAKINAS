using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AreaLine : MonoBehaviour
{
	[SerializeField]
	private Vector3 _size = Vector3.zero;
	[SerializeField]
	private Vector3 _offest = Vector3.zero;
	[SerializeField]
	private GameObject _player = null;
	private LineRenderer _lineRnderer;
	private BoxCollider _boxCollider;
	
    // Start is called before the first frame update
    void Start()
    {
		_lineRnderer = GetComponent<LineRenderer>();
		_boxCollider = GetComponent<BoxCollider>();
		SetPoint();
    }

	private void SetPoint()
	{
		_lineRnderer.positionCount = 6;
		_lineRnderer.SetPosition(0, new Vector3(0, 0, _size.z) + _offest);
		_lineRnderer.SetPosition(1, new Vector3(_size.x,0,_size.z) + _offest);
		_lineRnderer.SetPosition(2, new Vector3(_size.x, 0, -_size.z) + _offest);
		_lineRnderer.SetPosition(3, new Vector3(-_size.x, 0, -_size.z) + _offest);
		_lineRnderer.SetPosition(4, new Vector3(-_size.x, 0, _size.z) + _offest);
		_lineRnderer.SetPosition(5, new Vector3(0, 0, _size.z) + _offest);
		_boxCollider.center = _offest;
		_boxCollider.size = new Vector3(_size.x * 2, _size.y, _size.z * 2);
	}

	private void SetLine()
	{
		
		for(int i = 0; i < _lineRnderer.positionCount; i++)
		{
			_lineRnderer.SetPosition(i, new Vector3(_lineRnderer.GetPosition(i).x, _player.transform.position.y, _lineRnderer.GetPosition(i).z));
		}
	}

    // Update is called once per frame
    void Update()
    {
		SetPoint();
		SetLine();
    }

	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			Debug.Log("エリア外に出ています。");
		}
	}
}
