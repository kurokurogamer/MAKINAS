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
	[SerializeField, Tooltip("追従")]
	private GameObject _cursorSet = null;
    // Use this for initialization
    void Start()
    {
		_lockOnSystem = Camera.main.GetComponent<LockOnSystem>();
    }

    // Update is called once per frame
    void Update()
    {

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

    }

    private void LateUpdate()
    {
		if (_lockOnSystem.GetTarget != null)
        {
            Vector2 postion = RectTransformUtility.WorldToScreenPoint(Camera.main, _lockOnSystem.GetTarget.transform.position);
			// ロックオンカーソルの移動
			_cursor.transform.position = new Vector3(postion.x, postion.y, 0f);
			_cursorSet.transform.position = _cursor.transform.position;
        }
    }
}
