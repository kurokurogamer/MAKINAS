using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
	private RectTransform _rTrans;
	private Vector3 _firstPos;
	private float _nowTime;
	[SerializeField, Tooltip("ロックオンシステム")]
	private LockOnUI _lockOnUI;
	[SerializeField, Tooltip("カメラシステム")]
	private CameraMove _cameraMove;
    // Start is called before the first frame update
    void Start()
    {
		_rTrans = GetComponent<RectTransform>();
		_firstPos = _rTrans.position;
		transform.position = _firstPos + new Vector3(0, 300, 0);
    }

    // Update is called once per frame
    void Update()
    {
		_nowTime += Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, _firstPos, 3);
		if(_nowTime > 2)
		{
			_cameraMove.enabled = true;
			_lockOnUI.enabled = true;
			this.enabled = false;
		}
    }
}
