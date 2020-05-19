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
    [SerializeField, Tooltip("敵")]
    private GameObject _enemy = null;
	[SerializeField]
	private GameObject _enemyCR = null;
    // 現在のターゲット名
    private GameObject _target = null;
    [SerializeField, Tooltip("")]
    private float LOCK_ON_TIME = 1.0f;
    // 現在の時間
    private float nowTime;
    // ロックオン検知変数
    private bool _isLockOn = false;
    [SerializeField, Tooltip("サークルの大きさ")]
    private float _circleScale = 400;
    [SerializeField, Tooltip("ロックオン可能距離")]
    private float _distance = 100f;
	[SerializeField, Tooltip("距離のテキスト")]
	private Text _text;
	Vector2 screenPoint;
	
	public static List<GameObject> _targetList = new List<GameObject>();

	private CheckRender _enemyTarget;

    public float GetNowTime
    {
        get { return nowTime; }
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
        _enemyTarget = _enemyCR.GetComponent<CheckRender>();
    }

    private void LockCheck()
    {
		RaycastHit hit;

		if(!_enemyTarget.IsRendFlag)
		{
			Debug.Log("敵が画面内にいない");
			return;
		}
		
		if (Physics.Linecast(transform.position, _enemy.transform.position, out hit, _layer, QueryTriggerInteraction.Ignore))
        {
			Debug.Log("敵発見");
			screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, _enemy.transform.position);

			screenPoint.x = screenPoint.x - (Screen.width / 2);
			screenPoint.y = screenPoint.y - (Screen.height / 2);

			// ロックオンサークル内の場合
			if (screenPoint.magnitude <= _circleScale)
			{
				Vector3 vector = transform.position - _enemy.transform.position;
				nowTime += Time.deltaTime;
				_target = _enemy;
				_text.text = vector.magnitude.ToString("000.00");
				return;
			}
		}
        nowTime = 0;
        _target = null;
    }

    // Update is called once per frame
    void Update()
    {
        LockCheck();
        // ロックオン時間を越えているなら
        if (nowTime >= LOCK_ON_TIME)
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
        
    }
}
