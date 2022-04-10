using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private ForceField forceField;
    [SerializeField] private Transform core;

    [SerializeField] private GameObject[] arrows;
    [SerializeField] private Rigidbody2D rb;

    private Vector2[] eightDirections = new Vector2[8]
    {
        new Vector2(1f, 0f),
        new Vector2(1f, 1f),
        new Vector2(0f, 1f),
        new Vector2(-1f, 1f),
        new Vector2(-1f, 0f),
        new Vector2(-1f, -1f),
        new Vector2(0f, -1f),
        new Vector2(1f, -1f),
    };
    private Vector2 moveDirection;
    [SerializeField] private float moveForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        // Set Left as default direction (0th in array)
        moveDirection = eightDirections[0];
        arrows[0].SetActive(true);
    }

    private void Update()
    {
        moveDirection = SetDirection();
        ActivateArrow();

        ForceMovement();
        FreeMovement();
    }

    // ForceMovement applied when ForceField is active
    private void ForceMovement()
    {
        // Disable when the ForceField is inactive
        if (!forceField.isActiveAndEnabled) { return; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 directionVector = moveDirection.normalized;
            rb.velocity = directionVector * moveForce;
        }
    }

    // FreeMovement applied when ForceField is inactive
    private void FreeMovement()
    {
        // Disable when the ForceField is active
        if (forceField.isActiveAndEnabled) { return; }
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
        {
            Vector3 directionVector = moveDirection.normalized;
            transform.position += directionVector * moveForce * Time.deltaTime;
        }
    }

    // Set the direction toward which Player moves
    public Vector2 SetDirection()
    {
        if (!Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s")) 
        { 
            return moveDirection;
        }
        
        Vector2 _direction = new Vector2();

        if (Input.GetKey("d"))
        {
            if (!Input.GetKey("w") && !Input.GetKey("s")) { _direction = eightDirections[0]; }
            if (Input.GetKey("w")) { _direction = eightDirections[1]; }
            if (Input.GetKey("s")) { _direction = eightDirections[7]; }
        }

        if (Input.GetKey("w"))
        {
            if (!Input.GetKey("a") && !Input.GetKey("d")) { _direction = eightDirections[2]; }
            if (Input.GetKey("a")) { _direction = eightDirections[3]; }
            if (Input.GetKey("d")) { _direction = eightDirections[1]; }
        }

        if (Input.GetKey("a"))
        {
            if (!Input.GetKey("s") && !Input.GetKey("w")) { _direction = eightDirections[4]; }
            if (Input.GetKey("s")) { _direction = eightDirections[5]; }
            if (Input.GetKey("w")) { _direction = eightDirections[3]; }
        }

        if (Input.GetKey("s"))
        {
            if (!Input.GetKey("d") && !Input.GetKey("a")) { _direction = eightDirections[6]; }
            if (Input.GetKey("d")) { _direction = eightDirections[7]; }
            if (Input.GetKey("a")) { _direction = eightDirections[5]; }
        }

        return _direction;
    }

    // Determine the current direction and activate the appropriate arrow
    private void ActivateArrow()
    {
        for (int i = 0; i < eightDirections.Length; i++)
        {
            if (moveDirection == eightDirections[i])
            {
                arrows[i].SetActive(true);
            }
            else
            {
                arrows[i].SetActive(false);
            }
        }
    }
}
