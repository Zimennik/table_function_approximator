using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{

	public int PointId { get; set; }

	public event Action<int, Vector2> OnPointMove;
	
	
	void OnMouseDown()
	{
	}


}
