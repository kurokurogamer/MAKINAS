using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOnUI : MonoBehaviour
{
	// カメラのロックオンシステム
    private LockOnSystem _lockOnSystem = null;
	[SerializeField, Tooltip("ターゲット時カラー")]
	private Color _color1 = Color.red;
	[SerializeField, Tooltip("非ターゲット時カラー")]
	private Color _color2 = Color.green;
	[SerializeField, Tooltip("カーソルのプレハブ")]
	private GameObject _cursor = null;
    private List<GameObject> _cursorList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
		_lockOnSystem = Camera.main.GetComponent<LockOnSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_lockOnSystem)
        {
            // ターゲットの数よりカーソルの数が少なければ
            while (_cursorList.Count < _lockOnSystem.TargetList.Count)
            {
				Debug.Log(_lockOnSystem.TargetList.Count + "画面内の敵数");
                GameObject cursor = Instantiate(_cursor.gameObject);
                cursor.transform.SetParent(transform);
                _cursorList.Add(cursor);
				_cursorList[0].GetComponent<Image>().color = _color1;
            }
        }

        if (_lockOnSystem != null)
        {
			for(int i = 0; i < _cursorList.Count; i++)
			{
				_cursorList[i].SetActive(false);
			}
            for (int i = 0; i < _lockOnSystem.TargetList.Count; i++)
            {
				// 敵のワールド座標をスクリーン座標に変換し代入
				Vector2 postion = RectTransformUtility.WorldToScreenPoint(Camera.main, _lockOnSystem.TargetList[i].transform.position);

				_cursorList[i].SetActive(true);

				if (_lockOnSystem.GetTarget == _lockOnSystem.TargetList[i])
				{
					// カーソルの位置に座標を代入する
					_cursorList[0].transform.position = new Vector3(postion.x, postion.y, 0f);
					if (_lockOnSystem.TargetList.Count <= _cursorList.Count)
					{
						_cursorList[_lockOnSystem.TargetList.Count-1].transform.position = new Vector3(postion.x, postion.y, 0f);
					}
				}
				else
				{
					// カーソルの位置に座標を代入する
					_cursorList[i].transform.position = new Vector3(postion.x, postion.y, 0f);
				}
            }
        }
    }

    private void LateUpdate()
    {

    }
}
