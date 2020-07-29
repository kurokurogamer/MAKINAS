using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOnSystem : MonoBehaviour
{
    [SerializeField, Tooltip("レイヤーマスク")]
    private LayerMask _layer = 0;
    // 現在のターゲット名
    private GameObject _target = null;
	[SerializeField, Tooltip("プレイヤー")]
	private GameObject _player = null;
    public GameObject Player
    {
        get { return _player; }
    }

    [SerializeField, Tooltip("ロックオンに要する時間")]
    private float LOCK_ON_TIME = 1.0f;

    // 現在の時間
    private float _nowTime;
    // ロックオン検知変数
    private bool _isLockOn;
    [SerializeField, Tooltip("サークルの大きさ")]
    private float _circleScale = 250;
    [SerializeField, Tooltip("ロックオン可能距離")]
    private float _distance = 100f;
    public float Distance { set { _distance = value; } }
    // ターゲットのスクリーン座標
	private Vector2 _screenPoint;
    [SerializeField, Tooltip("カメラの範囲")]
    private Vector3 _scale = Vector3.one;

	private List<GameObject> _targetList = new List<GameObject>();
	public List<GameObject> TargetList
	{
		get { return _targetList; }
	}
    public float GetNowTime
    {
        get { return _nowTime; }
    }
    public GameObject GetTarget
    {
        get { return _target; }
    }
    public bool GetIsLockOn
    {
        get { return _isLockOn; }
    }
    // Use this for initialization
    void Start()
    {
        _nowTime = 0;
        _isLockOn = false;
        _screenPoint = Vector2.zero;
    }

	private void LockCheck()
	{
		_nowTime += Time.deltaTime;

		bool targetFlag = false;

		RaycastHit hit;
		float distance = 0;
		float centerPoint = _circleScale;
        foreach (GameObject target in _targetList)
        {
            if (Physics.Linecast(_player.transform.position, target.transform.position, out hit, _layer, QueryTriggerInteraction.Ignore))
            {

                _screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, target.transform.position);
                _screenPoint.x = _screenPoint.x - (Screen.width / 2);
                _screenPoint.y = _screenPoint.y - (Screen.height / 2);
                if (_screenPoint.magnitude <= centerPoint)
                {
                    centerPoint = _screenPoint.magnitude;
                    targetFlag = true;
                    distance = Vector3.Distance(target.transform.position, _player.transform.position);
                    //_text.text = distance.ToString("000.00");
                    if (_target == null)
                    {
                        _target = target;
                    }
                }
            }
        }

		if (!targetFlag)
		{
			_nowTime = 0;
			_target = null;
		}
	}

    // Update is called once per frame
    void Update()
    {
        LockCheck();
        // ロックオン時間を越えているなら
        if (_nowTime >= LOCK_ON_TIME)
        {
            _isLockOn = true;
        }
        else
        {
            _isLockOn = false;
        }
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;
        if (Physics.BoxCast(_player.transform.position, _scale * 0.5f, _player.transform.forward, out hit, _player.transform.rotation, 100, _layer, QueryTriggerInteraction.Ignore))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_player.transform.position, _player.transform.forward * hit.distance);
            Gizmos.DrawWireCube(_player.transform.position + _player.transform.forward * hit.distance, _scale);
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_player.transform.position, _player.transform.forward * 100);
            Gizmos.DrawWireCube(_player.transform.position + _player.transform.forward * 100, _scale);
        }

    }
}
