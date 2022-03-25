using System.Collections.Generic;
using UnityEngine;

public class ProtectShieldController : MonoBehaviour
{
    private LineRenderer shapeRenderer;
    private EdgeCollider2D shapeCollider;

    [SerializeField] public float shapeRadius;
    [SerializeField] private int shapePoints;
    [SerializeField] private float minRadius, maxRadius, generateSpeed;
    [SerializeField] private float offsetX, offsetY;
    
    // This determines shape grow (true) or shrink (false)
    private bool generateVector;

    // Center of the Protect Shield
    public Vector2 protectShieldCenter;

    [SerializeField] ScoreSystem scoreSystem;

    private void Awake()
    {
        shapeRenderer = GetComponent<LineRenderer>();
        shapeCollider = GetComponent<EdgeCollider2D>();
    }

    private void Start()
    {
        generateVector = true;
        InvokeRepeating("RenderShape", .5f, .01f);

        shapeRadius = 5f;
    }

    private void Update()
    {
        protectShieldCenter = new Vector2(transform.position.x + offsetX, transform.position.y + offsetY);
    }

    private void RenderShape()
    {
        shapeRadius = GenerateRadius(minRadius, maxRadius, generateSpeed);
        GenerateShapeAndCollision(shapePoints, shapeRadius);
    }

    private void GenerateShapeAndCollision(int steps, float radius)
    {
        shapeRenderer.positionCount = steps;
        List<Vector2> points = new List<Vector2>();

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            // This returns a value between 0.0 and 1.0 which represents where
            // the currentStep is on the circle (Left at 0.0, counter-clockwise)
            // 0.0 and 1.0 happen to be at the same position (Left)
            float circumferenceProgress = (float)currentStep / (steps - 1);

            // This is called TAU, 1 TAU = 6.28 radius
            // TAU represents the circumference in terms of its radius
            // It means the circle travels 6.28 times its radius to go full circle
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            // TRANSLATE RADIAN TO X- AND Y-DIMENSION
            // Calculate the x using Cosine, y using Sine
            // This returns a value between -1.0 and 1.0 on the x axis and y axis respectively
            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            // Above are only the scale of the circle
            // To get the actual size of the circle, multiple them by the radius
            // offsetX & offsetY to make the Shield dynamically move
            float x = xScaled * radius + offsetX;
            float y = yScaled * radius + offsetY;

            // Add current point to list of EdgeCollider2D
            points.Add(new Vector2(x, y));

            // Must separately bound the shape to Center Point's position (this ProtectShield object itself)
            // Because LineRenderer's positions controlled by a script are independent of object's position
            x += transform.position.x;
            y += transform.position.y;

            // Now translate the current position to the LineRenderer
            Vector3 currentPosition = new Vector3(x, y, 0f);

            // currentStep is "Index" in the Inspector
            // currentPosition is the X, Y, Z in the Inspector
            shapeRenderer.SetPosition(currentStep, currentPosition);

            shapeRenderer.sortingOrder = 5;
        }

        // Set all points of EdgeCollider2D
        shapeCollider.SetPoints(points);
    }

    private float GenerateRadius(float minR, float maxR, float speed)
    {
        float _radius = shapeRadius;

        if (generateVector)
        {
            _radius += speed;
            if (_radius >= maxR) { generateVector = false; }
            return _radius;
        }
        else
        {
            _radius -= speed;
            if (_radius <= minR) { generateVector = true; }
            return _radius;
        }
    }
}
