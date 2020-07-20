using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraObj : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _obj;
    void Start()
    {
        _obj = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
