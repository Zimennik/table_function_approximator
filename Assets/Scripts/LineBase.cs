using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LineBase : MonoBehaviour
{
	public List<Vector3> Points = new List<Vector3>();
	public LineRenderer LineRenderer;
	public GameObject PointPrefab;
	protected const float STEP = 0.05f;

	public void Awake()
	{
		Points = new List<Vector3>()
		{
			new Vector3(1, 5),
			new Vector3(2, 0),
			new Vector3(3, 3),
			new Vector3(4, 4),
			new Vector3(5, 2),
		};
	}
	
	public virtual void SetPoints(List<Vector3> points)
	{
		Points = points;
	}

	public virtual List<Vector3> Calculate()
	{
		return new List<Vector3>();
	}

	public void DrawLine(List<Vector3> points)
	{
		LineRenderer.positionCount = points.Count;
		LineRenderer.SetPositions(points.ToArray());
	}

	public void DrawPoints()
	{
		foreach (Vector3 vector3 in Points)
		{
			Instantiate(PointPrefab, vector3, Quaternion.identity);
		}
	}
}
