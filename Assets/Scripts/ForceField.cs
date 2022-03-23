using UnityEngine;

public class ForceField : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] Transform playerTransform;
    [SerializeField] private SpriteRenderer sprite;

    private int forceVector;
    [SerializeField] private float forceMagnitude;

    private Color pullColor;
    private Color pushColor;

    private void Start()
    {
        forceVector = 1;
        pullColor = sprite.color;
        pushColor = Color.blue;
    }

    private void Update()
    {
        if (Input.GetKeyDown("g")) { SwitchForce(); }

        Force();
    }

    private void Force()
    {
        Vector3 direction = transform.position - playerTransform.position;
        Vector3 appliedForce = direction.normalized * forceMagnitude * forceVector * Time.deltaTime;

        player.AddForce(appliedForce);
    }


    // Switch between Gravity (pulling) and Anti-Gravity (pushing)
    private void SwitchForce()
    {
        forceVector = -forceVector;

        if (sprite.color == pullColor) { sprite.color = pushColor; }
        else { sprite.color = pullColor; }

    }
}
