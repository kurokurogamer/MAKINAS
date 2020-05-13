using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private GameObject _target = null;
    // Use this for initialization
    void Start()
    {

    }

    private void Follow()
    {
        transform.position = _target.transform.position;
    }

    private void Follow(GameObject target)
    {
        transform.position = target.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }
}
