using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ProtectShieldController : MonoBehaviour
{
    private LineRenderer shieldRenderer;
    private EdgeCollider2D shieldCollider;

    [SerializeField] GameController gameController;
    [SerializeField] private Material shieldMaterial;

    [SerializeField] public float shieldRadius;
    [SerializeField] private int shieldRenderPoints;
    [SerializeField] private float shieldMinRadius, shieldMaxRadius;
    [SerializeField] private float shieldGenerateInterval, shieldGenerateSpeed;
    [SerializeField] private float shieldMoveRange, shieldMoveSpeed;
    
    // Determines shield grow (true) or shrink (false)
    private bool shieldGenerateVector;
    // Initial position of ProtectShield, same as center of game arena
    [HideInInspector] public Vector3 shieldInitialCenterPosition;

    private void Awake()
    {
        shieldRenderer = GetComponent<LineRenderer>();
        shieldCollider = GetComponent<EdgeCollider2D>();
    }

    private void Start()
    {
        shieldGenerateVector = true;

        // Initially set shieldCenter to the center of game arena
        shieldInitialCenterPosition = transform.position;

        InvokeRepeating("RenderShield", .5f, shieldGenerateInterval);
    }

    private void Update()
    {
        if (gameController.level > 7 && coroutine_InvokeShieldMovement == null)
        {
            coroutine_InvokeShieldMovement = InvokeShieldMovement();
            StartCoroutine(coroutine_InvokeShieldMovement);
        }
    }

    private IEnumerator InvokeShieldMovement()
    {
        StartCoroutine(ShieldMovement());
        yield return new WaitForSeconds(Random.Range(5f, 10f));

        coroutine_InvokeShieldMovement = null;
    }
    private IEnumerator coroutine_InvokeShieldMovement = null;

    private IEnumerator ShieldMovement()
    {
        // Generate a randomSpot within shieldMoveRange
        // also take into account shieldMaxRadius
        Vector3 randomSpot = shieldInitialCenterPosition + Random.insideUnitSphere * shieldMoveRange;
        randomSpot.z = 0f;

        while (Vector3.Distance(transform.position, randomSpot) > 0.01f)
        {
            Vector3 currentPosition = transform.position;
            transform.position = Vector3.MoveTowards(currentPosition, randomSpot, shieldMoveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.001f);
        }
    }

    private void RenderShield()
    {
        GenerateMinMaxRadius();
        GenerateRadius();
        GenerateShapeAndCollision(shieldRenderPoints, shieldRadius);
    }

    private void GenerateShapeAndCollision(int steps, float radius)
    {
        shieldRenderer.positionCount = steps;
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
            float x = xScaled * radius;
            float y = yScaled * radius;

            // Add current point to list of EdgeCollider2D
            points.Add(new Vector2(x, y));

            // Must separately bound the shield to Center Point's position (this ProtectShield object itself)
            // Because LineRenderer's positions controlled by a script are independent of object's position
            x += transform.position.x;
            y += transform.position.y;

            // Now translate the current position to the LineRenderer
            Vector3 currentPosition = new Vector3(x, y, 0f);

            // currentStep is "Index" in the Inspector
            // currentPosition is the X, Y, Z in the Inspector
            shieldRenderer.SetPosition(currentStep, currentPosition);
        }

        // Set all points of EdgeCollider2D
        shieldCollider.SetPoints(points);
    }

    private void GenerateRadius()
    {
        if (shieldGenerateVector)
        {
            shieldRadius += shieldGenerateSpeed;
            if (shieldRadius >= shieldMaxRadius) { shieldGenerateVector = false; }
        }
        else
        {
            shieldRadius -= shieldGenerateSpeed;
            if (shieldRadius <= shieldMinRadius) { shieldGenerateVector = true; }
        }
    }

    private void GenerateMinMaxRadius()
    {
        float sizeDiff;
        float index = gameController.level;

        if (index < 7) 
        { 
            sizeDiff = .45f;
            shieldMinRadius = 6f;
            shieldMoveRange = 7f;
        }
        else if (index < 13) 
        { 
            sizeDiff = .35f;
            shieldMinRadius = 3.5f;
            shieldMoveRange = 9f;
        }
        else 
        { 
            sizeDiff = .25f;
            shieldMinRadius = 2f;
            shieldMoveRange = 11f;
        }

        shieldMaxRadius = shieldMinRadius * (1f + sizeDiff);
    }
}
