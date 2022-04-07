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

        // Initially set both randomSpot and shieldCenter to the center of game arena
        //randomSpot = transform.position;
        shieldInitialCenterPosition = transform.position;

        InvokeRepeating("RenderShield", .5f, shieldGenerateInterval);
    }

    private void Update()
    {
        //Debug.Log("SHIELD CENTER POSITION: " + transform.position.ToString());

        // MOUSE WORLD POSITION
        //Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("MOUSE WORLD POSITION: " + mouseWorldPosition.ToString());

        // Press O to move shield towards randomSpot (TETING PURPOSE)
        if (Input.GetKeyDown(KeyCode.O) && coroutine_ShieldMovement == null)
        {
            coroutine_ShieldMovement = ShieldMovement();
            StartCoroutine(coroutine_ShieldMovement);
        }
    }

    private IEnumerator ShieldMovement()
    {
        // Generate a randomSpot within shieldMoveRange
        // also take into account shieldMaxRadius
        //Vector3 randomSpot = shieldInitialCenterPosition + Random.insideUnitSphere * shieldMoveRange;
        Vector3 randomSpot = shieldInitialCenterPosition + Random.insideUnitSphere * shieldMoveRange;
        randomSpot.z = 0f;
        Debug.Log("RANDOM SPOT: " + randomSpot.ToString());

        while (Vector3.Distance(transform.position, randomSpot) > 0.01f)
        {
            Vector3 currentPosition = transform.position;
            transform.position = Vector3.MoveTowards(currentPosition, randomSpot, shieldMoveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.001f);
        }

        coroutine_ShieldMovement = null;
    }
    private IEnumerator coroutine_ShieldMovement = null;

    private void RenderShield()
    {
        shieldRadius = GenerateRadius(shieldMinRadius, shieldMaxRadius, shieldGenerateSpeed);
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

    private float GenerateRadius(float minR, float maxR, float speed)
    {
        float _radius = shieldRadius;

        if (shieldGenerateVector)
        {
            _radius += speed;
            if (_radius >= maxR) { shieldGenerateVector = false; }
            return _radius;
        }
        else
        {
            _radius -= speed;
            if (_radius <= minR) { shieldGenerateVector = true; }
            return _radius;
        }
    }
}
