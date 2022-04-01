using UnityEngine;

public class Entity : MonoBehaviour
{
    [HideInInspector] public GameController gameController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            gameController.currentPoint++;
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Core"))
        {
            gameController.currentPoint--;
            Destroy(gameObject);
        }
    }
}
