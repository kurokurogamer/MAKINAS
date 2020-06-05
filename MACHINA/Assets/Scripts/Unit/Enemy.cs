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
    [SerializeField, Tooltip("移動速度")]
    private float _speed = 0.1f;
    [SerializeField, Tooltip("移動幅")]
    private float _distance = 3;
	[SerializeField, Tooltip("発射感覚")]
	private float _whileTime = 1;
	// 現在のカウント
	private float _nowTime;
    // 初期座標
    private Vector3 _firstPos;
	[SerializeField, Tooltip("武器")]
	private Weapon _weapon = null;

    // Use this for initialization
    void Start()
    {
        _firstPos = transform.position;
		_nowTime = 0;
    }

    private void Move()
    {
        switch (_moveType)
        {
            case MOVE_TYPE.X:
                transform.position = new Vector3(_firstPos.x + Mathf.Sin(Time.frameCount * _speed) * _distance, _firstPos.y, _firstPos.z);
                break;
            case MOVE_TYPE.Y:
                transform.position = new Vector3(_firstPos.x, _firstPos.y + Mathf.Sin(Time.frameCount), _firstPos.z);
                break;
            case MOVE_TYPE.Z:
                transform.position = new Vector3(_firstPos.x, _firstPos.y, _firstPos.z + Mathf.Sin(Time.frameCount));
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
    }

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			transform.LookAt(other.transform);
			_nowTime += Time.deltaTime;
			if (_nowTime > _whileTime)
			{
				_weapon.Attack();
				_nowTime = 0;
			}
		}
	}
}