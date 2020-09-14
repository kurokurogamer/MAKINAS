using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    private float point = 10000;
    Text _text = null;
    PlayerSt main;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main.GetComponent<PlayerSt>();
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        point = main.HP;
        _text.text = point.ToString();
    }
}
