using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Weapon
{
    [SerializeField, Tooltip("最低角度")]
    private int _min = 0;
    [SerializeField, Tooltip("最大角度")]
    private int _max = 35;

    private GameObject _target;
    private GameObject[] _point;
    private float _degree;
    private Vector3 _firstRotate = Vector3.zero;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _point = new GameObject[1];
        _point[0] = this.gameObject;
        _degree = 0;
    }
    private void Atan2()
    {
        if (_target == null)
        {
            return;
        }

        Vector3 dt = _target.transform.position - transform.position;

        float rad2 = Mathf.Atan2(dt.y, dt.z);
        _degree = rad2 * Mathf.Rad2Deg;

    }

    // Update is called once per frame
    void Update()
    {
        ReLoad();

        _nowWaitTime += Time.deltaTime;
        _target = Camera.main.GetComponent<LockOnSystem>().GetTarget;
        Atan2();
        _point[0].transform.localRotation = Quaternion.Euler(-_degree, 0, 0);
        Attack();
    }
}
