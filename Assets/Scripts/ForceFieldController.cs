using System.Collections;
using UnityEngine;

public class ForceFieldController : MonoBehaviour
{
    [SerializeField] private Transform coreCenter;

    [SerializeField] private Vector3 destination;
    [SerializeField] private float circleRadius;
    [SerializeField] private float speed;

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
            StartCoroutine(MoveToPosition(circleRadius, speed, moveVector, destination));
        }

        if (Input.GetKeyDown("h"))
        {
            linearMove = !linearMove;
            if (linearMove) { StartCoroutine(LinearMovement(circleRadius, speed)); }
        }
;   }

    private IEnumerator LinearMovement(float _radius, float _speed)
    {
        // Must account for the offset of the Center Point
        float deltaX = transform.position.x - coreCenter.position.x;
        float deltaY = transform.position.y - coreCenter.position.y;

        // Calculate the current position in radian degree
        float currentRad = Mathf.Atan2(deltaY, deltaX);

        while (linearMove)
        {
            currentRad += _speed * moveVector * Time.deltaTime;

            float x = Mathf.Cos(currentRad) * _radius + coreCenter.position.x;
            float y = Mathf.Sin(currentRad) * _radius + coreCenter.position.y;

            transform.position = new Vector3(x, y, 0f);

            yield return null;
        }
        
    }

    private IEnumerator MoveToPosition(float _radius, float _speed, int _moveVector, Vector3 _destination)
    {
        // Must account for the offset of the Center Point
        float deltaX = transform.position.x - coreCenter.position.x;
        float deltaY = transform.position.y - coreCenter.position.y;

        // Calculate the current position in radian degree
        float currentRad = Mathf.Atan2(deltaY, deltaX);

        while (Vector3.Distance(transform.position, _destination) > 0.1f)
        {
            currentRad += _speed * _moveVector * Time.deltaTime;

            float _x = Mathf.Cos(currentRad) * _radius + coreCenter.position.x;
            float _y = Mathf.Sin(currentRad) * _radius + coreCenter.position.y;

            transform.position = new Vector3(_x, _y, 0f);

            yield return null;
        }
    }

    private void SetRotation()
    {
        Vector3 directionToCenter = coreCenter.position - transform.position;
        float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
    }
}
