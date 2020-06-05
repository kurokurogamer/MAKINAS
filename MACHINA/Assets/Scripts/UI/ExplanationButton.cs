using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplanationButton : MonoBehaviour
{
	[SerializeField, TextArea(2,5), Tooltip("説明テキスト一覧")]
	private List<string> _string = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
