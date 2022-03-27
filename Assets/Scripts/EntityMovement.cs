using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public Transform coreTransform;
    
    private Rigidbody2D eRigidbody;
    public float speed;

    void Start()
    {
        eRigidbody = this.GetComponent<Rigidbody2D>();
        LinearMovement();
    }

    private void LinearMovement()
    {
        Vector3 _destination = coreTransform.position - transform.position;
        eRigidbody.velocity = _destination.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Core"))
        {
            Destroy(gameObject);
        }
    }
}
