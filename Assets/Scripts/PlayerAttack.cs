using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackArrow;
    [SerializeField] private Transform projectileContainer;
    [SerializeField] private float arrowDefaultPosition, projectileDefaultPosition;

    private void Update()
    {
        SetPosition(attackArrow, arrowDefaultPosition);
        SetRotationToMouse(attackArrow);

        // TESTING
        {
            // Convert mouse's screen position to world position
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);
            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 mouseWorldPosition = new Vector2(screenToWorld.x, screenToWorld.y);
            mouseWorldPosition.x -= transform.position.x;
            mouseWorldPosition.y -= transform.position.y;

            Debug.Log(mouseWorldPosition.ToString());
        }
    }

    private void SetPosition(Transform target, float targetDefaultPosition)
    {
        // Convert mouse's screen position to world position
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 mouseWorldPosition = new Vector2(screenToWorld.x, screenToWorld.y);

        // Calculate relative position of mouse to Player object
        float deltaX = mouseWorldPosition.x - transform.position.x;
        float deltaY = mouseWorldPosition.y - transform.position.y;

        // Convert mousePosition to radian position
        // LEFT is the default position (0 radian degree)
        float currentRad = Mathf.Atan2(deltaY, deltaX);

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
