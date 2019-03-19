using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lagrange : LineBase
{
    private const float order = 4;

    // Use this for initialization
    void Start()
    {
        List<Vector3> points = new List<Vector3>();

        for (float i = 0; i <= 6; i += STEP)
        {
            points.Add(new Vector3(i, Calc(i, Points)));
        }
        DrawLine(points);
        DrawPoints();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override List<Vector3> Calculate()
    {
        return base.Calculate();
    }

    private float Calc(float currentX, List<Vector3> points)
    {
        float result = 0;

        for (int i = 0; i <= order; i++)
        {
            result += points[i].y * GetBasis(i, currentX, points);
        }
        return result;
    }

    private float GetBasis(int iterration, float currentX, List<Vector3> points)
    {
        float result = 1;
        for (int j = 0; j <= order; j++)
        {
            if (j == iterration) continue;

            result *= (currentX - points[j].x) / (points[iterration].x - points[j].x);
        }
        return result;
    }
}