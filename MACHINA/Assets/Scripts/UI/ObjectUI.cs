using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectUI : MonoBehaviour
{
    [SerializeField, Tooltip("ボタン用UI")]
    private GameObject _ui = null;
    [SerializeField, Tooltip("オブジェクト名用UI")]
    private Text _text = null;
    [SerializeField, Tooltip("オブジェクト名")]
    private string _objectName = "未定";
    [SerializeField, Tooltip("オフセット")]
    private Vector3[] _offset = new Vector3[2];
    [SerializeField, Tooltip("追加シーン名")]
    private string _sceneName = "";
    [SerializeField]
    private GameObject _blurObj = null;
    [SerializeField]
    private FadeUI _fadeUi = null;
	private bool _check = false;
	private bool _drive = false;
	[SerializeField]
	private CharCtr _chraCtr = null;

    // UI表示の有効判定
    private bool _active = true;
    public bool Active
    {
        get { return _active; }
        set { _active = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(_active == false)
        {
            _ui.SetActive(false);
            _text.gameObject.SetActive(false);
        }
		CheckButton();
	}

	private void CheckButton()
	{
		if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.X))
		{
			if (_sceneName != "" && !_drive && _check)
			{
				//SceneCtl.instance.UnLoadScene(_sceneName);
				_blurObj.SetActive(true);
				_fadeUi.Mode = FadeUI.FADE_MODE.IN;
				//Time.timeScale = 0;
				_drive = true;
				SceneCtl.instance.AddScene(_sceneName);
			}
		}

		if(Input.GetButtonDown("Fire1"))
		{
			_drive = false;
			_blurObj.SetActive(false);
			_fadeUi.Mode = FadeUI.FADE_MODE.OUT;
		}
	}

	private void OnTriggerStay(Collider other)
    {
        if(other.tag != "Player")
        {
            return;
        }
        if (_ui == null || _text == null)
        {
            return;
        }

		_check = true;
		_ui.SetActive(true);
        _text.gameObject.SetActive(true);
        // 敵のワールド座標をスクリーン座標に変換し代入
        Vector2 postion1 = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position + _offset[0]);
        Vector2 postion2 = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position + _offset[1]);

        _ui.transform.position = new Vector3(postion1.x, postion1.y, 0f);
        _text.transform.position = new Vector3(postion2.x, postion2.y, 0f);
        // オブジェクト名を反映
        _text.text = _objectName;
    }

    private void OnTriggerExit(Collider other)
    {
		if (other.tag != "Player")
		{
			return;
		}

		_check = false;

		_ui.SetActive(false);
        _text.gameObject.SetActive(false);
    }
}
