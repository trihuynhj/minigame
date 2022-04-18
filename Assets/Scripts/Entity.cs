using UnityEngine;

public class Entity : MonoBehaviour
{
    [HideInInspector] public GameController gameController;
    private SpriteRenderer sprite;
    public bool isEnemy;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isEnemy) { sprite.color = Color.black; }
        else { sprite.color = Color.white; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            if (isEnemy) { gameController.vitalityPoint++; }
            else { gameController.vitalityPoint--; }

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Core"))
        {
            if (isEnemy) { gameController.vitalityPoint--; }
            else { gameController.vitalityPoint++; }
            
            Destroy(gameObject);
        }
    }
}
