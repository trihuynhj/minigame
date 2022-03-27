using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public Transform coreTransform;
    
    private Rigidbody2D eRigidbody;
    private Collider2D eCollider;
    public float speed;

    void Start()
    {
        eRigidbody = this.GetComponent<Rigidbody2D>();
        eCollider = this.GetComponent<Collider2D>();
        LinearMovement();
    }

    private void LinearMovement()
    {
        Vector3 _destination = coreTransform.position - transform.position;
        eRigidbody.velocity = _destination.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Core")
        {
            Destroy(gameObject);
        }
    }
}
