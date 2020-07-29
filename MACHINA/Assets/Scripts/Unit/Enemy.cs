using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	// 移動タイプ
    enum MOVE_TYPE
    {
        X,
        Y,
        Z,
        MAX
    }

    [SerializeField, Tooltip("移動タイプ")]
    private MOVE_TYPE _moveType = MOVE_TYPE.X;
	[SerializeField, Tooltip("武器")]
	private Weapon _weapon = null;
    [SerializeField, Tooltip("移動速度")]
    private float _speed = 0.1f;
    [SerializeField, Tooltip("移動幅")]
    private float _distance = 3;
    // 初期座標
    private Vector3 _firstPos;
    private GameObject _player;
    [SerializeField, Tooltip("検知距離")]
    private float _distancePoint = 100.0f;

    // Use this for initialization
    void Start()
    {
        _player = Camera.main.GetComponent<LockOnSystem>().Player;
        _firstPos = transform.position;
    }

    private void Attack()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if(distance < _distancePoint)
        {
            _weapon.Attack();
        }
    }

    private void Move()
    {
        switch (_moveType)
        {
            case MOVE_TYPE.X:
                transform.position = new Vector3(_firstPos.x + Mathf.Sin(Time.deltaTime * _speed) * _distance, _firstPos.y, _firstPos.z);
                break;
            case MOVE_TYPE.Y:
                transform.position = new Vector3(_firstPos.x, _firstPos.y + Mathf.Sin(Time.deltaTime * _speed) * _distance, _firstPos.z);
                break;
            case MOVE_TYPE.Z:
                transform.position = new Vector3(_firstPos.x, _firstPos.y, _firstPos.z + Mathf.Sin(Time.deltaTime * _speed) * _distance);
                break;
            case MOVE_TYPE.MAX:
				break;
			default:
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }
}