using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MenuSelect
{
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    public void VolumeChange()
    {
        SetInput();
        if(Select())
        {

        }

    }

    // Update is called once per frame
    void Update()
    {
        SetInput();
        Select();
    }
}
