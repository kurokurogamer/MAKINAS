using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCursor : MonoBehaviour
{
	[SerializeField, Tooltip("プレイヤー")]
	private GameObject _player = null;
	private RectTransform _rectTrans;
	private RectTransform _parentRect; 
    // Start is called before the first frame update
    void Start()
    {
		_rectTrans = GetComponent<RectTransform>();
		_parentRect = transform.parent.GetComponent<RectTransform>();
		_player = Camera.main.GetComponent<LockOnSystem>()._centerPoint;
    }

	private void MapMove()
	{
		_rectTrans.localPosition = new Vector3(_player.transform.position.x * (_parentRect.sizeDelta.x / 1000.0f), _player.transform.position.z * (_parentRect.sizeDelta.y / 1000.0f), 0);
	}

    // Update is called once per frame
    void Update()
    {
		MapMove();
    }
}
