using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Weapon
{
    [SerializeField, Tooltip("最低角度")]
    private int _min = 0;
    [SerializeField, Tooltip("最大角度")]
    private int _max = 35;

    // ロックオンシステム
    private LockOnSystem _lockOn;
    // 標的となるターゲット
    private GameObject _target;
    // 回転させるポイント
    private GameObject _rotPoint;
    private float _degree;
    private Vector3 _firstRotate;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _lockOn = Camera.main.GetComponent<LockOnSystem>();

        _target = null;
        _rotPoint = this.gameObject;
        _degree = 0;
        _firstRotate = Vector3.zero;
    }
    private void Atan2()
    {
        _target = _lockOn.GetTarget;
        if (_target == null)
        {
            return;
        }

        Vector3 dt = _target.transform.position - transform.position;

        float rad2 = Mathf.Atan2(dt.y, dt.z);
        _degree = rad2 * Mathf.Rad2Deg;
        _rotPoint.transform.localRotation = Quaternion.Euler(-_degree, 0, 0);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Atan2();
    }
}
