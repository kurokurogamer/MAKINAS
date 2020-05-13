using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// テストコード
public class PlayerFollow : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 2;
    [SerializeField]
    private GameObject playerObj;
    private Vector3 offset;

    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        offset = this.transform.position - playerObj.transform.position;
    }

    private void LateUpdate()
    {
        var nowPosition = this.transform.position;
        var newPos = Vector3.Lerp(transform.position, playerObj.transform.position + offset, moveSpeed * Time.deltaTime);
        this.transform.position = newPos;
    }
}
