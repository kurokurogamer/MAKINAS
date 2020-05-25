using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOnSystem : MonoBehaviour
{
    [SerializeField, Tooltip("システムの有効化")]
    private bool systemActive = false;
    [SerializeField]
    private LayerMask _layer = 0;
    // 現在のターゲット名
    private GameObject _target = null;
	[SerializeField, Tooltip("プレイヤー")]
	private GameObject _player = null;
    [SerializeField, Tooltip("")]
    private float LOCK_ON_TIME = 1.0f;
    // 現在の時間
    private float _nowTime;
    // ロックオン検知変数
    private bool _isLockOn = false;
    [SerializeField, Tooltip("サークルの大きさ")]
    private float _circleScale = 250;
    [SerializeField, Tooltip("ロックオン可能距離")]
    private float _distance = 100f;
	[SerializeField, Tooltip("距離のテキスト")]
	private Text _text = null;
	Vector2 screenPoint;
	
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

				screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, target.transform.position);
				screenPoint.x = screenPoint.x - (Screen.width / 2);
				screenPoint.y = screenPoint.y - (Screen.height / 2);

				if (screenPoint.magnitude <= centerPoint)
				{
					centerPoint = screenPoint.magnitude;
					targetFlag = true;
					distance = target.transform.position.magnitude - _player.transform.position.magnitude;
					_text.text = distance.ToString("000.00");
					_target = target;
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
}
