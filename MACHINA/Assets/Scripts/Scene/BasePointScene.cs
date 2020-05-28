using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePointScene : MonoBehaviour
{
	private List<BasePoint> _childList;
    // Start is called before the first frame update
    void Start()
    {
		_childList = new List<BasePoint>();

		foreach (Transform trans in transform)
		{
			_childList.Add(trans.GetComponent<BasePoint>());
		}
    }

	private void Check()
	{
		foreach(BasePoint point in _childList)
		{
			if(point.Check == false)
			{
				return;
			}
		}
		SceneCtl.instance.SceneChange(SceneCtl.SceneList.RESULT);
	}

    // Update is called once per frame
    void Update()
    {
		Check();
    }
}
