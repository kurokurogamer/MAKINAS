using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSkipButton : MonoBehaviour
{
    private FadeUI _ui;

    // Start is called before the first frame update
    void Start()
    {
        _ui = GetComponent<FadeUI>();
    }

    private void Skip()
    {
        _ui.FadeSkip();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3")
            || Input.GetButtonDown("Mouse0") || Input.GetButtonDown("Mouse1") || Input.GetButtonDown("Mouse2"))
        {
            Skip();
        }

        if (_ui.Alpha == 0)
        {
            this.enabled = false;
        }
    }
}
