using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGizmos : MonoBehaviour
{
    private enum GIZMOS_TYPE
    {
        CUBE,
        SPHERE,
        MAX
    }

    [SerializeField, Tooltip("デバックフラグ")]
    private bool _debugFlag = true;
    [SerializeField, Tooltip("Debug用の表示Gizmos")]
    private GIZMOS_TYPE _type = GIZMOS_TYPE.CUBE;
    [SerializeField, Tooltip("適用色")]
    private Color _color = Color.white;
	[SerializeField, Tooltip("当たり判定")]
	private BoxCollider _collider = null;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        if(!_debugFlag)
        {
            return;
        }
		Vector3 size = Vector3.zero;
		Vector3 offset = Vector3.zero;

		if(_collider != null)
		{
			size = _collider.size;
			offset = _collider.center;
		}
		else
		{
			size = Vector3.one;
		}
        Matrix4x4 rotationMatrix = transform.localToWorldMatrix;
        Gizmos.matrix = rotationMatrix;
        Gizmos.color = _color;
        switch (_type)
        {
            case GIZMOS_TYPE.CUBE:
                Gizmos.DrawCube(Vector3.zero + offset, size);
                break;
            case GIZMOS_TYPE.SPHERE:
                Gizmos.DrawSphere(Vector3.zero, 0.5f);
                break;
            case GIZMOS_TYPE.MAX:
            default:
                break;
        }
    }
}