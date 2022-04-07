using System.Collections;
using UnityEngine;

public class ForceFieldController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private Transform coreTransform;

    [SerializeField] private float circleRadius;
    [SerializeField] private float moveSpeed, linearSpeed;

    // Positive = counter-clockwise, negative = clockwise
    private int moveVector;
    // Toggle LinearMovement
    private bool linearMove;

    private void Start()
    {
        moveVector = -1;
        linearMove = false;
    }

    private void Update()
    {
        // Sprite must always rotate to face the center point
        SetRotation();

        if (Input.GetKeyDown("p")) 
        { 
            moveVector = -moveVector;
        }

        if (Input.GetKeyDown("r"))
        {
            if (linearMove) { return; }
            StartCoroutine(MoveToPosition(circleRadius, moveSpeed, moveVector));
        }

        if (Input.GetKeyDown("h"))
        {
            linearMove = !linearMove;
            if (linearMove) { StartCoroutine(LinearMovement(circleRadius, linearSpeed)); }
        }
;   }

    private IEnumerator LinearMovement(float _radius, float _speed)
    {
        // Must account for the offset of the Center Point
        float deltaX = transform.position.x - coreTransform.position.x;
        float deltaY = transform.position.y - coreTransform.position.y;

        // Calculate the current position in radian degree
        float currentRad = Mathf.Atan2(deltaY, deltaX);

        while (linearMove)
        {
            currentRad += _speed * moveVector * Time.deltaTime;

            float x = Mathf.Cos(currentRad) * _radius + coreTransform.position.x;
            float y = Mathf.Sin(currentRad) * _radius + coreTransform.position.y;

            transform.position = new Vector3(x, y, 0f);

            yield return new WaitForSeconds(0.001f);
        }
        
    }

    private IEnumerator MoveToPosition(float _radius, float _speed, int _moveVector)
    {
        // Generate a random destination to move to (z must be zero to check distance)
        Vector3 xrandomDestination = coreTransform.position + Random.onUnitSphere * _radius;
        xrandomDestination.z = 0f;
        Debug.Log("RANDOM DESTINATION: " + xrandomDestination.ToString());

        float randomRad = Random.Range(0f, 359f) * Mathf.Deg2Rad;
        float randomX = Mathf.Cos(randomRad) * _radius + coreTransform.position.x;
        float randomY = Mathf.Sin(randomRad) * _radius + coreTransform.position.y;
        Vector3 randomDestination = new Vector3(randomX, randomY, 0f);

        // Must account for the offset of the Center Point
        Vector2 delta = transform.position - coreTransform.position;
        // Calculate the current position in radian degree
        float currentRad = Mathf.Atan2(delta.y, delta.x);

        while (Vector3.Distance(transform.position, randomDestination) > 1f)
        {
            currentRad += _speed * _moveVector * Time.deltaTime;

            float _x = Mathf.Cos(currentRad) * _radius + coreTransform.position.x;
            float _y = Mathf.Sin(currentRad) * _radius + coreTransform.position.y;

            transform.position = new Vector3(_x, _y, 0f);

            yield return new WaitForSeconds(0.001f);
        }
    }

    private void SetRotation()
    {
        Vector3 directionToCenter = coreTransform.position - transform.position;
        float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
    }
}
