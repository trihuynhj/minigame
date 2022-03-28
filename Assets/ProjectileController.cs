using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D pRigidbody;
    public Vector3 targetPosition;

    [SerializeField] private float pSpeed;

    private void Start()
    {
        pRigidbody = GetComponent<Rigidbody2D>();
        ProjectileMovement();
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
        Vector2 _destination = transform.position - targetPosition;
        pRigidbody.velocity = _destination.normalized * pSpeed;
    }
}
