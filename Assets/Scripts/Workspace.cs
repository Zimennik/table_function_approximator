using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Workspace : MonoBehaviour
{
    public List<Point> Points = new List<Point>();
    public Point PointPrefab;

    public LeastSquare LeastSquare;
    public Lagrange Lagrange;

    private const int MINX = 0;
    private const int MINY = 0;
    private const int MAXX = 5;
    private const int MAXY = 5;
    private const int POINTS_COUNT = 5;


    public List<Vector3> GetPoints()
    {
        return Points.Select(x => x.transform.position).ToList();
    }


    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Points.Count >= 5) return;

            int xPos = GetClosestX(Input.mousePosition);

            if (Points.FirstOrDefault(x => x.PointId == xPos) == null)
            {
                float yPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;


                CreatePoint(new Vector3(xPos, yPos, 0), xPos);

                if (Points.Count >= POINTS_COUNT)
                {
                    RedrawLines();
                }
            }
        }
    }

    public void RedrawLines()
    {
        if (Points == null || Points.Count < POINTS_COUNT) return;
        LeastSquare.DrawLine(GetPoints());
        Lagrange.DrawLine(GetPoints());
    }


    public void CreatePoint(Vector3 pos, int Id)
    {
        Point p = Instantiate(PointPrefab, pos, Quaternion.identity);
        p.PointId = Id;
        p.OnPointMove += OnPointMove;

        Points.Add(p);
    }

    public void OnPointMove(int id, Vector3 pos)
    {
        RedrawLines();
    }

    private int GetClosestX(Vector3 screenPos)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        return Mathf.Clamp(Mathf.RoundToInt(worldPos.x), MINX, MAXX);
    }
}