using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Workspace : MonoBehaviour
{

	public List<Transform> Points;

	public List<Vector3> GetPoints()
	{
		return Points.Select(x => x.transform.position).ToList();
	}

	public void CreatePoint(Vector2 pos)
	{
		
	}
	
	
	
}
