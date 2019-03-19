using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class LeastSquare : LineBase
{
    public override void DrawLine(List<Vector3> points)
    {
        Points = points;  
        
        List<Vector3> linePoints = new List<Vector3>();

        List<float> abcd = LSM_Cubic(Points);

        for (float i = 0; i <= 6; i += STEP)
        {
            linePoints.Add(new Vector3(i, GetY(i, abcd)));
        }
        base.DrawLine(linePoints);
    }

    private float GetY(float currentX, List<float> abcd)
    {
        return abcd[0] * Mathf.Pow(currentX, 3) + abcd[1] * Mathf.Pow(currentX, 2) + abcd[2] * currentX + abcd[3];
    }

    public override List<Vector3> Calculate()
    {
        List<Vector3> result = new List<Vector3>();

        List<float> x = new List<float>();


        for (float i = 0; i <= 10; i += STEP)
        {
            result.Add(new Vector3(i, i*i/2));
        }

        return result;
    }
    
        private List<float> Kramer(float[,] left, float[] right)
        {
            int size = right.Length;

            List<float> result = new List<float>();

            float det = Determinant(left);
            

            for (int i = 0; i < size; i++)
            {


                float[,] subMatrix = left.Clone() as float[,];

                for (int j = 0; j < size; j++)
                {
                    subMatrix[j, i] = right[j];
                }

                float detI = Determinant(subMatrix);


                result.Add(detI / det);

            }

            return result;
        }

        private float Determinant(float[,] matrix)
        {
            float result = 0;

            int matrWidth = matrix.GetLength(0);


            if (matrWidth == 1) return matrix[0, 0];
            if (matrWidth == 2) return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            for (int i = 0; i < matrWidth; i++)
            {
                float[,] subMatrix = new float[matrWidth - 1, matrWidth - 1];

                for (int x = 0; x < (matrWidth - 1); x++)
                {
                    for (int y = 0; y < matrWidth; y++)
                    {
                        if (y == i) continue;

                        if (y < i)
                        {
                            subMatrix[x, y] = matrix[x + 1, y];
                        }
                        else
                        {
                            subMatrix[x, y - 1] = matrix[x + 1, y];
                        }

                    }
                }



                float minor = Determinant(subMatrix);


                result += (float)Mathf.Pow(-1, i) * minor * matrix[0, i];
            }



            return result;
        }

        public List<float> LSM_Cubic(List<Vector3> points)
        {
            float[,] left = new float[4, 4];
            float[] right = new float[4];

            for (int i = 0; i < points.Count; i++)
            {
                left[0, 0] += (float)Mathf.Pow(points[i].x, 6);
                left[0, 1] += (float)Mathf.Pow(points[i].x, 5);
                left[0, 2] += (float)Mathf.Pow(points[i].x, 4);
                left[0, 3] += (float)Mathf.Pow(points[i].x, 3);

                left[1, 0] += (float)Mathf.Pow(points[i].x, 5);
                left[1, 1] += (float)Mathf.Pow(points[i].x, 4);
                left[1, 2] += (float)Mathf.Pow(points[i].x, 3);
                left[1, 3] += (float)Mathf.Pow(points[i].x, 2);

                left[2, 0] += (float)Mathf.Pow(points[i].x, 4);
                left[2, 1] += (float)Mathf.Pow(points[i].x, 3);
                left[2, 2] += (float)Mathf.Pow(points[i].x, 2);
                left[2, 3] += points[i].x;

                left[3, 0] += (float)Mathf.Pow(points[i].x, 3);
                left[3, 1] += (float)Mathf.Pow(points[i].x, 2);
                left[3, 2] += points[i].x;
                left[3, 3] += 1;

                right[0] += points[i].y * (float)Mathf.Pow(points[i].x, 3);
                right[1] += points[i].y * (float)Mathf.Pow(points[i].x, 2);
                right[2] += points[i].y * points[i].x;
                right[3] += points[i].y;
            }

            return Kramer(left, right);
        }
    
    
   
}