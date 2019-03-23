using System.Collections.Generic;
using UnityEngine;

public abstract class LineBase : MonoBehaviour
{
	public List<Vector3> Points = new List<Vector3>();
	public LineRenderer LineRenderer;
	public GameObject PointPrefab;
	protected const float STEP = 0.05f;



	public virtual void DrawLine(List<Vector3> points)
	{
		LineRenderer.positionCount = points.Count;
		LineRenderer.SetPositions(points.ToArray());
	}

}
