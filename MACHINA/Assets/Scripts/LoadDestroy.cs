using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDestroy : MonoBehaviour
{
    private void Awake()
    {
        Destroy(this.gameObject);
    }
}
