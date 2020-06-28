using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 自機、敵変わらずに使用するヒットポイントスクリプト
public class HitPoint : MonoBehaviour
{
	[SerializeField, Tooltip("耐久値")]
	private int _hitPoint = 100;
	[SerializeField, Tooltip("バリア耐久値")]
	private int _barrierPoint= 0;
	[SerializeField, Tooltip("バリア")]
	private GameObject _barrier;
	private float _nowTime;
	private Coroutine _coroutine = null;
	[SerializeField]
	private UnityEngine.UI.Image _image = null;
	[SerializeField]
	private UnityEngine.UI.Text _text = null;
	private int _firstPoint;


	public int HP
	{
		get { return _hitPoint; }
		set { _hitPoint = value; }
	}

	public int BHP
	{
		get { return _barrierPoint; }
		set { _barrierPoint = value; }
	}

	private void Start()
	{
		_firstPoint = _hitPoint;
	}

	public void Damage(int value, ShotObj.BULLET_TYPE type)
	{
		if(_barrier != null && type == ShotObj.BULLET_TYPE.BEAM)
		{
			if (_barrierPoint > 0)
			{
				StopAllCoroutines();
				StartCoroutine(Active());
				_barrierPoint -= value;
				if(_barrierPoint == 0)
				{
					StopAllCoroutines();
					_barrier.SetActive(false);
					_barrier = null;
				}
			}
		}
		else
		{
			_hitPoint -= value;
			if(_hitPoint < 0)
			{
				gameObject.SetActive(false);
			}
		}
	}

	private IEnumerator Active()
	{
		_barrier.SetActive(true);
		yield return new WaitForSeconds(0.3f);
		_barrier.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        if(_hitPoint <= 0)
		{
			transform.gameObject.SetActive(false);
		}
		if (_image)
		{
			float gage = ((float)_hitPoint / (float)_firstPoint);
			_image.fillAmount = (gage) / 4;
			_text.text = _hitPoint.ToString();
		}
    }
}
