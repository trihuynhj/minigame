using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // ATTACK ARROW
    [SerializeField] private Transform attackArrow;
    [SerializeField] private float attackArrowDefaultPosition;

    // PROJECTILE
    [SerializeField] private Transform projectileContainer;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileDefaultPosition;
    [SerializeField] private float attackInterval;
    private bool canSpawnProjectile;

    private void Start()
    {
        canSpawnProjectile = true;
    }

    private void Update()
    {
        SetPosition(attackArrow, attackArrowDefaultPosition);
        SetRotationToMouse(attackArrow);

        if (Input.GetMouseButtonDown(0) && canSpawnProjectile) { SpawnProjectile(); }

        SetAttackInterval(attackInterval);
    }

    private void SpawnProjectile()
    {
        // Instantiate (inside ProjectileContainer) and Set Projectile Initial Position
        GameObject p = Instantiate(projectilePrefab, projectileContainer);
        SetPosition(p.transform, projectileDefaultPosition);
        
        // Set Projectile Movement to Mouse Position
        ProjectileController pController = p.GetComponent<ProjectileController>();
        pController.targetPosition = MousePositionToPlayer();
    }

    private void SetAttackInterval(float timeWait)
    {
        float elapsedTime = 0f;

        while (elapsedTime < timeWait)
        {
            canSpawnProjectile = false;
            elapsedTime += Time.deltaTime;
        }

        canSpawnProjectile = true;

    } 

    private void SetPosition(Transform target, float targetDefaultPosition)
    {
        Vector3 delta = MousePositionToPlayer();

        // Convert mousePosition to radian position
        // LEFT is the default position (0 radian degree)
        float currentRad = Mathf.Atan2(delta.y, delta.x);

        // Translate currentRad into scale x and y,
        // then convert to actual position with designated radius (radius = arrow's x coordinate)
        // (also needs to be fixed to Player object)
        float x = Mathf.Cos(currentRad) * targetDefaultPosition + transform.position.x;
        float y = Mathf.Sin(currentRad) * targetDefaultPosition + transform.position.y;

        // Update arrow's position
        target.position = new Vector3(x, y, 0f);
    }

    private Vector3 MousePositionToPlayer()
    {
        // Convert mouse's screen position to world position
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 mouseWorldPosition = new Vector2(screenToWorld.x, screenToWorld.y);

        // Calculate relative position of mouse to Player object
        float x = mouseWorldPosition.x - transform.position.x;
        float y = mouseWorldPosition.y - transform.position.y;

        return new Vector3(x, y, 0f);
    }

    private void SetRotationToMouse(Transform target)
    {
        Vector2 directionToPlayer = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        target.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
