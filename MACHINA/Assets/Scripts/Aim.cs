using UnityEngine;

public class Aim : MonoBehaviour
{

    [SerializeField, Tooltip("メイン標準オブジェクト")]
    private GameObject _mainAim = null;
    [SerializeField, Tooltip("サブ標準オブジェクト")]
    private GameObject _subAim = null;
    [SerializeField]
    private float _followSpeed = 1f;
    [SerializeField]
    private GameObject[] _waepon = null;
    private LockOnSystem _lockOnSystem;
    // Use this for initialization
    void Start()
    {
        _lockOnSystem = Camera.main.GetComponent<LockOnSystem>();
    }

    private void Move()
    {
        // Aimにおくれて追従するオブジェクト
        _subAim.transform.position = Vector3.Lerp(_subAim.transform.position, _mainAim.transform.position, Time.deltaTime * _followSpeed);
    }

    private void Rotate()
    {
        GameObject target = _lockOnSystem.GetTarget;
        foreach(var wapon in _waepon)
        {
			if (target != null)
			{
				wapon.transform.LookAt(target.transform);
			}
			else
			{
				wapon.transform.localEulerAngles = Vector3.zero;
			}
        }
    }
    // Update is called once per frame
    void Update()
    {
		Move();
        Rotate(); 
    }
}
