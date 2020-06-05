using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphererTest : MonoBehaviour
{
	RaycastHit hit;
	[SerializeField, Tooltip("レイヤーマスク")]
	private LayerMask _layer = 0;
	[SerializeField]
	bool isEnable = false;

	void OnDrawGizmos()
	{
		if (isEnable == false)
			return;

		var radius = transform.lossyScale.x * 0.5f;

		var isHit = Physics.SphereCast(transform.position, radius, Vector3.down, out hit, 100, _layer, QueryTriggerInteraction.Ignore);
		if (isHit)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawRay(transform.position, Vector3.down * hit.distance);
			Gizmos.DrawWireSphere(transform.position + Vector3.down * (hit.distance), radius);
		}
		else
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawRay(transform.position, Vector3.down * 100);
			Gizmos.DrawWireSphere(transform.position + Vector3.down * 100, radius);
		}
	}
}
