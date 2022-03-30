using UnityEngine;

public class Entity : MonoBehaviour
{    private void OnTriggerEnter2D(Collider2D collision)
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
