using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountTo : MonoBehaviour
{
    [SerializeField]
    private float _secondTime = 3.0f;
    private float _nowTime;
    // Start is called before the first frame update
    void Start()
    {
        _nowTime = 0;
    }

    private void Count(int i)
    {
        _nowTime += Time.deltaTime;
        if (_nowTime > _secondTime)
        {
            _nowTime = 3.0f;
        }
        Debug.Log((int)(300 * _nowTime / _secondTime));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
