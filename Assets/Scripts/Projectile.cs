using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetimeInSeconds;

    private void Start()
    {
        Invoke("SelfDestroy", lifetimeInSeconds);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Entity"))
        {
            Destroy(gameObject);
        }
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
