using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public Transform coreTransform;

    private Rigidbody2D rb;
    public float speed;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        LinearMovement();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, coreTransform.position) < coreTransform.localScale.x + .1f)
        {
            Destroy(gameObject);
        }
    }

    private void LinearMovement()
    {
        Vector3 _destination = coreTransform.position - transform.position;
        rb.velocity = _destination.normalized * speed;
    }
}
