using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGage : MonoBehaviour
{
    private float point = 10000;
    Image _image = null;
    PlayerSt main;

    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main.GetComponent<PlayerSt>();
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_image)
        {
            _image.fillAmount = main.HP / 10000;
        }
    }
}
