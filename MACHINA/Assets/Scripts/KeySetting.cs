using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySetting : MonoBehaviour
{
	public Dictionary<string, string> _button;
	
	// Start is called before the first frame update
	void Start()
	{
		_button = new Dictionary<string, string>();
		_button.Add("ButtonA", "Fire1");
		_button.Add("ButtonB", "Fire2");
		_button.Add("ButtonX", "Fire3");
		_button.Add("ButtonY", "Jump");
		_button.Add("ButtonLB", "LB");
		_button.Add("ButtonRB", "RB");
		_button.Add("ButtonLT", "LT");
		_button.Add("ButtonRT", "RT");
		_button.Add("ButtonSTL", "LStick");
		_button.Add("ButtonSTR", "RStick");
		_button.Add("ButtonMenu", "Menu");
		_button.Add("ButtonView", "View");
	}

	public void Config()
	{
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
