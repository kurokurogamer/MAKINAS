using UnityEngine;

public class LockOnSystem : MonoBehaviour
{
    [SerializeField, Tooltip("システムの有効化")]
    private bool systemActive = false;
    [SerializeField]
    private LayerMask _layer = 0;
    [SerializeField]
    private GameObject _enemy = null;
    // 現在のターゲット名
    private GameObject _target = null;
    [SerializeField, Tooltip("")]
    private int LOCK_ON_TIME = 1;
    // 現在の時間
    private float nowTime;
    // ロックオン検知変数
    private bool _isLockOn = false;
    [SerializeField, Tooltip("サークルの大きさ")]
    private float _circleScale = 400;
    [SerializeField, Tooltip("ロックオン可能距離")]
    private float _distance = 100f;

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
        _enemyTarget = _enemy.GetComponent<CheckRender>();
    }

    private void LockCheck()
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, _enemy.transform.position, out hit, _layer, QueryTriggerInteraction.Ignore))
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, _enemy.transform.position);

            screenPoint.x = screenPoint.x - (Screen.width / 2);
            screenPoint.y = screenPoint.y - (Screen.height / 2);


            // ロックオンサークル内の場合
            if (screenPoint.magnitude <= _circleScale)
            {
                nowTime += Time.deltaTime;
                _target = _enemy;
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
