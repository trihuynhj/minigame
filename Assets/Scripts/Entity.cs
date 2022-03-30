using UnityEngine;

public class Entity : MonoBehaviour
{
    public Transform coreTransform;
    
    private Rigidbody2D eRigidbody;
    public float speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Core"))
        {
            Destroy(gameObject);
        }
    }
}
