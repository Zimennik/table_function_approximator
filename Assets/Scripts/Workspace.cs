using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Workspace : MonoBehaviour
{
    public List<Point> Points = new List<Point>();
    public Point PointPrefab;
    public Transform PointsHolder;

    public LeastSquare LeastSquare;
    public Lagrange Lagrange;

    private const int MINX = 0;
    private const int MAXX = 5;
    private const int POINTS_COUNT = 5;


    public List<Vector3> GetPoints()
    {
        return Points.Select(x => x.transform.position).ToList();
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
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


    public void CreatePoint(Vector3 pos, int id)
    {
        Point p = Instantiate(PointPrefab, PointsHolder);
        p.transform.position = pos;
        p.PointId = id;
        p.OnPointMove += OnPointMove;

        Points.Add(p);
    }

    public void OnPointMove()
    {
        RedrawLines();
    }

    private int GetClosestX(Vector3 screenPos)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        return Mathf.Clamp(Mathf.RoundToInt(worldPos.x), MINX, MAXX);
    }
}