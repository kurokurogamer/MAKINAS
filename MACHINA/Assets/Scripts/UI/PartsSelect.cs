using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSelect : MenuSelect
{
	[SerializeField, Tooltip("表示するパーツリスト")]
	private List<GameObject> _partsList = new List<GameObject>();
    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
    }
	
	// パーツアクティブ処理
	private void ActiveParts()
	{
		for (int i = 0; i < _partsList.Count; i++)
		{
			if (i == _id)
			{
				_partsList[i].SetActive(true);
			}
			else
			{
				_partsList[i].SetActive(false);
			}
		}
	}

	protected override void Check()
	{

	}

	// Update is called once per frame
	void Update()
	{
		SetInput();
		Seletct();
		ActiveParts();
    }
}
