using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSceneMove : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> _enemyList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
		foreach(Transform trans in transform)
		{
			_enemyList.Add(trans.gameObject);
		}
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject enemy in _enemyList)
		{
			if(enemy.activeInHierarchy == true)
			{
				return;
			}
		}
		SceneCtl.instance.LoadScene("MenuScene2");
    }
}
