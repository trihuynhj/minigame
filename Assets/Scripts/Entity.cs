using UnityEngine;

public class Entity : MonoBehaviour
{
    [HideInInspector] public GameController gameController;
    private SpriteRenderer sprite;
    public bool isEnemy;

    private float entityHealth;
    public float attackStrength;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Set Health to correspond to Size
        entityHealth = transform.localScale.x + transform.localScale.y;
    }

    private void Update()
    {
        if (isEnemy) { sprite.color = Color.black; }
        else { sprite.color = Color.white; }

        if (entityHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            if (isEnemy) { gameController.points++; }
            else { gameController.vitalityPoints--; }

            entityHealth -= attackStrength;
        }
        else if (collision.CompareTag("Core"))
        {
            if (isEnemy) 
            {
                gameController.vitalityPoints--;
                gameController.points--;
            }
            else { gameController.points++; }
            
            Destroy(gameObject);
        }
    }
}
