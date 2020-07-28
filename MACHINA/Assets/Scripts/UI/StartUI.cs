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
	private LockOnUI _lockOnUI = null;
	private CameraMove _cameraMove = null;
	[SerializeField]
	private GameObject _circle = null;
	[SerializeField]
	private GameObject _conpas = null;
	private Compass _script;
	[SerializeField, Tooltip("回転速度")]
	private int _rotateSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
		_rTrans = GetComponent<RectTransform>();
		_firstPos = _rTrans.position;
		transform.position = _firstPos + new Vector3(0, 300, 0);
		if (_circle && _conpas)
		{
			_script = _conpas.GetComponent<Compass>();
			StartCoroutine(Rotate());
		}
		_cameraMove = Camera.main.GetComponent<CameraMove>();
    }

    // Update is called once per frame
    void Update()
    {
		_nowTime += Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, _firstPos, 3);
	}

	private IEnumerator Rotate()
	{
		while (_nowTime < 5)
		{
			_circle.transform.Rotate(new Vector3(0, 0, -_rotateSpeed * Time.deltaTime));
			_conpas.transform.Rotate(new Vector3(0, 0, _rotateSpeed * Time.deltaTime));
			yield return null;
		}
		_script.enabled = true;
		_cameraMove.enabled = true;
		_lockOnUI.enabled = true;
		this.enabled = false;
		yield return null;
	}
}
