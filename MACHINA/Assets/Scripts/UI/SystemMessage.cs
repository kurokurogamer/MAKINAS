using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemMessage : MonoBehaviour
{
	[SerializeField, Tooltip("仮Message設定")]
	private List<string> _message = new List<string>();
	[SerializeField, Tooltip("")]
	private float _changeTime = 5.0f;
	private float _nowTime = 0;
	private int ID;
	private Text _text;
    // Start is called before the first frame update
    void Start()
    {
		ID = 0;
		_text = GetComponent<Text>();
    }

	private void String()
	{
		_nowTime += Time.deltaTime;
		if(_nowTime > _changeTime)
		{
			ID++;
			if(ID >= _message.Count)
			{
				ID = 0;
			}
			_nowTime = 0;
		}
		_text.text = _message[ID];
	}

    // Update is called once per frame
    void Update()
    {
		String();
    }
}
