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
	[SerializeField]
	private RectTransform _ui = null;

    // Use this for initialization
    void Start()
    {
		_lockOnSystem = Camera.main.GetComponent<LockOnSystem>();
    }

	// Update is called once per frame
	void Update()
	{
		if (_lockOnSystem == null)
		{
			return;
		}



		// ターゲットの数よりカーソルの数が少なければ
		while (_cursorList.Count < _lockOnSystem.TargetList.Count)
		{
			GameObject cursor = Instantiate(_cursor.gameObject);
			cursor.transform.SetParent(transform);
			_cursorList.Add(cursor);
		}
		Debug.Log(_lockOnSystem.TargetList.Count + "画面内の敵数");

		for (int i = 0; i < _cursorList.Count; i++)
		{
			_cursorList[i].SetActive(false);
		}
		_ui.gameObject.SetActive(false);
		// ロックオン座標の更新
		for (int i = 0; i < _lockOnSystem.TargetList.Count; i++)
		{

			float _distance = Vector3.Distance(_lockOnSystem.TargetList[i].transform.position, _lockOnSystem.Player.transform.position);

			if (_distance < 100.0f)
			{
				_cursorList[i].SetActive(true);

				// 敵のワールド座標をスクリーン座標に変換し代入
				Vector2 postion = RectTransformUtility.WorldToScreenPoint(Camera.main, _lockOnSystem.TargetList[i].transform.position);
				_cursorList[i].transform.position = new Vector3(postion.x, postion.y, 0f);

				// メインターゲットのカラー変更
				if (_lockOnSystem.GetTarget == _lockOnSystem.TargetList[i])
				{
					_ui.gameObject.SetActive(true);
					_ui.transform.position = new Vector3(postion.x, postion.y, 0f);
					_cursorList[i].GetComponent<Image>().color = _color1;
				}
				else
				{
					_cursorList[i].GetComponent<Image>().color = Color.white;
				}
			}
		}
	}

    private void LateUpdate()
    {

    }
}
