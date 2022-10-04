using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{

	[SerializeField] private float _viewRadius;
	[Range(0, 360)]
	[SerializeField] private float _viewAngle;

	[SerializeField] private LayerMask _targetMask;
	[SerializeField] private LayerMask _obstacleMask;

	private List<Transform> _visibleTargets = new List<Transform>();


	public float ViewRadius => _viewRadius;
	public float ViewAngle => _viewAngle;
	public IEnumerable<Transform> VisibleTargets => _visibleTargets;



	private void Start()
	{
		StartCoroutine("FindTargetsWithDelay", .2f);
	}



    private IEnumerator FindTargetsWithDelay(float delay)
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		}
	}

	private void FindVisibleTargets()
	{
		_visibleTargets.Clear();
		Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, _viewRadius, _targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.right, dirToTarget) < _viewAngle / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, target.position);
				if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, _obstacleMask))
				{
					_visibleTargets.Add(target);
				}
			}
		}
	}




	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
	}




}