using UnityEngine;

public class ProjectileController : MonoBehaviour
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
            Destroy(collision.gameObject);
        }
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
