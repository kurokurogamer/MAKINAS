using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextCamera : MonoBehaviour
{
	private LockOnSystem _lockOnSystem;
	private Text _text;

    // Start is called before the first frame update
    void Start()
    {
		_lockOnSystem = Camera.main.GetComponent<LockOnSystem>();
		_text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
		_text.text = _lockOnSystem.Distance.ToString("000.00");
    }
}
