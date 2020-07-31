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
	private Image _cursor = null;
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
                Debug.Log("Cursorの生成");
                Debug.Log(_cursorList.Count + "カーソル");
                Debug.Log(_lockOnSystem.TargetList.Count + "画面内の敵数");

                GameObject cursor = Instantiate(_cursor.gameObject);
                cursor.transform.SetParent(transform);
                _cursorList.Add(cursor);
            }
        }
        if (0 < _lockOnSystem.GetNowTime)
        {
            _cursor.gameObject.SetActive(true);
            if (_lockOnSystem.GetIsLockOn)
            {
                // ロックオン完了
                _cursor.color = _color1;
            }
            else
            {
                // ロックオン中
                _cursor.color = _color2;
            }
        }
        else
        {
            _cursor.gameObject.SetActive(false);
        }
        if (_lockOnSystem != null)
        {
            for (int i = 0; i < _lockOnSystem.TargetList.Count; i++)
            {
                Vector2 postion = RectTransformUtility.WorldToScreenPoint(Camera.main, _lockOnSystem.TargetList[i].transform.position);
                // カーソルの移動
                _cursorList[i].transform.position = new Vector3(postion.x, postion.y, 0f);
            }
        }
    }

    private void LateUpdate()
    {

    }
}
