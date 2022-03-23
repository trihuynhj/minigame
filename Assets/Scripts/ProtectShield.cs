using System.Collections.Generic;
using UnityEngine;

public class ProtectShield : MonoBehaviour
{
    private LineRenderer shapeRenderer;
    private EdgeCollider2D shapeCollider;

    [SerializeField] private Transform centerPoint;

    [SerializeField] private int shapePoints;
    [SerializeField] private float shapeRadius;
    [SerializeField] private float minRadius, maxRadius, generateSpeed;
    [SerializeField] private float offsetX, offsetY;
    
    // This determines shape grow (true) or shrink (false)
    private bool generateVector;

    [SerializeField] GameController gameController;

    private void Awake()
    {
        shapeRenderer = GetComponent<LineRenderer>();
        shapeCollider = GetComponent<EdgeCollider2D>();
    }

    private void Start()
    {
        generateVector = true;
        InvokeRepeating("renderShape", .5f, .01f);
    }

    private void renderShape()
    {
        generateRadius(minRadius, maxRadius, generateSpeed);
        generateShapeAndCollision(shapePoints, shapeRadius);
    }

    private void generateShapeAndCollision(int steps, float radius)
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

            // Must separately bound the shape to Center Point's position
            // Because LineRenderer's positions are independent of parent object's position
            x += centerPoint.position.x;
            y += centerPoint.position.y;

            // Now translate the current position to the LineRenderer
            Vector3 currentPosition = new Vector3(x, y, 0f);

            // currentStep is "Index" in the Inspector
            // currentPosition is the X, Y, Z in the Inspector
            shapeRenderer.SetPosition(currentStep, currentPosition);
        }

        // Set all points of EdgeCollider2D
        shapeCollider.SetPoints(points);
    }

    private void generateRadius(float minR, float maxR, float speed)
    {
        if (generateVector)
        {
            shapeRadius += speed;
            if (shapeRadius >= maxR) { generateVector = false; }
        }
        else
        {
            shapeRadius -= speed;
            if (shapeRadius <= minR) { generateVector = true; }
        }
    }

    // 
    public void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
}
