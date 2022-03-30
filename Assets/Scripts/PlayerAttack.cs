using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // ATTACK ARROW
    [SerializeField] private Transform attackArrow;
    [SerializeField] private float attackArrowDefaultPosition;

    // PROJECTILE
    [SerializeField] private float projectileDefaultPosition;
    [SerializeField] private Transform projectileContainer;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileLifetime, projectileSpeed;
    [SerializeField] private float attackInterval;
  
    private void Update()
    {
        SetPositionToMouse(attackArrow, attackArrowDefaultPosition);
        SetRotationToMouse(attackArrow);

        if (Input.GetMouseButtonDown(0)) { SpawnProjectile(); }
    }

    private void SpawnProjectile()
    {
        // Instantiate (inside ProjectileContainer) and Set Projectile Initial Position
        GameObject projectile = Instantiate(projectilePrefab, projectileContainer);
        SetPositionToMouse(projectile.transform, projectileDefaultPosition);

        // Set Projectile movement toward Mouse Position via its Rigidbody2D
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = mouseWorldPosition - projectile.transform.position;
        projectileRb.AddForce(directionToMouse * projectileSpeed, ForceMode2D.Impulse);

        // Set Projectile lifetime in seconds after instantiation
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.lifetimeInSeconds = projectileLifetime;
    }

    private void SetPositionToMouse(Transform target, float targetDefaultPosition)
    {
        // Convert mouse's screen position to world position
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate relative position of mouse to Player object
        Vector3 mousePositionToPlayer = mouseWorldPosition - transform.position;

        // Convert mousePosition to radian position
        // LEFT is the default position (0 radian degree)
        float currentRad = Mathf.Atan2(mousePositionToPlayer.y, mousePositionToPlayer.x);

        // Translate currentRad into scale x and y,
        // then convert to actual position with designated radius (radius = arrow's x coordinate)
        // (also needs to be fixed to Player object)
        float x = Mathf.Cos(currentRad) * targetDefaultPosition + transform.position.x;
        float y = Mathf.Sin(currentRad) * targetDefaultPosition + transform.position.y;

        // Update arrow's position
        target.position = new Vector3(x, y, 0f);
    }

    private void SetRotationToMouse(Transform target)
    {
        Vector2 directionToPlayer = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        target.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
