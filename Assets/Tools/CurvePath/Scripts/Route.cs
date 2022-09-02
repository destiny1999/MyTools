using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField]
    Transform[] controlPoint;

    Vector2 gizmosPosition;

    List<Vector2> dots = new List<Vector2>();
    [SerializeField] float tipSquareSize = 3f;
    [SerializeField][Range(0.01f, 10f)] float bezierTValue = 0.05f;

    private void Start()
    {
        SaveDots();
    }

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += bezierTValue)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoint[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoint[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoint[2].position +
                Mathf.Pow(t, 3) * controlPoint[3].position;

            Gizmos.DrawSphere(gizmosPosition, tipSquareSize);
        }

        Gizmos.DrawLine(new Vector2(controlPoint[0].position.x, controlPoint[0].position.y),
            new Vector2(controlPoint[1].position.x, controlPoint[1].position.y));

        Gizmos.DrawLine(new Vector2(controlPoint[2].position.x, controlPoint[2].position.y),
            new Vector2(controlPoint[3].position.x, controlPoint[3].position.y));

    }
    void SaveDots()
    {
        Vector2 dotPosition = Vector2.zero;
        for (float t = 0; t <= 1; t += bezierTValue)
        {
            dotPosition = Mathf.Pow(1 - t, 3) * controlPoint[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoint[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoint[2].position +
                Mathf.Pow(t, 3) * controlPoint[3].position;

            dots.Add(dotPosition);
        }
    }
    public List<Vector2> GetDots()
    {
        return dots;
    }
}
