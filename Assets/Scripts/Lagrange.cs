using System.Collections.Generic;
using UnityEngine;

public class Lagrange : LineBase
{
    private const float ORDER = 4;


    public override void DrawLine(List<Vector3> points)
    {
        Points = points;
        List<Vector3> linePoints = new List<Vector3>();

        for (float i = 0; i <= 6; i += STEP)
        {
            linePoints.Add(new Vector3(i, Calc(i, Points)));
        }
        base.DrawLine(linePoints);
    }

    private float Calc(float currentX, List<Vector3> points)
    {
        float result = 0;

        for (int i = 0; i <= ORDER; i++)
        {
            result += points[i].y * GetBasis(i, currentX, points);
        }
        return result;
    }

    private float GetBasis(int iterration, float currentX, List<Vector3> points)
    {
        float result = 1;
        for (int j = 0; j <= ORDER; j++)
        {
            if (j == iterration) continue;

            result *= (currentX - points[j].x) / (points[iterration].x - points[j].x);
        }
        return result;
    }
}