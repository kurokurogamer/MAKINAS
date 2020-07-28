using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;

public class CreateMapDate : MonoBehaviour
{
	[SerializeField]
	Texture2D _texture;
	Texture2D _drawTexture;
	[SerializeField, Tooltip("判定するレイヤー")]
	private LayerMask _layerMask = 0;
	[SerializeField]
	private Vector2 _areaSize = Vector2.zero;
	[SerializeField]
	private float _sum = 100f;
	RaycastHit hit;
	[SerializeField]
	private Material _mat;
	

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(SetMapDate());
	}

	private IEnumerator SetMapDate()
	{
		Time.timeScale = 0;

		Debug.Log("開始");
		_drawTexture = new Texture2D(_texture.width, _texture.height, TextureFormat.RGBA32, false);
		_drawTexture.filterMode = FilterMode.Point;
		Color[] colors = _drawTexture.GetPixels();
		for(int i = 0; i< colors.Length; i++)
		{
			colors[i] = Color.black;
		}
		_drawTexture.SetPixels(colors);
		_drawTexture.Apply();
		_mat.mainTexture = _drawTexture;

		for (int y = 0; y < _areaSize.y; y++)
		{
			if (y % 5 == 0)
			{
				_drawTexture.Apply();
				_mat.mainTexture = _drawTexture;
				yield return new WaitForSecondsRealtime(0.01f);
			}
			for (int x = 0; x < _areaSize.x; x++)
			{
				if (Physics.Raycast(new Vector3(x - _areaSize.x / 2, 100, y - _areaSize.x / 2), Vector3.down, out hit, 150, _layerMask, QueryTriggerInteraction.Ignore))
				{
					_drawTexture.SetPixel(x, y, new Color(0, 0.5f + (hit.point.y / _sum), 0.5f + (hit.point.y / _sum)));
				}
				else
				{
					_drawTexture.SetPixel(x, y, new Color(0, 0, 0));
				}
			}
		}
		_drawTexture.Apply();
		_mat.mainTexture = _drawTexture;
		Debug.Log("終了");
		Time.timeScale = 1;
		SceneCtl.instance.AddScene("GameUI");
		yield return null;
	}

	private void Update()
	{
	}
}
