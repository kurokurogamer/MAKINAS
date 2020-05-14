using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnUI : MonoBehaviour
{

    [SerializeField]
    private LockOnSystem _lockOnSystem = null;
    // カーソルのリスト
    [SerializeField]
    private List<GameObject> _AimSet = new List<GameObject>();
    [SerializeField]
    
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_lockOnSystem.GetTarget != null)
        {
            Vector2 postion = RectTransformUtility.WorldToScreenPoint(Camera.main, _lockOnSystem.GetTarget.transform.position);
            // ロックオンカーソルの移動
            _AimSet[0].transform.position = new Vector3(postion.x, postion.y, 0f);
            _AimSet[1].transform.position = new Vector3(postion.x, postion.y, 0f);
        }

        if (0 < _lockOnSystem.GetNowTime)
        {
            _AimSet[2].SetActive(true);
            if (_lockOnSystem.GetIsLockOn)
            {
                Debug.Log("ロックオン完了");
                // ロックオン完了
                _AimSet[0].SetActive(true);
                _AimSet[1].SetActive(false);

            }
            else
            {
                // ロックオン中
                Debug.Log("ロックオン中");
                _AimSet[0].SetActive(false);
                _AimSet[1].SetActive(true);
            }
        }
        else
        {
            // ロックオンサークル外
            _AimSet[0].SetActive(false);
            _AimSet[1].SetActive(false);
            _AimSet[2].SetActive(false);
        }

    }

    private void LateUpdate()
    {
        if (_lockOnSystem.GetTarget != null)
        {
            Vector2 postion = RectTransformUtility.WorldToScreenPoint(Camera.main, _lockOnSystem.GetTarget.transform.position);
            // ロックオンカーソルの移動
            _AimSet[2].transform.position = new Vector3(postion.x, postion.y, 0f);
        }
    }
}
