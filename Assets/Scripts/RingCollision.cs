using UnityEngine;
using System.Collections.Generic;


public class RingCollision : MonoBehaviour
{
    private EdgeCollider2D ringCollider;
    [SerializeField] private int colliderPoints;
    [SerializeField] private float colliderRadius;

    private void Awake()
    {
        ringCollider = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        RenderRing();
    }

    private void RenderRing()
    {
        List<Vector2> points = new List<Vector2>();

        for (int currentPoint = 0; currentPoint < colliderPoints; currentPoint++)
        {
            float circumferenceProgress = (float)currentPoint / (colliderPoints - 1);
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float x = Mathf.Cos(currentRadian) * colliderRadius;
            float y = Mathf.Sin(currentRadian) * colliderRadius;

            // Add current point to list of EdgeCollider2D
            points.Add(new Vector2(x, y));
        }
        ringCollider.SetPoints(points);
    }
}
