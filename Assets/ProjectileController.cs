using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D pRigidbody;
    public float pSpeed;

    public Transform playerTransform;
    public Vector3 targetPosition;
    

    private void Start()
    {
        pRigidbody = GetComponent<Rigidbody2D>();
        ProjectileMovement();
    }

    private void Update()
    {
        Debug.Log("Target Position: " + targetPosition.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Entity"))
        {
            Destroy(gameObject);
        }
    }

    private void ProjectileMovement()
    {
        Vector2 _destination = targetPosition - playerTransform.position;
        pRigidbody.velocity = _destination.normalized * pSpeed;
    }
}
