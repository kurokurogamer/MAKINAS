using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceUI : MonoBehaviour
{
	[SerializeField]
	private Text _text = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void Message()
	{
		_text.text = "テスト";
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
