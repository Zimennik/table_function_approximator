using System;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int PointId;

    public event Action OnPointMove;

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

        curPosition.x = PointId;
        curPosition.z = 0;

        curPosition.y = Mathf.Clamp(curPosition.y, 0f, 5f);
        transform.position = curPosition;

        if (OnPointMove != null) OnPointMove();
    }
}