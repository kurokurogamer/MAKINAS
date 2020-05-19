using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOnUI : MonoBehaviour
{
	// カメラのロックオンシステム
    private LockOnSystem _lockOnSystem = null;
	[SerializeField]
	private Color _color1 = Color.red;
	[SerializeField, Tooltip("target時の色")]
	private Color _color2 = Color.green;
	[SerializeField, Tooltip("cursor本体")]
	private Image _cursor;
    // Use this for initialization
    void Start()
    {
		_lockOnSystem = Camera.main.GetComponent<LockOnSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_lockOnSystem.GetTarget != null)
        {
            Vector2 postion = RectTransformUtility.WorldToScreenPoint(Camera.main, _lockOnSystem.GetTarget.transform.position);
			// ロックオンカーソルの移動
			_cursor.transform.position = new Vector3(postion.x, postion.y, 0f);
        }

        if (0 < _lockOnSystem.GetNowTime)
        {
			_cursor.gameObject.SetActive(true);
            if (_lockOnSystem.GetIsLockOn)
            {
                Debug.Log("ロックオン完了");
				// ロックオン完了
				_cursor.color = _color1;
            }
            else
            {
                // ロックオン中
                Debug.Log("ロックオン中");
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
            //_AimSet[2].transform.position = new Vector3(postion.x, postion.y, 0f);
        }
    }
}
